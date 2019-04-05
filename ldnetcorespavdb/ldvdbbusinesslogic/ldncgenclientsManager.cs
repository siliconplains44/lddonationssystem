using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class clientsManager : IclientsManager
    {
        private DbContext _dbContext = null;

        private clientsRepository AllocateRepo()
        {
            clientsRepository repo = new clientsRepository(_dbContext);
            return repo;
        }
        public clientsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(clients newclients)
        {
            return AllocateRepo().Create(newclients);
        }
        public int Update(clients existingclients)
        {
            return AllocateRepo().Update(existingclients);
        }
        public int Delete(clients existingclients)
        {
            return AllocateRepo().Delete(existingclients);
        }
        public clients RetrieveByID(long existingclientsid)
        {
            return AllocateRepo().RetrieveByID(existingclientsid);
        }
        public List<clients> RetrieveWithWhereClauseclients(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClauseclients(WhereClause);
        }
    }
}
