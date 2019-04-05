using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class accounttypesManager : IaccounttypesManager
    {
        private DbContext _dbContext = null;

        private accounttypesRepository AllocateRepo()
        {
            accounttypesRepository repo = new accounttypesRepository(_dbContext);
            return repo;
        }
        public accounttypesManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(accounttypes newaccounttypes)
        {
            return AllocateRepo().Create(newaccounttypes);
        }
        public int Update(accounttypes existingaccounttypes)
        {
            return AllocateRepo().Update(existingaccounttypes);
        }
        public int Delete(accounttypes existingaccounttypes)
        {
            return AllocateRepo().Delete(existingaccounttypes);
        }
        public accounttypes RetrieveByID(long existingaccounttypesid)
        {
            return AllocateRepo().RetrieveByID(existingaccounttypesid);
        }
        public List<accounttypes> RetrieveWithWhereClauseaccounttypes(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClauseaccounttypes(WhereClause);
        }
    }
}
