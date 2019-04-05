using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class clientrequestsManager : IclientrequestsManager
    {
        private DbContext _dbContext = null;

        private clientrequestsRepository AllocateRepo()
        {
            clientrequestsRepository repo = new clientrequestsRepository(_dbContext);
            return repo;
        }
        public clientrequestsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(clientrequests newclientrequests)
        {
            return AllocateRepo().Create(newclientrequests);
        }
        public int Update(clientrequests existingclientrequests)
        {
            return AllocateRepo().Update(existingclientrequests);
        }
        public int Delete(clientrequests existingclientrequests)
        {
            return AllocateRepo().Delete(existingclientrequests);
        }
        public clientrequests RetrieveByID(long existingclientrequestsid)
        {
            return AllocateRepo().RetrieveByID(existingclientrequestsid);
        }
        public List<clientrequests> RetrieveWithWhereClauseclientrequests(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClauseclientrequests(WhereClause);
        }
    }
}
