using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ldvdbclasslibrary;


namespace ldvdbbusinesslogic
{
    public interface ICustomBusinessLogic
    {
        void SetConfiguration(AppConfiguration appconfiguration);

        bool RegisterUserAccountByRegistrationId(string registrationId);
        string GenerateRandomNumberString(int countCharacters);
        bool CreateUserAccount(string email, string password, AccountType accountType, string host);
        displaylogin LoginSystemUser(string email, string password);
        long GetSystemUserAccountType(long userId);
        long SendPasswordResetEmail(string email, string host);
        long UpdateUserPassword(string passwordResetCode, string password);
        individuals RetrieveIndividualInformationBySystemUserID(long systemUserID);
        bool SaveIndividualInformation(long systemUserID, individuals individualInformation);
        List<programs> RetrieveAllPrograms();
        List<programevents> RetrieveAllProgramEventsByProgramId(long programId);
        List<DisplayAdministrator> RetrieveAllAdministrators();
        bool AddAdministrator(DisplayAdministrator displayAdministrator, string host);
        void DeleteAdministrator(long adminId);
        List<displayclient> RetrieveAllClients();
        bool AddClient(displayclient displayClient);
        bool ModifyClient(displayclient displayClient);
        void DeleteClient(long clientId);
        displayclient RetrieveClientInformationByClientID(long ClientId);
        bool SaveClientInformation(displayclient displayClient);
        List<displayclient> RetrieveClientsNotInExclusionList(List<long> clientIdExclusionList);
        individuals RetrieveClientFatherByClientID(long clientId);
        bool SaveClientFather(long clientId, long clientFatherId);
        individuals RetrieveClientMotherByClientId(long clientId);
        bool SaveClientMother(long clientId, long clientMotherId);
        bool DoesClientHaveSystemUserAccount(long clientId);
        bool CreateClientSystemUserAccount(long clientId, string password, string emailaddress, string host);
        bool DeactivateClientSystemUserAccount(long clientId);
        List<displayprogram> RetrieveListOfProgramsClientIsEnlistedIn(long clientId);
        List<displayprogramevent> RetrieveListOfProgramEventsClientIsEnlistedIn(long clientId, long programId);
        bool EnlistClientInProgram(long clientId, long programId);
        bool UnenlistClientInProgram(long clientId, long programId);
        bool EnlistClientInProgramEvent(long clientId, long programEventId);
        bool UnenlistClientInProgramEvent(long clientId, long programEventId);
        List<programdonorcommitments> RetrieveDonorAssignedClientRequests(long clientId);
        List<displaydonor> RetrieveAllDonors();
        bool AddDonor(displaydonor displayDonor, string host);
        bool ModifyDonor(displaydonor displayDonor);
        bool DeleteDonor(long donorId);
        displaydonor RetrieveDonorInformationByDonorID(long DonorId);
        bool SaveDonorInformation(displaydonor displayDonor);
        List<programs> RetrieveListOfProgramsDonorIsEnlistedIn(long donorId);        
        List<clientrequests> RetrieveClientRequestsByProgramId(long programId, long clientId);
        bool SendInternalMessage(string host, messages message, List<messagerecipients> listMessageRecipients);
        List<displaymessage> RetrieveAllInboxMessages(long accountID, long accountTypeId);
        List<displayprogram> RetrieveAllProgramsClientNotEnlistedIn(long clientId);
        List<displayprogramevent> RetrieveAllProgramEventsClientNotEnlistedIn(long clientId, long programId);
        List<displayprogram> RetrieveAllProgramsDonorNotEnlistedIn(long donorId);
        displayaccount RetrieveDisplayAccountByLoggedInUserID(long loggedInUserID);
        List<displayhappyclientphotos> RetrieveHappyClientPhotosByProgramID(long clientId, long programId);
        bool AddHappyClientPhoto(long clientId, long programId, byte[] photoData, string fileName, long fileSize);
        bool DeleteHappyClientPhoto(long happyClientPictureID);
        bool DeactivateClient(long clientId);
        List<displayclientrequest> RetrieveAllNonCommittedClientRequests();
        List<displayclientrequest> RetrieveAllCommittedClientRequestsByDonorId(long donorId);
        bool DeactivateDonor(long donorId);
        void SetMessageRecipientMessageAsRead(long messageRecipientId);
        List<displayavailablemessagerecipient> RetrieveAllAvailableMessageRecipientsWithExclusionList(
            List<displayavailablemessagerecipient> exclusionlist);
        displayavailablemessagerecipient RetrieveMessageRecipientByAccountIDAndAccountTypeID(
            long accountId, long accountTypeId);
        displaynotificationsettings RetrieveNotificationSettingsByAccountIdAndAccountTypeId(long accountId,
            long accountTypeId);
        bool SaveNotificationSettingsByAccountIdAndAccountTypeId(long accountId, long accountTypeId,
            displaynotificationsettings notifysettings);
        List<displaychild> RetrieveAllChildrenByParentClientID(long parentClientId);
        List<programs> RetrieveAllProgramsChildIsEnlistedIn(long childClientId);
        List<clientrequests> RetrieveAllNonDonorCommittedClientRequestsByClientIdAndProgramId(long clientId, long programId);
        List<clientrequests> RetrieveAllDonorCommittedClientRequestsByClientIdAndProgramId(long clientId, long programId);
        List<displayclientrequest> RetrieveAllNonDonorCommittedClientRequestsByProgramID(long programId);
        List<displayclientrequest> RetrieveAllclientRequestsCommittedToByDonorID(long donorId);
        string RetrieveWelcomeMessageByLoggedInUserID(long loggedInUserID);
    }
}
