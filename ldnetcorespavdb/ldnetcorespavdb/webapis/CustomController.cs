using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Dynamic;

using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using ldvdbclasslibrary;
using ldvdbdal;
using ldvdbbusinesslogic;

namespace ldnetcorespavdb.WebApis
{
    public class ProtocolReturnPacket
    {
        public bool succeeded { get; set; }
        public dynamic payload { get; set; }
    }

    [Produces("application/json")]
    [Route("api/Custom")]
    public class CustomController : Controller
    {
        // POST: api/Main
        [HttpPost]
        public string Post()
        {
            var protocolReturnPacket = new ProtocolReturnPacket();
            protocolReturnPacket.succeeded = false;

            try
            {
                var stream = new StreamReader(Request.Body);
                var body = stream.ReadToEnd();                

                var converter = new ExpandoObjectConverter();
                dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(body, converter);
                var expandoDict = (IDictionary<string, object>)obj;
                var protocol = expandoDict["protocol"];

                switch ((CustomControllerProtocols)protocol)
                {
                    case CustomControllerProtocols.RegisterAccountByRegistrationId:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));
                                protocolReturnPacket.succeeded = bl.RegisterUserAccountByRegistrationId(Convert.ToString(expandoDict["payload"]));
                            }
                        }
                        break;
                    case CustomControllerProtocols.CreateUserAccount:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));
                                var expandopayload = (IDictionary<string, object>)expandoDict["payload"];
                                var email = Convert.ToString(expandopayload["email"]);
                                var password = Convert.ToString(expandopayload["password"]);
                                var accountType = (AccountType)Convert.ToInt32(expandopayload["accounttype"]);
                                string host = HttpContext.Request.Host.Value;
                                protocolReturnPacket.succeeded = bl.CreateUserAccount(email, password, accountType, host);
                            }
                        }
                        break;
                    case CustomControllerProtocols.LoginSystemUser:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));
                                var expandopayload = (IDictionary<string, object>)expandoDict["payload"];
                                var email = Convert.ToString(expandopayload["email"]);
                                var password = Convert.ToString(expandopayload["password"]);
                                var ret = bl.LoginSystemUser(email, password);
                                if (null != ret)
                                {
                                    protocolReturnPacket.succeeded = true;
                                    protocolReturnPacket.payload = ret;
                                }
                                else
                                {
                                    protocolReturnPacket.succeeded = false;
                                }                                
                            }
                        }
                        break;
                    case CustomControllerProtocols.GetSystemUserAccountType:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));
                                var expandopayload = (IDictionary<string, object>)expandoDict["payload"];
                                var userid = Convert.ToInt64(expandopayload["userid"]);
                                var ret = bl.GetSystemUserAccountType(userid);
                                if (-1 != ret)
                                {
                                    protocolReturnPacket.succeeded = true;
                                    protocolReturnPacket.payload = ret;
                                }
                                else
                                {
                                    protocolReturnPacket.succeeded = false;
                                }
                            }
                        }
                        break;
                    case CustomControllerProtocols.SendPasswordResetEmail:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));
                                var expandopayload = (IDictionary<string, object>)expandoDict["payload"];
                                var email = Convert.ToString(expandopayload["email"]);
                                string host = HttpContext.Request.Host.Value;
                                var ret = bl.SendPasswordResetEmail(email, host);
                                if (-1 != ret)
                                {
                                    protocolReturnPacket.succeeded = true;
                                }
                                else
                                {
                                    protocolReturnPacket.succeeded = false;
                                }
                            }
                        }
                        break;
                    case CustomControllerProtocols.UpdateUserPassword:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));
                                var expandopayload = (IDictionary<string, object>)expandoDict["payload"];
                                var password = Convert.ToString(expandopayload["password"]);
                                var passwordresetcode = Convert.ToString(expandopayload["passwordresetcode"]);
                                string host = HttpContext.Request.Host.Value;
                                var ret = bl.UpdateUserPassword(passwordresetcode, password);
                                if (-1 != ret)
                                {
                                    protocolReturnPacket.succeeded = true;
                                }
                                else
                                {
                                    protocolReturnPacket.succeeded = false;
                                }
                            }
                        }
                        break;
                    case CustomControllerProtocols.CreateModuleView:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                moduleviewsManager manager = null;

                                manager = new moduleviewsManager(_context);

                                var newmoduleviews = new moduleviews();

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                newmoduleviews.Name = Convert.ToString(payloadexpandodict["name"]);
                                newmoduleviews.Occurred = DateTime.Now;
                                newmoduleviews.LoggedInSecurityUserID = Convert.ToInt64(payloadexpandodict["loggedinsecurityuserid"]);

                                var newobjectid = manager.Create(newmoduleviews);

                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveIndividualInformation:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var LoggedInSecurityUserID = Convert.ToInt64(payloadexpandodict["loggedinsecurityuserid"]);

                                var individualInfo = bl.RetrieveIndividualInformationBySystemUserID(LoggedInSecurityUserID);

                                protocolReturnPacket.payload = individualInfo;
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.SaveIndividualInformation:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var LoggedInSecurityUserID = Convert.ToInt64(payloadexpandodict["loggedinsecurityuserid"]);

                                var indinfo = new individuals();

                                indinfo.LastName = Convert.ToString(payloadexpandodict["LastName"]);
                                indinfo.MiddleName = Convert.ToString(payloadexpandodict["MiddleName"]);
                                indinfo.FirstName = Convert.ToString(payloadexpandodict["FirstName"]);
                                indinfo.FatherIndividualID = -1;
                                indinfo.MotherIndividualID = -1;
                                indinfo.Birthdate = Convert.ToDateTime(payloadexpandodict["Birthdate"]);
                                indinfo.MobilePhoneNumber = Convert.ToString(payloadexpandodict["MobilePhoneNumber"]);
                                indinfo.HomePhoneNumber = Convert.ToString(payloadexpandodict["HomePhoneNumber"]);
                                indinfo.AddressLine1 = Convert.ToString(payloadexpandodict["AddressLine1"]);
                                indinfo.AddressLine2 = Convert.ToString(payloadexpandodict["AddressLine2"]);
                                indinfo.City = Convert.ToString(payloadexpandodict["City"]);
                                indinfo.State = Convert.ToString(payloadexpandodict["State"]);
                                indinfo.Zip = Convert.ToString(payloadexpandodict["Zip"]);

                                protocolReturnPacket.succeeded = bl.SaveIndividualInformation(LoggedInSecurityUserID, indinfo);
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllPrograms:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var programs = bl.RetrieveAllPrograms();

                                protocolReturnPacket.payload = programs;
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveProgramEventsByProgram:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var programId = Convert.ToInt64(payloadexpandodict["programid"]);

                                var programevents = bl.RetrieveAllProgramEventsByProgramId(programId);

                                protocolReturnPacket.payload = programevents;
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllAdministrators:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var administrators = bl.RetrieveAllAdministrators();

                                protocolReturnPacket.payload = administrators;
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.AddAdministrator:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var displayAdministrator = new DisplayAdministrator();

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                displayAdministrator.LastName = Convert.ToString(payloadexpandodict["LastName"]);
                                displayAdministrator.FirstName = Convert.ToString(payloadexpandodict["FirstName"]);
                                displayAdministrator.PhoneNumber = Convert.ToString(payloadexpandodict["PhoneNumber"]);
                                displayAdministrator.EmailAddress = Convert.ToString(payloadexpandodict["EmailAddress"]);
                                displayAdministrator.Password = Convert.ToString(payloadexpandodict["Password"]);

                                string host = HttpContext.Request.Host.Value;
                                var ret = bl.AddAdministrator(displayAdministrator, host);

                                protocolReturnPacket.succeeded = ret;
                            }
                        }
                        break;
                    case CustomControllerProtocols.DeleteAdministrator:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var adminId = Convert.ToInt64(payloadexpandodict["adminid"]);

                                bl.DeleteAdministrator(adminId);

                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllClients:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var clients = bl.RetrieveAllClients();

                                protocolReturnPacket.payload = clients;
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.AddClient:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var displayClient = new displayclient();

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                displayClient.EmailAddress = Convert.ToString(payloadexpandodict["EmailAddress"]);
                                displayClient.LastName = Convert.ToString(payloadexpandodict["LastName"]);
                                displayClient.MiddleName = Convert.ToString(payloadexpandodict["MiddleName"]);
                                displayClient.FirstName = Convert.ToString(payloadexpandodict["FirstName"]);
                                displayClient.FatherIndividualID = Convert.ToInt64(payloadexpandodict["FatherIndividualID"]);
                                displayClient.MotherIndividualID = Convert.ToInt64(payloadexpandodict["MotherIndividualID"]);
                                displayClient.BirthDate = Convert.ToDateTime(payloadexpandodict["BirthDate"]);
                                displayClient.MobilePhoneNumber = Convert.ToString(payloadexpandodict["MobilePhoneNumber"]);
                                displayClient.HomePhoneNumber = Convert.ToString(payloadexpandodict["HomePhoneNumber"]);
                                displayClient.AddressLine1 = Convert.ToString(payloadexpandodict["AddressLine1"]);
                                displayClient.AddressLine2 = Convert.ToString(payloadexpandodict["AddressLine2"]);
                                displayClient.City = Convert.ToString(payloadexpandodict["City"]);
                                displayClient.State = Convert.ToString(payloadexpandodict["State"]);
                                displayClient.Zip = Convert.ToString(payloadexpandodict["Zip"]);

                                var ret = bl.AddClient(displayClient);

                                protocolReturnPacket.succeeded = ret;
                            }
                        }
                        break;
                    case CustomControllerProtocols.ModifyClient:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var displayClient = new displayclient();

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                displayClient.ClientID = Convert.ToInt64(payloadexpandodict["ClientID"]);
                                displayClient.EmailAddress = Convert.ToString(payloadexpandodict["EmailAddress"]);
                                displayClient.LastName = Convert.ToString(payloadexpandodict["LastName"]);
                                displayClient.MiddleName = Convert.ToString(payloadexpandodict["MiddleName"]);
                                displayClient.FirstName = Convert.ToString(payloadexpandodict["FirstName"]);
                                displayClient.FatherIndividualID = Convert.ToInt64(payloadexpandodict["FatherIndividualID"]);
                                displayClient.MotherIndividualID = Convert.ToInt64(payloadexpandodict["MotherIndividualID"]);
                                displayClient.BirthDate = Convert.ToDateTime(payloadexpandodict["BirthDate"]);
                                displayClient.MobilePhoneNumber = Convert.ToString(payloadexpandodict["MobilePhoneNumber"]);
                                displayClient.HomePhoneNumber = Convert.ToString(payloadexpandodict["HomePhoneNumber"]);
                                displayClient.AddressLine1 = Convert.ToString(payloadexpandodict["AddressLine1"]);
                                displayClient.AddressLine2 = Convert.ToString(payloadexpandodict["AddressLine2"]);
                                displayClient.City = Convert.ToString(payloadexpandodict["City"]);
                                displayClient.State = Convert.ToString(payloadexpandodict["State"]);
                                displayClient.Zip = Convert.ToString(payloadexpandodict["Zip"]);

                                var ret = bl.ModifyClient(displayClient);

                                protocolReturnPacket.succeeded = ret;
                            }
                        }
                        break;
                    case CustomControllerProtocols.DeleteClient:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var clientId = Convert.ToInt64(payloadexpandodict["clientid"]);

                                bl.DeleteClient(clientId);

                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveClientInformationByClientID:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var clientId = Convert.ToInt64(payloadexpandodict["clientid"]);

                                protocolReturnPacket.payload = bl.RetrieveClientInformationByClientID(clientId);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.SaveClientInformation:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var displayClient = new displayclient();

                                displayClient.ClientID = Convert.ToInt64(payloadexpandodict["ClientId"]);
                                displayClient.EmailAddress = Convert.ToString(payloadexpandodict["EmailAddress"]);
                                displayClient.LastName = Convert.ToString(payloadexpandodict["LastName"]);
                                displayClient.MiddleName = Convert.ToString(payloadexpandodict["MiddleName"]);
                                displayClient.FirstName = Convert.ToString(payloadexpandodict["FirstName"]);
                                displayClient.BirthDate = Convert.ToDateTime(payloadexpandodict["Birthdate"]);
                                displayClient.MobilePhoneNumber = Convert.ToString(payloadexpandodict["MobilePhoneNumber"]);
                                displayClient.HomePhoneNumber = Convert.ToString(payloadexpandodict["HomePhoneNumber"]);
                                displayClient.AddressLine1 = Convert.ToString(payloadexpandodict["AddressLine1"]);
                                displayClient.AddressLine2 = Convert.ToString(payloadexpandodict["AddressLine2"]);
                                displayClient.City = Convert.ToString(payloadexpandodict["City"]);
                                displayClient.State = Convert.ToString(payloadexpandodict["State"]);
                                displayClient.Zip = Convert.ToString(payloadexpandodict["Zip"]);

                                protocolReturnPacket.succeeded = bl.SaveClientInformation(displayClient); 
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveClientsNotInExclusionList:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var listClientExclusions = new List<long>();

                                listClientExclusions.Add(Convert.ToInt64(payloadexpandodict["clientexclusionid"]));

                                var clients = bl.RetrieveClientsNotInExclusionList(listClientExclusions);

                                protocolReturnPacket.payload = clients;
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveClientFatherByClientID:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                protocolReturnPacket.payload = bl.RetrieveClientFatherByClientID(Convert.ToInt64(payloadexpandodict["clientid"]));
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.SaveClientFather:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                protocolReturnPacket.succeeded = bl.SaveClientFather(Convert.ToInt64(payloadexpandodict["clientid"]),
                                    Convert.ToInt64(payloadexpandodict["clientfatherid"]));
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveClientMotherByClientId:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                protocolReturnPacket.payload = bl.RetrieveClientMotherByClientId(Convert.ToInt64(payloadexpandodict["clientid"]));
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.SaveClientMother:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                protocolReturnPacket.succeeded = bl.SaveClientMother(Convert.ToInt64(payloadexpandodict["clientid"]),
                                    Convert.ToInt64(payloadexpandodict["clientmotherid"]));
                            }
                        }
                        break;
                    case CustomControllerProtocols.DoesClientHaveSystemUserAccount:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                protocolReturnPacket.payload =
                                    bl.DoesClientHaveSystemUserAccount(Convert.ToInt64(payloadexpandodict["clientid"]));
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.CreateClientSystemUserAccount:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                string host = HttpContext.Request.Host.Value;

                                protocolReturnPacket.succeeded = bl.CreateClientSystemUserAccount(Convert.ToInt64(payloadexpandodict["clientid"]),
                                    Convert.ToString(payloadexpandodict["password"]), Convert.ToString(payloadexpandodict["emailaddress"]), host);
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveListOfProgramsClientIsEnlistedIn:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                protocolReturnPacket.payload =
                                    bl.RetrieveListOfProgramsClientIsEnlistedIn(
                                        Convert.ToInt64(payloadexpandodict["clientid"]));
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveListOfProgramEventsClientIsEnlistedIn:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                protocolReturnPacket.payload =
                                    bl.RetrieveListOfProgramEventsClientIsEnlistedIn(
                                        Convert.ToInt64(payloadexpandodict["clientid"]), Convert.ToInt64(payloadexpandodict["programid"]));
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.EnlistClientInProgram:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                protocolReturnPacket.succeeded = bl.EnlistClientInProgram(Convert.ToInt64(payloadexpandodict["clientid"]),
                                    Convert.ToInt64(payloadexpandodict["programid"]));
                            }
                        }
                        break;
                    case CustomControllerProtocols.UnenlistClientInProgram:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                protocolReturnPacket.succeeded = bl.UnenlistClientInProgram(Convert.ToInt64(payloadexpandodict["clientid"]),
                                    Convert.ToInt64(payloadexpandodict["programid"]));
                            }
                        }
                        break;
                    case CustomControllerProtocols.EnlistClientInProgramEvent:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                protocolReturnPacket.succeeded = bl.EnlistClientInProgramEvent(Convert.ToInt64(payloadexpandodict["clientid"]),
                                    Convert.ToInt64(payloadexpandodict["programeventid"]));
                            }
                        }
                        break;
                    case CustomControllerProtocols.UnenlistClientInProgramEvent:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                protocolReturnPacket.succeeded = bl.UnenlistClientInProgramEvent(Convert.ToInt64(payloadexpandodict["clientid"]),
                                    Convert.ToInt64(payloadexpandodict["programeventid"]));
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveDonorAssignedClientRequests:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllDonors:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                protocolReturnPacket.payload = bl.RetrieveAllDonors();
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.AddDonor:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var displayDonor = new displaydonor();

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                displayDonor.EmailAddress = Convert.ToString(payloadexpandodict["EmailAddress"]);
                                displayDonor.Password = Convert.ToString(payloadexpandodict["Password"]);
                                displayDonor.LastName = Convert.ToString(payloadexpandodict["LastName"]);
                                displayDonor.MiddleName = Convert.ToString(payloadexpandodict["MiddleName"]);
                                displayDonor.FirstName = Convert.ToString(payloadexpandodict["FirstName"]);
                                displayDonor.BirthDate = Convert.ToDateTime(payloadexpandodict["BirthDate"]);
                                displayDonor.MobilePhoneNumber = Convert.ToString(payloadexpandodict["MobilePhoneNumber"]);
                                displayDonor.HomePhoneNumber = Convert.ToString(payloadexpandodict["HomePhoneNumber"]);
                                displayDonor.AddressLine1 = Convert.ToString(payloadexpandodict["AddressLine1"]);
                                displayDonor.AddressLine2 = Convert.ToString(payloadexpandodict["AddressLine2"]);
                                displayDonor.City = Convert.ToString(payloadexpandodict["City"]);
                                displayDonor.State = Convert.ToString(payloadexpandodict["State"]);
                                displayDonor.Zip = Convert.ToString(payloadexpandodict["Zip"]);

                                string host = HttpContext.Request.Host.Value;
                                var ret = bl.AddDonor(displayDonor, host);

                                protocolReturnPacket.succeeded = ret;
                            }
                        }
                        break;
                    case CustomControllerProtocols.ModifyDonor:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var displayDonor = new displaydonor();

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                displayDonor.DonorID = Convert.ToInt64(payloadexpandodict["DonorID"]);
                                displayDonor.EmailAddress = Convert.ToString(payloadexpandodict["EmailAddress"]);
                                displayDonor.LastName = Convert.ToString(payloadexpandodict["LastName"]);
                                displayDonor.MiddleName = Convert.ToString(payloadexpandodict["MiddleName"]);
                                displayDonor.FirstName = Convert.ToString(payloadexpandodict["FirstName"]);
                                displayDonor.BirthDate = Convert.ToDateTime(payloadexpandodict["BirthDate"]);
                                displayDonor.MobilePhoneNumber = Convert.ToString(payloadexpandodict["MobilePhoneNumber"]);
                                displayDonor.HomePhoneNumber = Convert.ToString(payloadexpandodict["HomePhoneNumber"]);
                                displayDonor.AddressLine1 = Convert.ToString(payloadexpandodict["AddressLine1"]);
                                displayDonor.AddressLine2 = Convert.ToString(payloadexpandodict["AddressLine2"]);
                                displayDonor.City = Convert.ToString(payloadexpandodict["City"]);
                                displayDonor.State = Convert.ToString(payloadexpandodict["State"]);
                                displayDonor.Zip = Convert.ToString(payloadexpandodict["Zip"]);

                                var ret = bl.ModifyDonor(displayDonor);

                                protocolReturnPacket.succeeded = ret;
                            }
                        }
                        break;
                    case CustomControllerProtocols.DeleteDonor:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var donorId = Convert.ToInt64(payloadexpandodict["donorid"]);

                                var ret = bl.DeleteDonor(donorId);

                                protocolReturnPacket.succeeded = ret;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveListOfProgramsDonorIsEnlistedIn:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var donorId = Convert.ToInt64(payloadexpandodict["donorid"]);

                                protocolReturnPacket.payload = bl.RetrieveListOfProgramsDonorIsEnlistedIn(donorId);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveClientRequestsByProgramId:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                protocolReturnPacket.payload =
                                    bl.RetrieveClientRequestsByProgramId(Convert.ToInt64(payloadexpandodict["programid"]), Convert.ToInt64(payloadexpandodict["clientid"]));
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.SendInternalMessage:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var loggedInUserID = Convert.ToInt64(payloadexpandodict["loggedInUserID"]);

                                var adisplayaccount = bl.RetrieveDisplayAccountByLoggedInUserID(loggedInUserID);

                                var amessage = new messages();

                                amessage.FromAccountID = adisplayaccount.FromAccountID;
                                amessage.FromAccountTypeID = adisplayaccount.FromAccountTypeID;
                                amessage.Subject = Convert.ToString(payloadexpandodict["Subject"]);
                                amessage.Body = Convert.ToString(payloadexpandodict["Body"]);

                                var alistMessageRecipients = new List<messagerecipients>();

                                // offload message recipients here
                                var listMessageRecipients = (System.Collections.Generic.List<System.Object>)payloadexpandodict["messagerecipients"];

                                foreach (var messageRecipient in listMessageRecipients)
                                {
                                    var amessageRecipient = new messagerecipients();

                                    var origmessagerecipient = (IDictionary<string, object>)messageRecipient;

                                    amessageRecipient.AccountID = Convert.ToInt64(origmessagerecipient["AccountID"]);
                                    amessageRecipient.AccountTypeID = Convert.ToInt64(origmessagerecipient["AccountTypeID"]);

                                    alistMessageRecipients.Add(amessageRecipient);
                                }

                                string host = HttpContext.Request.Host.Value;

                                bl.SendInternalMessage(host, amessage, alistMessageRecipients);

                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllInboxMessages:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var accountId = Convert.ToInt64(payloadexpandodict["accountid"]);
                                var accountTypeId = Convert.ToInt64(payloadexpandodict["accounttypeid"]);

                                accountId = bl.RetrieveDisplayAccountByLoggedInUserID(accountId).FromAccountID;

                                protocolReturnPacket.payload = bl.RetrieveAllInboxMessages(accountId, accountTypeId);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveDonorInformationByDonorID:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var donorId = Convert.ToInt64(payloadexpandodict["donorid"]);

                                protocolReturnPacket.payload = bl.RetrieveDonorInformationByDonorID(donorId);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.SaveDonorInformation:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var displayDonor = new displaydonor();

                                displayDonor.DonorID = Convert.ToInt64(payloadexpandodict["DonorId"]);
                                displayDonor.LastName = Convert.ToString(payloadexpandodict["LastName"]);
                                displayDonor.MiddleName = Convert.ToString(payloadexpandodict["MiddleName"]);
                                displayDonor.FirstName = Convert.ToString(payloadexpandodict["FirstName"]);
                                displayDonor.BirthDate = Convert.ToDateTime(payloadexpandodict["Birthdate"]);
                                displayDonor.MobilePhoneNumber = Convert.ToString(payloadexpandodict["MobilePhoneNumber"]);
                                displayDonor.HomePhoneNumber = Convert.ToString(payloadexpandodict["HomePhoneNumber"]);
                                displayDonor.AddressLine1 = Convert.ToString(payloadexpandodict["AddressLine1"]);
                                displayDonor.AddressLine2 = Convert.ToString(payloadexpandodict["AddressLine2"]);
                                displayDonor.City = Convert.ToString(payloadexpandodict["City"]);
                                displayDonor.State = Convert.ToString(payloadexpandodict["State"]);
                                displayDonor.Zip = Convert.ToString(payloadexpandodict["Zip"]);

                                protocolReturnPacket.succeeded = bl.SaveDonorInformation(displayDonor); 
                            }
                        }
                        break;
                    case CustomControllerProtocols.DeactivateClientSystemUserAccount:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var clientid = Convert.ToInt64(payloadexpandodict["clientid"]);

                                protocolReturnPacket.payload = bl.DeactivateClientSystemUserAccount(clientid);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveClientProgramsNotEnlistedIn:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var clientid = Convert.ToInt64(payloadexpandodict["clientid"]);

                                protocolReturnPacket.payload = bl.RetrieveAllProgramsClientNotEnlistedIn(clientid);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveClientProgramEventsNotEnlistedIn:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var clientid = Convert.ToInt64(payloadexpandodict["clientid"]);
                                var programid = Convert.ToInt64(payloadexpandodict["programid"]);

                                protocolReturnPacket.payload = bl.RetrieveAllProgramEventsClientNotEnlistedIn(clientid, programid);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveDonorProgramsNotEnlistedIn:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var donorid = Convert.ToInt64(payloadexpandodict["donorid"]);

                                protocolReturnPacket.payload = bl.RetrieveAllProgramsDonorNotEnlistedIn(donorid);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveHappyClientPhotos:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var clientid = Convert.ToInt64(payloadexpandodict["clientid"]);
                                var programid = Convert.ToInt64(payloadexpandodict["programid"]);

                                protocolReturnPacket.payload = bl.RetrieveHappyClientPhotosByProgramID(clientid, programid);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.AddHappyClientPhoto:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var clientid = Convert.ToInt64(payloadexpandodict["clientid"]);
                                var programid = Convert.ToInt64(payloadexpandodict["programid"]);
                                var photoData = Convert.ToString(payloadexpandodict["photodata"]);
                                var fileName = Convert.ToString(payloadexpandodict["filename"]);
                                var fileSize = Convert.ToInt64(payloadexpandodict["filesize"]);

                                protocolReturnPacket.payload = bl.AddHappyClientPhoto(clientid, programid, Convert.FromBase64String(photoData), fileName, fileSize);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.DeleteHappyClientPhoto:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var happyclientphotoid = Convert.ToInt64(payloadexpandodict["happyclientphotoid"]);

                                protocolReturnPacket.payload = bl.DeleteHappyClientPhoto(happyclientphotoid);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.DeactivateClient:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var clientid = Convert.ToInt64(payloadexpandodict["clientid"]);

                                protocolReturnPacket.payload = bl.DeactivateClient(clientid);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllNonCommittedClientRequests:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                protocolReturnPacket.payload = bl.RetrieveAllNonCommittedClientRequests();
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllCommittedClientRequestsByDonorId:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var donorid = Convert.ToInt64(payloadexpandodict["donorid"]);

                                protocolReturnPacket.payload = bl.RetrieveAllCommittedClientRequestsByDonorId(donorid);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.DeactivateDonor:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var donorid = Convert.ToInt64(payloadexpandodict["donorid"]);

                                protocolReturnPacket.payload = bl.DeactivateDonor(donorid);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.SetMessageRecipientMessageRead:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var messageRecipientId = Convert.ToInt64(payloadexpandodict["messagerecipientid"]);

                                bl.SetMessageRecipientMessageAsRead(messageRecipientId);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllAvailableMessageRecipientsWithExclusionList:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var alistdisplayavailablemessagerecipient = new List<displayavailablemessagerecipient>();

                                // offload message recipients here
                                var listMessageRecipients = (System.Collections.Generic.List<System.Object>)payloadexpandodict["messagerecipients"];

                                foreach (var messageRecipient in listMessageRecipients)
                                {
                                    var adisplayavailablemessagerecipient = new displayavailablemessagerecipient();

                                    var origmessagerecipient = (IDictionary<string, object>)messageRecipient;

                                    adisplayavailablemessagerecipient.AccountID = Convert.ToInt64(origmessagerecipient["AccountID"]);
                                    adisplayavailablemessagerecipient.AccountTypeID = Convert.ToInt64(origmessagerecipient["AccountTypeID"]);

                                    alistdisplayavailablemessagerecipient.Add(adisplayavailablemessagerecipient);
                                }

                                protocolReturnPacket.payload =
                                    bl.RetrieveAllAvailableMessageRecipientsWithExclusionList(
                                        alistdisplayavailablemessagerecipient);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveMessageRecipientByAccountIDAndAccountTypeID:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var accountId = Convert.ToInt64(payloadexpandodict["accountid"]);
                                var accountTypeId = Convert.ToInt64(payloadexpandodict["accounttypeid"]);

                                protocolReturnPacket.payload =
                                    bl.RetrieveMessageRecipientByAccountIDAndAccountTypeID(
                                        accountId, accountTypeId);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveNotificationSettingsByAccountIdAndAccountTypeId:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var accountId = Convert.ToInt64(payloadexpandodict["accountid"]);
                                var accountTypeId = Convert.ToInt64(payloadexpandodict["accounttypeid"]);

                                accountId = bl.RetrieveDisplayAccountByLoggedInUserID(accountId).FromAccountID;

                                protocolReturnPacket.payload =
                                    bl.RetrieveNotificationSettingsByAccountIdAndAccountTypeId(
                                        accountId, accountTypeId);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.SaveNotificationSettingsByAccountIdAndAccountTypeId:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var accountId = Convert.ToInt64(payloadexpandodict["accountid"]);
                                var accountTypeId = Convert.ToInt64(payloadexpandodict["accounttypeid"]);
                                var enableEmailMessages = Convert.ToInt64(payloadexpandodict["enableemailmessages"]);
                                var enableSmsMessages = Convert.ToInt64(payloadexpandodict["enablesmsmessages"]);

                                accountId = bl.RetrieveDisplayAccountByLoggedInUserID(accountId).FromAccountID;

                                protocolReturnPacket.payload =
                                    bl.SaveNotificationSettingsByAccountIdAndAccountTypeId(
                                        accountId, accountTypeId, new displaynotificationsettings()
                                        {
                                            EmailMessagesEnabled = enableEmailMessages == 1 ? true : false,
                                            SmsMessagesEnabled = enableSmsMessages == 1 ? true : false
                                        });
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllChildrenByParentClientID:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var clientId = Convert.ToInt64(payloadexpandodict["clientid"]);

                                clientId = bl.RetrieveDisplayAccountByLoggedInUserID(clientId).FromAccountID;

                                protocolReturnPacket.payload =
                                    bl.RetrieveAllChildrenByParentClientID(clientId);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllProgramsChildIsEnlistedIn:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var clientId = Convert.ToInt64(payloadexpandodict["clientid"]);

                                protocolReturnPacket.payload =
                                    bl.RetrieveAllProgramsChildIsEnlistedIn(clientId);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllNonDonorCommittedClientRequestsByClientIdAndProgramId:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var clientId = Convert.ToInt64(payloadexpandodict["clientid"]);
                                var programId = Convert.ToInt64(payloadexpandodict["programid"]);

                                protocolReturnPacket.payload =
                                    bl.RetrieveAllNonDonorCommittedClientRequestsByClientIdAndProgramId(clientId, programId);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllDonorCommittedClientRequestsByClientIdAndProgramId:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var clientId = Convert.ToInt64(payloadexpandodict["clientid"]);
                                var programId = Convert.ToInt64(payloadexpandodict["programid"]);

                                protocolReturnPacket.payload =
                                    bl.RetrieveAllDonorCommittedClientRequestsByClientIdAndProgramId(clientId, programId);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllNonDonorCommittedClientRequestsByProgramID:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var programId = Convert.ToInt64(payloadexpandodict["programid"]);

                                protocolReturnPacket.payload =
                                    bl.RetrieveAllNonDonorCommittedClientRequestsByProgramID(programId);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveAllclientRequestsCommittedToByDonorID:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var donorid = Convert.ToInt64(payloadexpandodict["donorid"]);

                                donorid = bl.RetrieveDisplayAccountByLoggedInUserID(donorid).FromAccountID;

                                protocolReturnPacket.payload =
                                    bl.RetrieveAllclientRequestsCommittedToByDonorID(donorid);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveDisplayAccountByLoggedInUserID:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var loggedinuserid = Convert.ToInt64(payloadexpandodict["loggedinuserid"]);

                                loggedinuserid = bl.RetrieveDisplayAccountByLoggedInUserID(loggedinuserid).FromAccountID;

                                protocolReturnPacket.payload = loggedinuserid;
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;
                    case CustomControllerProtocols.RetrieveWelcomeMessageByLoggedInUserID:
                        {
                            using (var _context = new DbContext(new DbConnectionFactory("DefaultConnection")))
                            {
                                CustomBusinessLogic bl = new CustomBusinessLogic((ICustomDataAccess)new CustomDataAccess(_context));

                                var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                                var loggedinuserid = Convert.ToInt64(payloadexpandodict["loggedinuserid"]);

                                protocolReturnPacket.payload = bl.RetrieveWelcomeMessageByLoggedInUserID(loggedinuserid);
                                protocolReturnPacket.succeeded = true;
                            }
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                // might want to do something with this in the future.
            }            

            return JsonConvert.SerializeObject(protocolReturnPacket);
        }
    }
}