using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class individualsManager : IindividualsManager
    {
        private DbContext _dbContext = null;

        private individualsRepository AllocateRepo()
        {
            individualsRepository repo = new individualsRepository(_dbContext);
            return repo;
        }
        public individualsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(individuals newindividuals)
        {
            return AllocateRepo().Create(newindividuals);
        }
        public int Update(individuals existingindividuals)
        {
            return AllocateRepo().Update(existingindividuals);
        }
        public int Delete(individuals existingindividuals)
        {
            return AllocateRepo().Delete(existingindividuals);
        }
        public individuals RetrieveByID(long existingindividualsid)
        {
            return AllocateRepo().RetrieveByID(existingindividualsid);
        }
        public List<individuals> RetrieveWithWhereClauseindividuals(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClauseindividuals(WhereClause);
        }
    }
}
