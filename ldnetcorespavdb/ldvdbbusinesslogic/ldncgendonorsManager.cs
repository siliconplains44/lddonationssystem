using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class donorsManager : IdonorsManager
    {
        private DbContext _dbContext = null;

        private donorsRepository AllocateRepo()
        {
            donorsRepository repo = new donorsRepository(_dbContext);
            return repo;
        }
        public donorsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(donors newdonors)
        {
            return AllocateRepo().Create(newdonors);
        }
        public int Update(donors existingdonors)
        {
            return AllocateRepo().Update(existingdonors);
        }
        public int Delete(donors existingdonors)
        {
            return AllocateRepo().Delete(existingdonors);
        }
        public donors RetrieveByID(long existingdonorsid)
        {
            return AllocateRepo().RetrieveByID(existingdonorsid);
        }
        public List<donors> RetrieveWithWhereClausedonors(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClausedonors(WhereClause);
        }
    }
}
