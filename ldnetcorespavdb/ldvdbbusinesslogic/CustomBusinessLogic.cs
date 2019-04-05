using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Text;

using ldvdbclasslibrary;
using ldvdbdal;

using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Messaging.V1.Service;
using Twilio.Types;

namespace ldvdbbusinesslogic
{
    public class CustomBusinessLogic : ICustomBusinessLogic, IEmailSender, ITextSender
    {
        private ICustomDataAccess _customDataAccess = null;
        private static AppConfiguration appConfiguration { get; set; }        

        private string BuildRegistrationEmail(string host, string registrationCode)
        {
            var body = string.Empty;
            var bodyBuilder = new StringBuilder();

            bodyBuilder.AppendLine("You have created a new account at a Youth in View website");
            bodyBuilder.AppendLine("To activate your account click on the below link...");
            bodyBuilder.AppendLine("https://" + host + "/#confirmregistration?registerId=" + registrationCode);

            body = bodyBuilder.ToString();

            return body;
        }

        private string BuildInternalMessageDeliveredEmail(string host)
        {
            var body = string.Empty;
            var bodyBuilder = new StringBuilder();

            bodyBuilder.AppendLine("You have a new message in your inbox at the Youth in View website");
            bodyBuilder.AppendLine("To read your message login to...");
            bodyBuilder.AppendLine("https://" + host);

            body = bodyBuilder.ToString();

            return body;
        }

        private string BuildInternalMessageDeliveredSms(string host)
        {
            var body = string.Empty;
            var bodyBuilder = new StringBuilder();

            bodyBuilder.AppendLine("You have a new message in your inbox at the Youth in View website");
            bodyBuilder.AppendLine("To read your message login to...");
            bodyBuilder.AppendLine("https://" + host);

            body = bodyBuilder.ToString();

            return body;
        }

        private void SendEmailsAndTextsForMessageRecipients(string host, List<messagerecipients> listMessageRecipients)
        {
            foreach (var recipient in listMessageRecipients)
            {
                var adisplaycontactinformation = _customDataAccess.RetrieveContactInformationForSystemAccount(recipient.AccountID,
                    recipient.AccountTypeID);

                if (adisplaycontactinformation.EnableEmailMessages == true)
                {
                    var msg = BuildInternalMessageDeliveredEmail(host);
                    if (!String.IsNullOrEmpty(adisplaycontactinformation.EmailAddress))
                    {
                        SendThroughProduction(adisplaycontactinformation.EmailAddress, "Youth In View Internal Message", msg);
                    }
                }

                if (adisplaycontactinformation.EnableSmsMessages == true)
                {
                    var msg = BuildInternalMessageDeliveredSms(host);
                    if (!String.IsNullOrEmpty(adisplaycontactinformation.MobilePhoneNumber))
                    {
                        SendSMSThroughProduction(adisplaycontactinformation.MobilePhoneNumber, msg);
                    }
                }
            }
        }

        public CustomBusinessLogic(ICustomDataAccess customDataAccess)
        {
            this._customDataAccess = customDataAccess;
        }

        public void SetConfiguration(AppConfiguration appconfiguration)
        {
            CustomBusinessLogic.appConfiguration = appconfiguration;
        }

        public bool RegisterUserAccountByRegistrationId(string registrationId)
        {
            return _customDataAccess.RegisterUserAccountByRegistrationId(registrationId);
        }

        public string GenerateRandomNumberString(int countCharacters)
        {
            Random random = new Random();
            var randomString = "";

            for (int i = 0; i < countCharacters; i++)
            {
                randomString += random.Next(0, 9).ToString();
            }

            return randomString;
        }

        public bool CreateUserAccount(string email, string password, AccountType accountType, string host)
        {
            var ret = false;

            var registrationCode = GenerateRandomNumberString(100);

            var passwordHash = PasswordHasher.HashPassword(password);

            if (true == _customDataAccess.CreateUserAccount(email, passwordHash, accountType, registrationCode))
            {                               
                if (true == SendThroughProduction(email, "Youth in View Account Registration", BuildRegistrationEmail(host, registrationCode)))
                {
                    ret = true;                    
                }
            }

            return ret;
        }

        public bool SendThroughLocalTest(string to, string subject, string body)
        {
            var ret = true;

            try
            {
                var client = new SmtpClient("localhost", 25)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("inquiries@leoparddata.local", "letmein0987"),
                    UseDefaultCredentials = false,
                    EnableSsl = false
                };
                client.Send("inquiries@leoparddata.local", to, subject, body);
            }
            catch (Exception ex)
            {
                ret = false;
            }

            return ret;
        }

        public bool SendThroughProduction(string to, string subject, string body)
        {
            var client = new SendGridClient(CustomBusinessLogic.appConfiguration.SendGridApiKey);
            
            var from = new EmailAddress(CustomBusinessLogic.appConfiguration.FromEmailAddress, "");            
            var toemailaddress = new EmailAddress(to);            
            var msg = MailHelper.CreateSingleEmail(from, toemailaddress, subject, body, "");
            client.SendEmailAsync(msg).Wait();
            return true;
        }

        public bool SendSMSThroughProduction(string to, string body)
        {
            var accountSid = CustomBusinessLogic.appConfiguration.TwilioaccountSid;
            var authToken = CustomBusinessLogic.appConfiguration.TwilioAuthToken;
            var client = new TwilioRestClient(accountSid, authToken);

            // Pass the client into the resource method
            try
            {
                var message = MessageResource.Create(
                    to: new PhoneNumber(to),
                    from: new PhoneNumber(CustomBusinessLogic.appConfiguration.FromSmsPhoneNumber),
                    body: body,
                    client: client);
            }
            catch (Exception ex)
            {

            }
            

            return true;
        }

        public displaylogin LoginSystemUser(string email, string password)
        {       
            var tuple = _customDataAccess.LoginSystemUser(email);
            var passwordHash = tuple.Item2;

            if (true == PasswordHasher.VerifyHashedPassword(passwordHash, password))
                return new displaylogin() { SystemUserID = tuple.Item1.SystemUserID, AccountTypeID = tuple.Item1.AccountTypeID };
            else
                return new displaylogin() { SystemUserID = tuple.Item1.SystemUserID, AccountTypeID = tuple.Item1.AccountTypeID };
        }

        public long GetSystemUserAccountType(long userId)
        {
            return _customDataAccess.GetSystemUserAccountType(userId);
        }

        public long SendPasswordResetEmail(string email, string host)
        {
            var ret = -1;

            var passwordResetCode = GenerateRandomNumberString(100);

            if (0 == _customDataAccess.SendPasswordResetEmail(email, passwordResetCode))
            {
                var body = string.Empty;
                var bodyBuilder = new StringBuilder();
                
                bodyBuilder.AppendLine("To reset your account password click on the below link...");
                bodyBuilder.AppendLine("https://" + host + "/#modifypassword?passwordResetCode=" + passwordResetCode);

                body = bodyBuilder.ToString();
                
                if (true == SendThroughProduction(email, "Youth in View Account Registration", body))
                {
                    ret = 0;
                }
            }

            return ret;
        }

        public long UpdateUserPassword(string passwordResetCode, string password)
        {
            var hashedPassword = PasswordHasher.HashPassword(password);

            return _customDataAccess.UpdateUserPassword(passwordResetCode, hashedPassword);
        }

        public individuals RetrieveIndividualInformationBySystemUserID(long systemUserID)
        {
            return _customDataAccess.RetrieveIndividualInformationBySystemUserID(systemUserID);
        }

        public bool SaveIndividualInformation(long systemUserID, individuals individualInformation)
        {
            return _customDataAccess.SaveIndividualInformation(systemUserID, individualInformation);
        }

        public List<programs> RetrieveAllPrograms()
        {
            return _customDataAccess.RetrieveAllPrograms();
        }

        public List<programevents> RetrieveAllProgramEventsByProgramId(long programId)
        {
            return _customDataAccess.RetrieveAllProgramEventsByProgramId(programId);
        }

        public List<DisplayAdministrator> RetrieveAllAdministrators()
        {
            return _customDataAccess.RetrieveAllAdministrators();
        }

        public bool AddAdministrator(DisplayAdministrator displayAdministrator, string host)
        {
            var ret = false;

            var registrationCode = GenerateRandomNumberString(100);

            var passwordHash = PasswordHasher.HashPassword(displayAdministrator.Password);

            displayAdministrator.Password = passwordHash;

            if (true == _customDataAccess.AddAdministrator(displayAdministrator, registrationCode))
            {               
                if (true == SendThroughProduction(displayAdministrator.EmailAddress, "Youth in View Account Registration", BuildRegistrationEmail(host, registrationCode)))
                {
                    ret = true;
                }
            }

            return ret;
        }

        public void DeleteAdministrator(long adminId)
        {
            _customDataAccess.DeleteAdministrator(adminId);
        }

        public List<displayclient> RetrieveAllClients()
        {
            return _customDataAccess.RetrieveAllClients();
        }

        public bool AddClient(displayclient displayClient)
        {
            return _customDataAccess.AddClient(displayClient);
        }

        public bool ModifyClient(displayclient displayClient)
        {
            return _customDataAccess.ModifyClient(displayClient);
        }

        public void DeleteClient(long clientId)
        {
            _customDataAccess.DeleteClient(clientId);
        }

        public displayclient RetrieveClientInformationByClientID(long clientId)
        {
            return _customDataAccess.RetrieveClientInformationByClientID(clientId);
        }

        public bool SaveClientInformation(displayclient displayClient)
        {
            return _customDataAccess.SaveClientInformation(displayClient);
        }

        public List<displayclient> RetrieveClientsNotInExclusionList(List<long> clientIdExclusionList)
        {
            return _customDataAccess.RetrieveClientsNotInExclusionList(clientIdExclusionList);
        }

        public individuals RetrieveClientFatherByClientID(long clientId)
        {
            return _customDataAccess.RetrieveClientFatherByClientID(clientId);
        }

        public bool SaveClientFather(long clientId, long clientFatherId)
        {
            return _customDataAccess.SaveClientFather(clientId, clientFatherId);
        }

        public individuals RetrieveClientMotherByClientId(long clientId)
        {
            return _customDataAccess.RetrieveClientMotherByClientId(clientId);
        }

        public bool SaveClientMother(long clientId, long clientMotherId)
        {
            return _customDataAccess.SaveClientMother(clientId, clientMotherId);
        }

        public bool DoesClientHaveSystemUserAccount(long clientId)
        {
            return _customDataAccess.DoesClientHaveSystemUserAccount(clientId);
        }

        public bool CreateClientSystemUserAccount(long clientId, string password, string emailaddress, string host)
        {
            var ret = false;

            var registrationCode = GenerateRandomNumberString(100);

            var passwordHash = PasswordHasher.HashPassword(password);

            if (true == _customDataAccess.CreateClientSystemUserAccount(clientId, passwordHash, emailaddress, registrationCode))
            {
                if (true == SendThroughProduction(emailaddress, "Youth in View Account Registration", BuildRegistrationEmail(host, registrationCode)))
                {
                    ret = true;
                }
            }

            return ret;
        }

        public bool DeactivateClientSystemUserAccount(long clientId)
        {
            return _customDataAccess.DeactivateClientSystemUserAccount(clientId);
        }

        public List<displayprogram> RetrieveListOfProgramsClientIsEnlistedIn(long clientId)
        {
            return _customDataAccess.RetrieveListOfProgramsClientIsEnlistedIn(clientId);
        }

        public List<displayprogramevent> RetrieveListOfProgramEventsClientIsEnlistedIn(long clientId, long programId)
        {
            return _customDataAccess.RetrieveListOfProgramEventsClientIsEnlistedIn(clientId, programId);
        }

        public bool EnlistClientInProgram(long clientId, long programId)
        {
            return _customDataAccess.EnlistClientInProgram(clientId, programId);
        }

        public bool UnenlistClientInProgram(long clientId, long programId)
        {
            return _customDataAccess.UnenlistClientInProgram(clientId, programId);
        }

        public bool EnlistClientInProgramEvent(long clientId, long programEventId)
        {
            return _customDataAccess.EnlistClientInProgramEvent(clientId, programEventId);
        }

        public bool UnenlistClientInProgramEvent(long clientId, long programEventId)
        {
            return _customDataAccess.UnenlistClientInProgramEvent(clientId, programEventId);
        }

        public List<programdonorcommitments> RetrieveDonorAssignedClientRequests(long clientId)
        {
            return _customDataAccess.RetrieveDonorAssignedClientRequests(clientId);
        }

        public List<displaydonor> RetrieveAllDonors()
        {
            return _customDataAccess.RetrieveAllDonors();
        }

        public bool AddDonor(displaydonor displayDonor, string host)
        {
            var registrationCode = GenerateRandomNumberString(100);

            displayDonor.Password = PasswordHasher.HashPassword(displayDonor.Password);

            if (true == _customDataAccess.AddDonor(displayDonor, registrationCode))
            {
                if (true == SendThroughProduction(displayDonor.EmailAddress, "Youth in View Account Registration",
                        BuildRegistrationEmail(host, registrationCode)))
                {
                    return true;
                }
            }

            return false;
        }

        public bool ModifyDonor(displaydonor displayDonor)
        {
            return _customDataAccess.ModifyDonor(displayDonor);
        }

        public bool DeleteDonor(long donorId)
        {
            return _customDataAccess.DeleteDonor(donorId);
        }

        public displaydonor RetrieveDonorInformationByDonorID(long donorId)
        {
            return _customDataAccess.RetrieveDonorInformationByDonorID(donorId);
        }

        public bool SaveDonorInformation(displaydonor displayDonor)
        {
            return _customDataAccess.SaveDonorInformation(displayDonor);
        }

        public List<programs> RetrieveListOfProgramsDonorIsEnlistedIn(long donorId)
        {
            return _customDataAccess.RetrieveListOfProgramsDonorIsEnlistedIn(donorId);
        }        

        public List<clientrequests> RetrieveClientRequestsByProgramId(long programId, long clientId)
        {
            return _customDataAccess.RetrieveClientRequestsByProgramId(programId, clientId);
        }

        public bool SendInternalMessage(string host, messages message, List<messagerecipients> listMessageRecipients)
        {
            message.MessageSentDateTime = DateTime.Now;
            if (true == _customDataAccess.SendInternalMessage(message, listMessageRecipients))
            {
                SendEmailsAndTextsForMessageRecipients(host, listMessageRecipients);
                return true;
            }

            return false;
        }

        public List<displaymessage> RetrieveAllInboxMessages(long accountID, long accountTypeId)
        {
            return _customDataAccess.RetrieveAllInboxMessages(accountID, accountTypeId);
        }

        public List<displayprogram> RetrieveAllProgramsClientNotEnlistedIn(long clientId)
        {
            return _customDataAccess.RetrieveAllProgramsClientNotEnlistedIn(clientId);
        }

        public List<displayprogramevent> RetrieveAllProgramEventsClientNotEnlistedIn(long clientId, long programId)
        {
            return _customDataAccess.RetrieveAllProgramEventsClientNotEnlistedIn(clientId, programId);
        }

        public List<displayprogram> RetrieveAllProgramsDonorNotEnlistedIn(long donorId)
        {
            return _customDataAccess.RetrieveAllProgramsDonorNotEnlistedIn(donorId);
        }

        public displayaccount RetrieveDisplayAccountByLoggedInUserID(long loggedInUserID)
        {
            return _customDataAccess.RetrieveDisplayAccountByLoggedInUserID(loggedInUserID);
        }

        public List<displayhappyclientphotos> RetrieveHappyClientPhotosByProgramID(long clientId, long programId)
        {
            return _customDataAccess.RetrieveHappyClientPhotosByProgramID(clientId, programId);
        }

        public bool AddHappyClientPhoto(long clientId, long programId, byte[] photoData, string fileName, long fileSize)
        {
            return _customDataAccess.AddHappyClientPhoto(clientId, programId, photoData, fileName, fileSize);
        }

        public bool DeleteHappyClientPhoto(long happyClientPictureID)
        {
            return _customDataAccess.DeleteHappyClientPhoto(happyClientPictureID);
        }

        public bool DeactivateClient(long clientId)
        {
            return _customDataAccess.DeactivateClient(clientId);
        }

        public List<displayclientrequest> RetrieveAllNonCommittedClientRequests()
        {
            return _customDataAccess.RetrieveAllNonCommittedClientRequests();
        }

        public List<displayclientrequest> RetrieveAllCommittedClientRequestsByDonorId(long donorId)
        {
            return _customDataAccess.RetrieveAllCommittedClientRequestsByDonorId(donorId);
        }

        public bool DeactivateDonor(long donorId)
        {
            return _customDataAccess.DeactivateDonor(donorId);
        }

        public void SetMessageRecipientMessageAsRead(long messageRecipientId)
        {
            _customDataAccess.SetMessageRecipientMessageAsRead(messageRecipientId);
        }

        public List<displayavailablemessagerecipient> RetrieveAllAvailableMessageRecipientsWithExclusionList(
            List<displayavailablemessagerecipient> exclusionlist)
        {
           return _customDataAccess.RetrieveAllAvailableMessageRecipientsWithExclusionList(exclusionlist);
        }

        public displayavailablemessagerecipient RetrieveMessageRecipientByAccountIDAndAccountTypeID(
            long accountId, long accountTypeId)
        {
            return _customDataAccess.RetrieveMessageRecipientByAccountIDAndAccountTypeID(accountId, accountTypeId);
        }

        public displaynotificationsettings RetrieveNotificationSettingsByAccountIdAndAccountTypeId(long accountId,
            long accountTypeId)
        {
            return _customDataAccess.RetrieveNotificationSettingsByAccountIdAndAccountTypeId(accountId, accountTypeId);
        }

        public bool SaveNotificationSettingsByAccountIdAndAccountTypeId(long accountId, long accountTypeId,
            displaynotificationsettings notifysettings)
        {
            return _customDataAccess.SaveNotificationSettingsByAccountIdAndAccountTypeId(accountId, accountTypeId,
                notifysettings);
        }
        public List<displaychild> RetrieveAllChildrenByParentClientID(long parentClientId)
        {
            return _customDataAccess.RetrieveAllChildrenByParentClientID(parentClientId);
        }
        public List<programs> RetrieveAllProgramsChildIsEnlistedIn(long childClientId)
        {
            return _customDataAccess.RetrieveAllProgramsChildIsEnlistedIn(childClientId);
        }
        public List<clientrequests> RetrieveAllNonDonorCommittedClientRequestsByClientIdAndProgramId(long clientId, long programId)
        {
            return _customDataAccess.RetrieveAllNonDonorCommittedClientRequestsByClientIdAndProgramId(clientId, programId);
        }
        public List<clientrequests> RetrieveAllDonorCommittedClientRequestsByClientIdAndProgramId(long clientId, long programId)
        {
            return _customDataAccess.RetrieveAllDonorCommittedClientRequestsByClientIdAndProgramId(clientId, programId);
        }
        public List<displayclientrequest> RetrieveAllNonDonorCommittedClientRequestsByProgramID(long programId)
        {
            return _customDataAccess.RetrieveAllNonDonorCommittedClientRequestsByProgramID(programId);
        }
        public List<displayclientrequest> RetrieveAllclientRequestsCommittedToByDonorID(long donorId)
        {
            return _customDataAccess.RetrieveAllclientRequestsCommittedToByDonorID(donorId);
        }
        public string RetrieveWelcomeMessageByLoggedInUserID(long loggedInUserID)
        {
            return _customDataAccess.RetrieveWelcomeMessageByLoggedInUserID(loggedInUserID);
        }

    }
}
