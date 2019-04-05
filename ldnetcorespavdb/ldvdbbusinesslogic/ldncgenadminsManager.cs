using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class adminsManager : IadminsManager
    {
        private DbContext _dbContext = null;

        private adminsRepository AllocateRepo()
        {
            adminsRepository repo = new adminsRepository(_dbContext);
            return repo;
        }
        public adminsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(admins newadmins)
        {
            return AllocateRepo().Create(newadmins);
        }
        public int Update(admins existingadmins)
        {
            return AllocateRepo().Update(existingadmins);
        }
        public int Delete(admins existingadmins)
        {
            return AllocateRepo().Delete(existingadmins);
        }
        public admins RetrieveByID(long existingadminsid)
        {
            return AllocateRepo().RetrieveByID(existingadminsid);
        }
        public List<admins> RetrieveWithWhereClauseadmins(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClauseadmins(WhereClause);
        }
    }
}
