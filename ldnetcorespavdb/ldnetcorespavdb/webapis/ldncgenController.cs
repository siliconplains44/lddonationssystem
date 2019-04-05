using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Microsoft.Extensions.Caching.Memory; 
using System.IO; 

using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using ldvdbclasslibrary;
using ldvdbdal;
using ldvdbbusinesslogic; 

namespace netcoreldspaframework.WebApis
{
    public class ProtocolReturnPacket
    {
        public bool succeeded { get; set; }
        public dynamic payload { get; set; }
    }

    [Produces("application/json")]
    [Route("api/Main")]
    public class MainController : Controller
    {
        private IMemoryCache cache;

        public MainController(IMemoryCache cache)
        {
            this.cache = cache;
        }

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

                switch ((ControllerProtocols)protocol)
                {
                    case ControllerProtocols.StartTransaction: // start tran
                        {
                            var transactionId = Guid.NewGuid().ToString();
                            var _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                            var _uow = _context.CreateUnitOfWork();
                            cache.Set<DbContext>("dbcontext-" + transactionId, _context);
                            cache.Set<IUnitOfWork>("uow-" + transactionId, _uow);
                            protocolReturnPacket.payload = transactionId;
                            protocolReturnPacket.succeeded = true;
                        }
                        break;
                    case ControllerProtocols.CommittTransaction: // commit tran
                        {
                            string transactionId = obj.payload;
                            var _uow = (IUnitOfWork)cache.Get("uow-" + transactionId);
                            _uow.SaveChanges();
                            _uow.Dispose();
                            cache.Remove("dbcontext-" + transactionId);
                            cache.Remove("uow-" + transactionId);
                            protocolReturnPacket.succeeded = true;
                        }
                        break;
                    case ControllerProtocols.RollbackTransaction: // rollback tran
                        {
                            string transactionId = obj.payload;
                            var _uow = (IUnitOfWork)cache.Get("uow-" + transactionId);
                            _uow.Dispose();
                            cache.Remove("dbcontext-" + transactionId);
                            cache.Remove("uow-" + transactionId);
                            protocolReturnPacket.succeeded = true;
                        }
                        break;
                    case ControllerProtocols.Createaccounttypes:
                        {
                            var transactionid = expandoDict["transactionid"];
                            accounttypesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new accounttypesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new accounttypesManager(_context);
                            }

                            var newaccounttypes = new accounttypes();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newaccounttypes.Name = Convert.ToString(payloadexpandodict["Name"]);

                            var newobjectid = manager.Create(newaccounttypes);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updateaccounttypes:
                        {
                            var transactionid = expandoDict["transactionid"];
                            accounttypesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new accounttypesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new accounttypesManager(_context);
                            }

                            var existingaccounttypes = new accounttypes();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingaccounttypes.AccountTypeID = Convert.ToInt64(payloadexpandodict["AccountTypeID"]);
                            existingaccounttypes.Name = Convert.ToString(payloadexpandodict["Name"]);

                            var rowUpdateCount = manager.Update(existingaccounttypes);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deleteaccounttypes:
                        {
                            var transactionid = expandoDict["transactionid"];
                            accounttypesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new accounttypesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new accounttypesManager(_context);
                            }

                            var existingaccounttypes = new accounttypes();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingaccounttypes.AccountTypeID = Convert.ToInt64(payloadexpandodict["AccountTypeID"]);

                            var rowUpdateCount = manager.Delete(existingaccounttypes);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDaccounttypes:
                        {
                            var transactionid = expandoDict["transactionid"];
                            accounttypesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new accounttypesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new accounttypesManager(_context);
                            }

                            var existingaccounttypesid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingaccounttypesid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClauseaccounttypes:
                        {
                            var transactionid = expandoDict["transactionid"];
                            accounttypesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new accounttypesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new accounttypesManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClauseaccounttypes(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createadmins:
                        {
                            var transactionid = expandoDict["transactionid"];
                            adminsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new adminsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new adminsManager(_context);
                            }

                            var newadmins = new admins();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newadmins.UserID = Convert.ToInt64(payloadexpandodict["UserID"]);
                            newadmins.IndividualID = Convert.ToInt64(payloadexpandodict["IndividualID"]);
                            newadmins.IsDeleted = Convert.ToInt64(payloadexpandodict["IsDeleted"]);

                            var newobjectid = manager.Create(newadmins);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updateadmins:
                        {
                            var transactionid = expandoDict["transactionid"];
                            adminsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new adminsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new adminsManager(_context);
                            }

                            var existingadmins = new admins();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingadmins.AdminID = Convert.ToInt64(payloadexpandodict["AdminID"]);
                            existingadmins.UserID = Convert.ToInt64(payloadexpandodict["UserID"]);
                            existingadmins.IndividualID = Convert.ToInt64(payloadexpandodict["IndividualID"]);
                            existingadmins.IsDeleted = Convert.ToInt64(payloadexpandodict["IsDeleted"]);

                            var rowUpdateCount = manager.Update(existingadmins);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deleteadmins:
                        {
                            var transactionid = expandoDict["transactionid"];
                            adminsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new adminsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new adminsManager(_context);
                            }

                            var existingadmins = new admins();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingadmins.AdminID = Convert.ToInt64(payloadexpandodict["AdminID"]);

                            var rowUpdateCount = manager.Delete(existingadmins);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDadmins:
                        {
                            var transactionid = expandoDict["transactionid"];
                            adminsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new adminsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new adminsManager(_context);
                            }

                            var existingadminsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingadminsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClauseadmins:
                        {
                            var transactionid = expandoDict["transactionid"];
                            adminsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new adminsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new adminsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClauseadmins(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createclientprogramenlistments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientprogramenlistmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientprogramenlistmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientprogramenlistmentsManager(_context);
                            }

                            var newclientprogramenlistments = new clientprogramenlistments();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newclientprogramenlistments.ClientID = Convert.ToInt64(payloadexpandodict["ClientID"]);
                            newclientprogramenlistments.ProgramID = Convert.ToInt64(payloadexpandodict["ProgramID"]);

                            var newobjectid = manager.Create(newclientprogramenlistments);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updateclientprogramenlistments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientprogramenlistmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientprogramenlistmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientprogramenlistmentsManager(_context);
                            }

                            var existingclientprogramenlistments = new clientprogramenlistments();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingclientprogramenlistments.RecipientProgramEnlistmentID = Convert.ToInt64(payloadexpandodict["RecipientProgramEnlistmentID"]);
                            existingclientprogramenlistments.ClientID = Convert.ToInt64(payloadexpandodict["ClientID"]);
                            existingclientprogramenlistments.ProgramID = Convert.ToInt64(payloadexpandodict["ProgramID"]);

                            var rowUpdateCount = manager.Update(existingclientprogramenlistments);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deleteclientprogramenlistments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientprogramenlistmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientprogramenlistmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientprogramenlistmentsManager(_context);
                            }

                            var existingclientprogramenlistments = new clientprogramenlistments();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingclientprogramenlistments.RecipientProgramEnlistmentID = Convert.ToInt64(payloadexpandodict["RecipientProgramEnlistmentID"]);

                            var rowUpdateCount = manager.Delete(existingclientprogramenlistments);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDclientprogramenlistments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientprogramenlistmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientprogramenlistmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientprogramenlistmentsManager(_context);
                            }

                            var existingclientprogramenlistmentsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingclientprogramenlistmentsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClauseclientprogramenlistments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientprogramenlistmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientprogramenlistmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientprogramenlistmentsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClauseclientprogramenlistments(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createclientprogrameventenlistments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientprogrameventenlistmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientprogrameventenlistmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientprogrameventenlistmentsManager(_context);
                            }

                            var newclientprogrameventenlistments = new clientprogrameventenlistments();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newclientprogrameventenlistments.ClientID = Convert.ToInt64(payloadexpandodict["ClientID"]);
                            newclientprogrameventenlistments.ProgramEventID = Convert.ToInt64(payloadexpandodict["ProgramEventID"]);

                            var newobjectid = manager.Create(newclientprogrameventenlistments);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updateclientprogrameventenlistments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientprogrameventenlistmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientprogrameventenlistmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientprogrameventenlistmentsManager(_context);
                            }

                            var existingclientprogrameventenlistments = new clientprogrameventenlistments();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingclientprogrameventenlistments.ClientProgramEventEnlistementID = Convert.ToInt64(payloadexpandodict["ClientProgramEventEnlistementID"]);
                            existingclientprogrameventenlistments.ClientID = Convert.ToInt64(payloadexpandodict["ClientID"]);
                            existingclientprogrameventenlistments.ProgramEventID = Convert.ToInt64(payloadexpandodict["ProgramEventID"]);

                            var rowUpdateCount = manager.Update(existingclientprogrameventenlistments);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deleteclientprogrameventenlistments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientprogrameventenlistmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientprogrameventenlistmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientprogrameventenlistmentsManager(_context);
                            }

                            var existingclientprogrameventenlistments = new clientprogrameventenlistments();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingclientprogrameventenlistments.ClientProgramEventEnlistementID = Convert.ToInt64(payloadexpandodict["ClientProgramEventEnlistementID"]);

                            var rowUpdateCount = manager.Delete(existingclientprogrameventenlistments);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDclientprogrameventenlistments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientprogrameventenlistmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientprogrameventenlistmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientprogrameventenlistmentsManager(_context);
                            }

                            var existingclientprogrameventenlistmentsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingclientprogrameventenlistmentsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClauseclientprogrameventenlistments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientprogrameventenlistmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientprogrameventenlistmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientprogrameventenlistmentsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClauseclientprogrameventenlistments(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createclientrequests:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientrequestsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientrequestsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientrequestsManager(_context);
                            }

                            var newclientrequests = new clientrequests();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newclientrequests.ClientID = Convert.ToInt64(payloadexpandodict["ClientID"]);
                            newclientrequests.RequestInformation = Convert.ToString(payloadexpandodict["RequestInformation"]);
                            newclientrequests.ProgramID = Convert.ToInt64(payloadexpandodict["ProgramID"]);

                            var newobjectid = manager.Create(newclientrequests);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updateclientrequests:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientrequestsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientrequestsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientrequestsManager(_context);
                            }

                            var existingclientrequests = new clientrequests();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingclientrequests.ClientRequestID = Convert.ToInt64(payloadexpandodict["ClientRequestID"]);
                            existingclientrequests.ClientID = Convert.ToInt64(payloadexpandodict["ClientID"]);
                            existingclientrequests.RequestInformation = Convert.ToString(payloadexpandodict["RequestInformation"]);
                            existingclientrequests.ProgramID = Convert.ToInt64(payloadexpandodict["ProgramID"]);

                            var rowUpdateCount = manager.Update(existingclientrequests);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deleteclientrequests:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientrequestsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientrequestsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientrequestsManager(_context);
                            }

                            var existingclientrequests = new clientrequests();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingclientrequests.ClientRequestID = Convert.ToInt64(payloadexpandodict["ClientRequestID"]);

                            var rowUpdateCount = manager.Delete(existingclientrequests);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDclientrequests:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientrequestsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientrequestsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientrequestsManager(_context);
                            }

                            var existingclientrequestsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingclientrequestsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClauseclientrequests:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientrequestsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientrequestsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientrequestsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClauseclientrequests(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createclients:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientsManager(_context);
                            }

                            var newclients = new clients();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newclients.UserID = Convert.ToInt64(payloadexpandodict["UserID"]);
                            newclients.IndividualID = Convert.ToInt64(payloadexpandodict["IndividualID"]);
                            newclients.IsDeleted = Convert.ToInt64(payloadexpandodict["IsDeleted"]);

                            var newobjectid = manager.Create(newclients);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updateclients:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientsManager(_context);
                            }

                            var existingclients = new clients();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingclients.ClientID = Convert.ToInt64(payloadexpandodict["ClientID"]);
                            existingclients.UserID = Convert.ToInt64(payloadexpandodict["UserID"]);
                            existingclients.IndividualID = Convert.ToInt64(payloadexpandodict["IndividualID"]);
                            existingclients.IsDeleted = Convert.ToInt64(payloadexpandodict["IsDeleted"]);

                            var rowUpdateCount = manager.Update(existingclients);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deleteclients:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientsManager(_context);
                            }

                            var existingclients = new clients();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingclients.ClientID = Convert.ToInt64(payloadexpandodict["ClientID"]);

                            var rowUpdateCount = manager.Delete(existingclients);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDclients:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientsManager(_context);
                            }

                            var existingclientsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingclientsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClauseclients:
                        {
                            var transactionid = expandoDict["transactionid"];
                            clientsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new clientsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new clientsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClauseclients(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createdonorfundscommitments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            donorfundscommitmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new donorfundscommitmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new donorfundscommitmentsManager(_context);
                            }

                            var newdonorfundscommitments = new donorfundscommitments();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newdonorfundscommitments.Occured = Convert.ToDateTime(payloadexpandodict["Occured"]);
                            newdonorfundscommitments.DonorID = Convert.ToInt64(payloadexpandodict["DonorID"]);
                            newdonorfundscommitments.Amount = Convert.ToDecimal(payloadexpandodict["Amount"]);
                            newdonorfundscommitments.Received = Convert.ToDateTime(payloadexpandodict["Received"]);

                            var newobjectid = manager.Create(newdonorfundscommitments);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updatedonorfundscommitments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            donorfundscommitmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new donorfundscommitmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new donorfundscommitmentsManager(_context);
                            }

                            var existingdonorfundscommitments = new donorfundscommitments();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingdonorfundscommitments.DonorFundsCommitmentID = Convert.ToInt64(payloadexpandodict["DonorFundsCommitmentID"]);
                            existingdonorfundscommitments.Occured = Convert.ToDateTime(payloadexpandodict["Occured"]);
                            existingdonorfundscommitments.DonorID = Convert.ToInt64(payloadexpandodict["DonorID"]);
                            existingdonorfundscommitments.Amount = Convert.ToDecimal(payloadexpandodict["Amount"]);
                            existingdonorfundscommitments.Received = Convert.ToDateTime(payloadexpandodict["Received"]);

                            var rowUpdateCount = manager.Update(existingdonorfundscommitments);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deletedonorfundscommitments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            donorfundscommitmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new donorfundscommitmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new donorfundscommitmentsManager(_context);
                            }

                            var existingdonorfundscommitments = new donorfundscommitments();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingdonorfundscommitments.DonorFundsCommitmentID = Convert.ToInt64(payloadexpandodict["DonorFundsCommitmentID"]);

                            var rowUpdateCount = manager.Delete(existingdonorfundscommitments);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDdonorfundscommitments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            donorfundscommitmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new donorfundscommitmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new donorfundscommitmentsManager(_context);
                            }

                            var existingdonorfundscommitmentsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingdonorfundscommitmentsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClausedonorfundscommitments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            donorfundscommitmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new donorfundscommitmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new donorfundscommitmentsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClausedonorfundscommitments(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createdonors:
                        {
                            var transactionid = expandoDict["transactionid"];
                            donorsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new donorsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new donorsManager(_context);
                            }

                            var newdonors = new donors();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newdonors.UserID = Convert.ToInt64(payloadexpandodict["UserID"]);
                            newdonors.IndividualID = Convert.ToInt64(payloadexpandodict["IndividualID"]);
                            newdonors.IsDeleted = Convert.ToInt64(payloadexpandodict["IsDeleted"]);

                            var newobjectid = manager.Create(newdonors);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updatedonors:
                        {
                            var transactionid = expandoDict["transactionid"];
                            donorsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new donorsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new donorsManager(_context);
                            }

                            var existingdonors = new donors();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingdonors.DonorID = Convert.ToInt64(payloadexpandodict["DonorID"]);
                            existingdonors.UserID = Convert.ToInt64(payloadexpandodict["UserID"]);
                            existingdonors.IndividualID = Convert.ToInt64(payloadexpandodict["IndividualID"]);
                            existingdonors.IsDeleted = Convert.ToInt64(payloadexpandodict["IsDeleted"]);

                            var rowUpdateCount = manager.Update(existingdonors);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deletedonors:
                        {
                            var transactionid = expandoDict["transactionid"];
                            donorsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new donorsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new donorsManager(_context);
                            }

                            var existingdonors = new donors();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingdonors.DonorID = Convert.ToInt64(payloadexpandodict["DonorID"]);

                            var rowUpdateCount = manager.Delete(existingdonors);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDdonors:
                        {
                            var transactionid = expandoDict["transactionid"];
                            donorsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new donorsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new donorsManager(_context);
                            }

                            var existingdonorsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingdonorsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClausedonors:
                        {
                            var transactionid = expandoDict["transactionid"];
                            donorsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new donorsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new donorsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClausedonors(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createfileuploads:
                        {
                            var transactionid = expandoDict["transactionid"];
                            fileuploadsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new fileuploadsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new fileuploadsManager(_context);
                            }

                            var newfileuploads = new fileuploads();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newfileuploads.Filename = Convert.ToString(payloadexpandodict["Filename"]);
                            newfileuploads.Size = Convert.ToInt64(payloadexpandodict["Size"]);
                            newfileuploads.Created = Convert.ToDateTime(payloadexpandodict["Created"]);
                            newfileuploads.Data = (byte[])(payloadexpandodict["Data"]);

                            var newobjectid = manager.Create(newfileuploads);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updatefileuploads:
                        {
                            var transactionid = expandoDict["transactionid"];
                            fileuploadsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new fileuploadsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new fileuploadsManager(_context);
                            }

                            var existingfileuploads = new fileuploads();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingfileuploads.FileUploadID = Convert.ToInt64(payloadexpandodict["FileUploadID"]);
                            existingfileuploads.Filename = Convert.ToString(payloadexpandodict["Filename"]);
                            existingfileuploads.Size = Convert.ToInt64(payloadexpandodict["Size"]);
                            existingfileuploads.Created = Convert.ToDateTime(payloadexpandodict["Created"]);
                            existingfileuploads.Data = (byte[])(payloadexpandodict["Data"]);

                            var rowUpdateCount = manager.Update(existingfileuploads);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deletefileuploads:
                        {
                            var transactionid = expandoDict["transactionid"];
                            fileuploadsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new fileuploadsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new fileuploadsManager(_context);
                            }

                            var existingfileuploads = new fileuploads();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingfileuploads.FileUploadID = Convert.ToInt64(payloadexpandodict["FileUploadID"]);

                            var rowUpdateCount = manager.Delete(existingfileuploads);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDfileuploads:
                        {
                            var transactionid = expandoDict["transactionid"];
                            fileuploadsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new fileuploadsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new fileuploadsManager(_context);
                            }

                            var existingfileuploadsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingfileuploadsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClausefileuploads:
                        {
                            var transactionid = expandoDict["transactionid"];
                            fileuploadsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new fileuploadsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new fileuploadsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClausefileuploads(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createhappyclientpictures:
                        {
                            var transactionid = expandoDict["transactionid"];
                            happyclientpicturesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new happyclientpicturesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new happyclientpicturesManager(_context);
                            }

                            var newhappyclientpictures = new happyclientpictures();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newhappyclientpictures.ProgramID = Convert.ToInt64(payloadexpandodict["ProgramID"]);
                            newhappyclientpictures.RecipientClientID = Convert.ToInt64(payloadexpandodict["RecipientClientID"]);
                            newhappyclientpictures.FileUploadID = Convert.ToInt64(payloadexpandodict["FileUploadID"]);

                            var newobjectid = manager.Create(newhappyclientpictures);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updatehappyclientpictures:
                        {
                            var transactionid = expandoDict["transactionid"];
                            happyclientpicturesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new happyclientpicturesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new happyclientpicturesManager(_context);
                            }

                            var existinghappyclientpictures = new happyclientpictures();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existinghappyclientpictures.HappyClientPictureID = Convert.ToInt64(payloadexpandodict["HappyClientPictureID"]);
                            existinghappyclientpictures.ProgramID = Convert.ToInt64(payloadexpandodict["ProgramID"]);
                            existinghappyclientpictures.RecipientClientID = Convert.ToInt64(payloadexpandodict["RecipientClientID"]);
                            existinghappyclientpictures.FileUploadID = Convert.ToInt64(payloadexpandodict["FileUploadID"]);

                            var rowUpdateCount = manager.Update(existinghappyclientpictures);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deletehappyclientpictures:
                        {
                            var transactionid = expandoDict["transactionid"];
                            happyclientpicturesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new happyclientpicturesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new happyclientpicturesManager(_context);
                            }

                            var existinghappyclientpictures = new happyclientpictures();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existinghappyclientpictures.HappyClientPictureID = Convert.ToInt64(payloadexpandodict["HappyClientPictureID"]);

                            var rowUpdateCount = manager.Delete(existinghappyclientpictures);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDhappyclientpictures:
                        {
                            var transactionid = expandoDict["transactionid"];
                            happyclientpicturesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new happyclientpicturesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new happyclientpicturesManager(_context);
                            }

                            var existinghappyclientpicturesid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existinghappyclientpicturesid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClausehappyclientpictures:
                        {
                            var transactionid = expandoDict["transactionid"];
                            happyclientpicturesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new happyclientpicturesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new happyclientpicturesManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClausehappyclientpictures(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createindividualpictures:
                        {
                            var transactionid = expandoDict["transactionid"];
                            individualpicturesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new individualpicturesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new individualpicturesManager(_context);
                            }

                            var newindividualpictures = new individualpictures();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newindividualpictures.IndividualID = Convert.ToInt64(payloadexpandodict["IndividualID"]);
                            newindividualpictures.FileUploadID = Convert.ToInt64(payloadexpandodict["FileUploadID"]);

                            var newobjectid = manager.Create(newindividualpictures);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updateindividualpictures:
                        {
                            var transactionid = expandoDict["transactionid"];
                            individualpicturesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new individualpicturesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new individualpicturesManager(_context);
                            }

                            var existingindividualpictures = new individualpictures();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingindividualpictures.IndividualPictureID = Convert.ToInt64(payloadexpandodict["IndividualPictureID"]);
                            existingindividualpictures.IndividualID = Convert.ToInt64(payloadexpandodict["IndividualID"]);
                            existingindividualpictures.FileUploadID = Convert.ToInt64(payloadexpandodict["FileUploadID"]);

                            var rowUpdateCount = manager.Update(existingindividualpictures);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deleteindividualpictures:
                        {
                            var transactionid = expandoDict["transactionid"];
                            individualpicturesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new individualpicturesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new individualpicturesManager(_context);
                            }

                            var existingindividualpictures = new individualpictures();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingindividualpictures.IndividualPictureID = Convert.ToInt64(payloadexpandodict["IndividualPictureID"]);

                            var rowUpdateCount = manager.Delete(existingindividualpictures);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDindividualpictures:
                        {
                            var transactionid = expandoDict["transactionid"];
                            individualpicturesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new individualpicturesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new individualpicturesManager(_context);
                            }

                            var existingindividualpicturesid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingindividualpicturesid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClauseindividualpictures:
                        {
                            var transactionid = expandoDict["transactionid"];
                            individualpicturesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new individualpicturesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new individualpicturesManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClauseindividualpictures(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createindividuals:
                        {
                            var transactionid = expandoDict["transactionid"];
                            individualsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new individualsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new individualsManager(_context);
                            }

                            var newindividuals = new individuals();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newindividuals.LastName = Convert.ToString(payloadexpandodict["LastName"]);
                            newindividuals.MiddleName = Convert.ToString(payloadexpandodict["MiddleName"]);
                            newindividuals.FirstName = Convert.ToString(payloadexpandodict["FirstName"]);
                            newindividuals.FatherIndividualID = Convert.ToInt64(payloadexpandodict["FatherIndividualID"]);
                            newindividuals.MotherIndividualID = Convert.ToInt64(payloadexpandodict["MotherIndividualID"]);
                            newindividuals.Birthdate = Convert.ToDateTime(payloadexpandodict["Birthdate"]);
                            newindividuals.MobilePhoneNumber = Convert.ToString(payloadexpandodict["MobilePhoneNumber"]);
                            newindividuals.HomePhoneNumber = Convert.ToString(payloadexpandodict["HomePhoneNumber"]);
                            newindividuals.AddressLine1 = Convert.ToString(payloadexpandodict["AddressLine1"]);
                            newindividuals.AddressLine2 = Convert.ToString(payloadexpandodict["AddressLine2"]);
                            newindividuals.City = Convert.ToString(payloadexpandodict["City"]);
                            newindividuals.State = Convert.ToString(payloadexpandodict["State"]);
                            newindividuals.Zip = Convert.ToString(payloadexpandodict["Zip"]);

                            var newobjectid = manager.Create(newindividuals);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updateindividuals:
                        {
                            var transactionid = expandoDict["transactionid"];
                            individualsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new individualsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new individualsManager(_context);
                            }

                            var existingindividuals = new individuals();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingindividuals.IndividualID = Convert.ToInt64(payloadexpandodict["IndividualID"]);
                            existingindividuals.LastName = Convert.ToString(payloadexpandodict["LastName"]);
                            existingindividuals.MiddleName = Convert.ToString(payloadexpandodict["MiddleName"]);
                            existingindividuals.FirstName = Convert.ToString(payloadexpandodict["FirstName"]);
                            existingindividuals.FatherIndividualID = Convert.ToInt64(payloadexpandodict["FatherIndividualID"]);
                            existingindividuals.MotherIndividualID = Convert.ToInt64(payloadexpandodict["MotherIndividualID"]);
                            existingindividuals.Birthdate = Convert.ToDateTime(payloadexpandodict["Birthdate"]);
                            existingindividuals.MobilePhoneNumber = Convert.ToString(payloadexpandodict["MobilePhoneNumber"]);
                            existingindividuals.HomePhoneNumber = Convert.ToString(payloadexpandodict["HomePhoneNumber"]);
                            existingindividuals.AddressLine1 = Convert.ToString(payloadexpandodict["AddressLine1"]);
                            existingindividuals.AddressLine2 = Convert.ToString(payloadexpandodict["AddressLine2"]);
                            existingindividuals.City = Convert.ToString(payloadexpandodict["City"]);
                            existingindividuals.State = Convert.ToString(payloadexpandodict["State"]);
                            existingindividuals.Zip = Convert.ToString(payloadexpandodict["Zip"]);

                            var rowUpdateCount = manager.Update(existingindividuals);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deleteindividuals:
                        {
                            var transactionid = expandoDict["transactionid"];
                            individualsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new individualsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new individualsManager(_context);
                            }

                            var existingindividuals = new individuals();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingindividuals.IndividualID = Convert.ToInt64(payloadexpandodict["IndividualID"]);

                            var rowUpdateCount = manager.Delete(existingindividuals);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDindividuals:
                        {
                            var transactionid = expandoDict["transactionid"];
                            individualsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new individualsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new individualsManager(_context);
                            }

                            var existingindividualsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingindividualsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClauseindividuals:
                        {
                            var transactionid = expandoDict["transactionid"];
                            individualsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new individualsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new individualsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClauseindividuals(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createmessagerecipients:
                        {
                            var transactionid = expandoDict["transactionid"];
                            messagerecipientsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new messagerecipientsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new messagerecipientsManager(_context);
                            }

                            var newmessagerecipients = new messagerecipients();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newmessagerecipients.MessageID = Convert.ToInt64(payloadexpandodict["MessageID"]);
                            newmessagerecipients.AccountID = Convert.ToInt64(payloadexpandodict["AccountID"]);
                            newmessagerecipients.AccountTypeID = Convert.ToInt64(payloadexpandodict["AccountTypeID"]);
                            newmessagerecipients.MessageRead = Convert.ToDateTime(payloadexpandodict["MessageRead"]);

                            var newobjectid = manager.Create(newmessagerecipients);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updatemessagerecipients:
                        {
                            var transactionid = expandoDict["transactionid"];
                            messagerecipientsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new messagerecipientsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new messagerecipientsManager(_context);
                            }

                            var existingmessagerecipients = new messagerecipients();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingmessagerecipients.MessageRecipientID = Convert.ToInt64(payloadexpandodict["MessageRecipientID"]);
                            existingmessagerecipients.MessageID = Convert.ToInt64(payloadexpandodict["MessageID"]);
                            existingmessagerecipients.AccountID = Convert.ToInt64(payloadexpandodict["AccountID"]);
                            existingmessagerecipients.AccountTypeID = Convert.ToInt64(payloadexpandodict["AccountTypeID"]);
                            existingmessagerecipients.MessageRead = Convert.ToDateTime(payloadexpandodict["MessageRead"]);

                            var rowUpdateCount = manager.Update(existingmessagerecipients);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deletemessagerecipients:
                        {
                            var transactionid = expandoDict["transactionid"];
                            messagerecipientsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new messagerecipientsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new messagerecipientsManager(_context);
                            }

                            var existingmessagerecipients = new messagerecipients();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingmessagerecipients.MessageRecipientID = Convert.ToInt64(payloadexpandodict["MessageRecipientID"]);

                            var rowUpdateCount = manager.Delete(existingmessagerecipients);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDmessagerecipients:
                        {
                            var transactionid = expandoDict["transactionid"];
                            messagerecipientsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new messagerecipientsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new messagerecipientsManager(_context);
                            }

                            var existingmessagerecipientsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingmessagerecipientsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClausemessagerecipients:
                        {
                            var transactionid = expandoDict["transactionid"];
                            messagerecipientsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new messagerecipientsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new messagerecipientsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClausemessagerecipients(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createmessages:
                        {
                            var transactionid = expandoDict["transactionid"];
                            messagesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new messagesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new messagesManager(_context);
                            }

                            var newmessages = new messages();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newmessages.FromAccountID = Convert.ToInt64(payloadexpandodict["FromAccountID"]);
                            newmessages.FromAccountTypeID = Convert.ToInt64(payloadexpandodict["FromAccountTypeID"]);
                            newmessages.MessageSentDateTime = Convert.ToDateTime(payloadexpandodict["MessageSentDateTime"]);
                            newmessages.Subject = Convert.ToString(payloadexpandodict["Subject"]);
                            newmessages.Body = Convert.ToString(payloadexpandodict["Body"]);

                            var newobjectid = manager.Create(newmessages);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updatemessages:
                        {
                            var transactionid = expandoDict["transactionid"];
                            messagesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new messagesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new messagesManager(_context);
                            }

                            var existingmessages = new messages();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingmessages.MessageID = Convert.ToInt64(payloadexpandodict["MessageID"]);
                            existingmessages.FromAccountID = Convert.ToInt64(payloadexpandodict["FromAccountID"]);
                            existingmessages.FromAccountTypeID = Convert.ToInt64(payloadexpandodict["FromAccountTypeID"]);
                            existingmessages.MessageSentDateTime = Convert.ToDateTime(payloadexpandodict["MessageSentDateTime"]);
                            existingmessages.Subject = Convert.ToString(payloadexpandodict["Subject"]);
                            existingmessages.Body = Convert.ToString(payloadexpandodict["Body"]);

                            var rowUpdateCount = manager.Update(existingmessages);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deletemessages:
                        {
                            var transactionid = expandoDict["transactionid"];
                            messagesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new messagesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new messagesManager(_context);
                            }

                            var existingmessages = new messages();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingmessages.MessageID = Convert.ToInt64(payloadexpandodict["MessageID"]);

                            var rowUpdateCount = manager.Delete(existingmessages);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDmessages:
                        {
                            var transactionid = expandoDict["transactionid"];
                            messagesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new messagesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new messagesManager(_context);
                            }

                            var existingmessagesid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingmessagesid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClausemessages:
                        {
                            var transactionid = expandoDict["transactionid"];
                            messagesManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new messagesManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new messagesManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClausemessages(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createmoduleviews:
                        {
                            var transactionid = expandoDict["transactionid"];
                            moduleviewsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new moduleviewsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new moduleviewsManager(_context);
                            }

                            var newmoduleviews = new moduleviews();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newmoduleviews.Name = Convert.ToString(payloadexpandodict["Name"]);
                            newmoduleviews.Occurred = Convert.ToDateTime(payloadexpandodict["Occurred"]);
                            newmoduleviews.LoggedInSecurityUserID = Convert.ToInt64(payloadexpandodict["LoggedInSecurityUserID"]);

                            var newobjectid = manager.Create(newmoduleviews);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updatemoduleviews:
                        {
                            var transactionid = expandoDict["transactionid"];
                            moduleviewsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new moduleviewsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new moduleviewsManager(_context);
                            }

                            var existingmoduleviews = new moduleviews();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingmoduleviews.ModuleViewID = Convert.ToInt64(payloadexpandodict["ModuleViewID"]);
                            existingmoduleviews.Name = Convert.ToString(payloadexpandodict["Name"]);
                            existingmoduleviews.Occurred = Convert.ToDateTime(payloadexpandodict["Occurred"]);
                            existingmoduleviews.LoggedInSecurityUserID = Convert.ToInt64(payloadexpandodict["LoggedInSecurityUserID"]);

                            var rowUpdateCount = manager.Update(existingmoduleviews);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deletemoduleviews:
                        {
                            var transactionid = expandoDict["transactionid"];
                            moduleviewsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new moduleviewsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new moduleviewsManager(_context);
                            }

                            var existingmoduleviews = new moduleviews();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingmoduleviews.ModuleViewID = Convert.ToInt64(payloadexpandodict["ModuleViewID"]);

                            var rowUpdateCount = manager.Delete(existingmoduleviews);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDmoduleviews:
                        {
                            var transactionid = expandoDict["transactionid"];
                            moduleviewsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new moduleviewsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new moduleviewsManager(_context);
                            }

                            var existingmoduleviewsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingmoduleviewsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClausemoduleviews:
                        {
                            var transactionid = expandoDict["transactionid"];
                            moduleviewsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new moduleviewsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new moduleviewsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClausemoduleviews(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createnotificationsettings:
                        {
                            var transactionid = expandoDict["transactionid"];
                            notificationsettingsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new notificationsettingsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new notificationsettingsManager(_context);
                            }

                            var newnotificationsettings = new notificationsettings();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newnotificationsettings.AccountID = Convert.ToInt64(payloadexpandodict["AccountID"]);
                            newnotificationsettings.AccountTypeID = Convert.ToInt64(payloadexpandodict["AccountTypeID"]);
                            newnotificationsettings.EnableEmailMessages = Convert.ToInt64(payloadexpandodict["EnableEmailMessages"]);
                            newnotificationsettings.EnableSMSMessages = Convert.ToInt64(payloadexpandodict["EnableSMSMessages"]);

                            var newobjectid = manager.Create(newnotificationsettings);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updatenotificationsettings:
                        {
                            var transactionid = expandoDict["transactionid"];
                            notificationsettingsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new notificationsettingsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new notificationsettingsManager(_context);
                            }

                            var existingnotificationsettings = new notificationsettings();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingnotificationsettings.NotificationSettingID = Convert.ToInt64(payloadexpandodict["NotificationSettingID"]);
                            existingnotificationsettings.AccountID = Convert.ToInt64(payloadexpandodict["AccountID"]);
                            existingnotificationsettings.AccountTypeID = Convert.ToInt64(payloadexpandodict["AccountTypeID"]);
                            existingnotificationsettings.EnableEmailMessages = Convert.ToInt64(payloadexpandodict["EnableEmailMessages"]);
                            existingnotificationsettings.EnableSMSMessages = Convert.ToInt64(payloadexpandodict["EnableSMSMessages"]);

                            var rowUpdateCount = manager.Update(existingnotificationsettings);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deletenotificationsettings:
                        {
                            var transactionid = expandoDict["transactionid"];
                            notificationsettingsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new notificationsettingsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new notificationsettingsManager(_context);
                            }

                            var existingnotificationsettings = new notificationsettings();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingnotificationsettings.NotificationSettingID = Convert.ToInt64(payloadexpandodict["NotificationSettingID"]);

                            var rowUpdateCount = manager.Delete(existingnotificationsettings);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDnotificationsettings:
                        {
                            var transactionid = expandoDict["transactionid"];
                            notificationsettingsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new notificationsettingsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new notificationsettingsManager(_context);
                            }

                            var existingnotificationsettingsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingnotificationsettingsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClausenotificationsettings:
                        {
                            var transactionid = expandoDict["transactionid"];
                            notificationsettingsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new notificationsettingsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new notificationsettingsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClausenotificationsettings(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createprogramdonorcommitments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programdonorcommitmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programdonorcommitmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programdonorcommitmentsManager(_context);
                            }

                            var newprogramdonorcommitments = new programdonorcommitments();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newprogramdonorcommitments.DonorID = Convert.ToInt64(payloadexpandodict["DonorID"]);
                            newprogramdonorcommitments.CommitmentDateTime = Convert.ToDateTime(payloadexpandodict["CommitmentDateTime"]);
                            newprogramdonorcommitments.ClientRequestID = Convert.ToInt64(payloadexpandodict["ClientRequestID"]);
                            newprogramdonorcommitments.ReceivedAtCollectionPoint = Convert.ToInt64(payloadexpandodict["ReceivedAtCollectionPoint"]);
                            newprogramdonorcommitments.DistributedToRecipient = Convert.ToInt64(payloadexpandodict["DistributedToRecipient"]);

                            var newobjectid = manager.Create(newprogramdonorcommitments);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updateprogramdonorcommitments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programdonorcommitmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programdonorcommitmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programdonorcommitmentsManager(_context);
                            }

                            var existingprogramdonorcommitments = new programdonorcommitments();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingprogramdonorcommitments.ProgramDonorCommitmentID = Convert.ToInt64(payloadexpandodict["ProgramDonorCommitmentID"]);
                            existingprogramdonorcommitments.DonorID = Convert.ToInt64(payloadexpandodict["DonorID"]);
                            existingprogramdonorcommitments.CommitmentDateTime = Convert.ToDateTime(payloadexpandodict["CommitmentDateTime"]);
                            existingprogramdonorcommitments.ClientRequestID = Convert.ToInt64(payloadexpandodict["ClientRequestID"]);
                            existingprogramdonorcommitments.ReceivedAtCollectionPoint = Convert.ToInt64(payloadexpandodict["ReceivedAtCollectionPoint"]);
                            existingprogramdonorcommitments.DistributedToRecipient = Convert.ToInt64(payloadexpandodict["DistributedToRecipient"]);

                            var rowUpdateCount = manager.Update(existingprogramdonorcommitments);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deleteprogramdonorcommitments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programdonorcommitmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programdonorcommitmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programdonorcommitmentsManager(_context);
                            }

                            var existingprogramdonorcommitments = new programdonorcommitments();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingprogramdonorcommitments.ProgramDonorCommitmentID = Convert.ToInt64(payloadexpandodict["ProgramDonorCommitmentID"]);

                            var rowUpdateCount = manager.Delete(existingprogramdonorcommitments);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDprogramdonorcommitments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programdonorcommitmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programdonorcommitmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programdonorcommitmentsManager(_context);
                            }

                            var existingprogramdonorcommitmentsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingprogramdonorcommitmentsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClauseprogramdonorcommitments:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programdonorcommitmentsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programdonorcommitmentsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programdonorcommitmentsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClauseprogramdonorcommitments(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createprogramevents:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programeventsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programeventsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programeventsManager(_context);
                            }

                            var newprogramevents = new programevents();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newprogramevents.ProgramID = Convert.ToInt64(payloadexpandodict["ProgramID"]);
                            newprogramevents.IsSingleDate = Convert.ToInt64(payloadexpandodict["IsSingleDate"]);
                            newprogramevents.FromDate = Convert.ToDateTime(payloadexpandodict["FromDate"]);
                            newprogramevents.ToDate = Convert.ToDateTime(payloadexpandodict["ToDate"]);
                            newprogramevents.Description = Convert.ToString(payloadexpandodict["Description"]);
                            newprogramevents.Name = Convert.ToString(payloadexpandodict["Name"]);

                            var newobjectid = manager.Create(newprogramevents);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updateprogramevents:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programeventsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programeventsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programeventsManager(_context);
                            }

                            var existingprogramevents = new programevents();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingprogramevents.ProgramEventID = Convert.ToInt64(payloadexpandodict["ProgramEventID"]);
                            existingprogramevents.ProgramID = Convert.ToInt64(payloadexpandodict["ProgramID"]);
                            existingprogramevents.IsSingleDate = Convert.ToInt64(payloadexpandodict["IsSingleDate"]);
                            existingprogramevents.FromDate = Convert.ToDateTime(payloadexpandodict["FromDate"]);
                            existingprogramevents.ToDate = Convert.ToDateTime(payloadexpandodict["ToDate"]);
                            existingprogramevents.Description = Convert.ToString(payloadexpandodict["Description"]);
                            existingprogramevents.Name = Convert.ToString(payloadexpandodict["Name"]);

                            var rowUpdateCount = manager.Update(existingprogramevents);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deleteprogramevents:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programeventsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programeventsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programeventsManager(_context);
                            }

                            var existingprogramevents = new programevents();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingprogramevents.ProgramEventID = Convert.ToInt64(payloadexpandodict["ProgramEventID"]);

                            var rowUpdateCount = manager.Delete(existingprogramevents);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDprogramevents:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programeventsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programeventsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programeventsManager(_context);
                            }

                            var existingprogrameventsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingprogrameventsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClauseprogramevents:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programeventsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programeventsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programeventsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClauseprogramevents(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createprograms:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programsManager(_context);
                            }

                            var newprograms = new programs();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newprograms.Name = Convert.ToString(payloadexpandodict["Name"]);
                            newprograms.Description = Convert.ToString(payloadexpandodict["Description"]);
                            newprograms.IsPublished = Convert.ToInt64(payloadexpandodict["IsPublished"]);
                            newprograms.Year = Convert.ToInt64(payloadexpandodict["Year"]);

                            var newobjectid = manager.Create(newprograms);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updateprograms:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programsManager(_context);
                            }

                            var existingprograms = new programs();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingprograms.ProgramID = Convert.ToInt64(payloadexpandodict["ProgramID"]);
                            existingprograms.Name = Convert.ToString(payloadexpandodict["Name"]);
                            existingprograms.Description = Convert.ToString(payloadexpandodict["Description"]);
                            existingprograms.IsPublished = Convert.ToInt64(payloadexpandodict["IsPublished"]);
                            existingprograms.Year = Convert.ToInt64(payloadexpandodict["Year"]);

                            var rowUpdateCount = manager.Update(existingprograms);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deleteprograms:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programsManager(_context);
                            }

                            var existingprograms = new programs();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingprograms.ProgramID = Convert.ToInt64(payloadexpandodict["ProgramID"]);

                            var rowUpdateCount = manager.Delete(existingprograms);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDprograms:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programsManager(_context);
                            }

                            var existingprogramsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingprogramsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClauseprograms:
                        {
                            var transactionid = expandoDict["transactionid"];
                            programsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new programsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new programsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClauseprograms(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createsettings:
                        {
                            var transactionid = expandoDict["transactionid"];
                            settingsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new settingsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new settingsManager(_context);
                            }

                            var newsettings = new settings();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newsettings.Name = Convert.ToString(payloadexpandodict["Name"]);
                            newsettings.Value = Convert.ToString(payloadexpandodict["Value"]);

                            var newobjectid = manager.Create(newsettings);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updatesettings:
                        {
                            var transactionid = expandoDict["transactionid"];
                            settingsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new settingsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new settingsManager(_context);
                            }

                            var existingsettings = new settings();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingsettings.SettingID = Convert.ToInt64(payloadexpandodict["SettingID"]);
                            existingsettings.Name = Convert.ToString(payloadexpandodict["Name"]);
                            existingsettings.Value = Convert.ToString(payloadexpandodict["Value"]);

                            var rowUpdateCount = manager.Update(existingsettings);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deletesettings:
                        {
                            var transactionid = expandoDict["transactionid"];
                            settingsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new settingsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new settingsManager(_context);
                            }

                            var existingsettings = new settings();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingsettings.SettingID = Convert.ToInt64(payloadexpandodict["SettingID"]);

                            var rowUpdateCount = manager.Delete(existingsettings);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDsettings:
                        {
                            var transactionid = expandoDict["transactionid"];
                            settingsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new settingsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new settingsManager(_context);
                            }

                            var existingsettingsid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingsettingsid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClausesettings:
                        {
                            var transactionid = expandoDict["transactionid"];
                            settingsManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new settingsManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new settingsManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClausesettings(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Createusers:
                        {
                            var transactionid = expandoDict["transactionid"];
                            usersManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new usersManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new usersManager(_context);
                            }

                            var newusers = new users();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            newusers.Email = Convert.ToString(payloadexpandodict["Email"]);
                            newusers.Password = Convert.ToString(payloadexpandodict["Password"]);
                            newusers.IsActive = Convert.ToInt64(payloadexpandodict["IsActive"]);
                            newusers.RegistrationCode = Convert.ToString(payloadexpandodict["RegistrationCode"]);
                            newusers.PasswordResetCode = Convert.ToString(payloadexpandodict["PasswordResetCode"]);
                            newusers.AccountTypeID = Convert.ToInt64(payloadexpandodict["AccountTypeID"]);

                            var newobjectid = manager.Create(newusers);

                            protocolReturnPacket.succeeded = true;
                            protocolReturnPacket.payload = newobjectid;

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Updateusers:
                        {
                            var transactionid = expandoDict["transactionid"];
                            usersManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new usersManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new usersManager(_context);
                            }

                            var existingusers = new users();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingusers.UserID = Convert.ToInt64(payloadexpandodict["UserID"]);
                            existingusers.Email = Convert.ToString(payloadexpandodict["Email"]);
                            existingusers.Password = Convert.ToString(payloadexpandodict["Password"]);
                            existingusers.IsActive = Convert.ToInt64(payloadexpandodict["IsActive"]);
                            existingusers.RegistrationCode = Convert.ToString(payloadexpandodict["RegistrationCode"]);
                            existingusers.PasswordResetCode = Convert.ToString(payloadexpandodict["PasswordResetCode"]);
                            existingusers.AccountTypeID = Convert.ToInt64(payloadexpandodict["AccountTypeID"]);

                            var rowUpdateCount = manager.Update(existingusers);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.Deleteusers:
                        {
                            var transactionid = expandoDict["transactionid"];
                            usersManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new usersManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new usersManager(_context);
                            }

                            var existingusers = new users();

                            var payloadexpandodict = (IDictionary<string, object>)expandoDict["payload"];

                            existingusers.UserID = Convert.ToInt64(payloadexpandodict["UserID"]);

                            var rowUpdateCount = manager.Delete(existingusers);

                            if (rowUpdateCount == 1)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveByIDusers:
                        {
                            var transactionid = expandoDict["transactionid"];
                            usersManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new usersManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                            }
                            else
                            {
                                 _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new usersManager(_context);
                            }

                            var existingusersid = Convert.ToInt64(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveByID(Convert.ToInt64(existingusersid));

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
                            }
                        }
                        break;
                    case ControllerProtocols.RetrieveWithWhereClauseusers:
                        {
                            var transactionid = expandoDict["transactionid"];
                            usersManager manager = null;
                            DbContext _context = null;

                            if (null != transactionid)
                            {
                                manager = new usersManager(cache.Get<DbContext>("dbcontext-" + transactionid));
                             }
                            else
                            {
                                _context = new DbContext(new DbConnectionFactory("DefaultConnection"));
                                manager = new usersManager(_context);
                            }

                            var WhereClause = Convert.ToString(expandoDict["payload"]);

                            protocolReturnPacket.payload = manager.RetrieveWithWhereClauseusers(WhereClause);

                            if (null != protocolReturnPacket.payload)
                            {
                                protocolReturnPacket.succeeded = true;
                            }
                            else
                            {
                                protocolReturnPacket.succeeded = false;
                            }

                            if (transactionid == null)
                            {
                                _context.Dispose();
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
