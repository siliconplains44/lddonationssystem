using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class usersManager : IusersManager
    {
        private DbContext _dbContext = null;

        private usersRepository AllocateRepo()
        {
            usersRepository repo = new usersRepository(_dbContext);
            return repo;
        }
        public usersManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(users newusers)
        {
            return AllocateRepo().Create(newusers);
        }
        public int Update(users existingusers)
        {
            return AllocateRepo().Update(existingusers);
        }
        public int Delete(users existingusers)
        {
            return AllocateRepo().Delete(existingusers);
        }
        public users RetrieveByID(long existingusersid)
        {
            return AllocateRepo().RetrieveByID(existingusersid);
        }
        public List<users> RetrieveWithWhereClauseusers(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClauseusers(WhereClause);
        }
    }
}
