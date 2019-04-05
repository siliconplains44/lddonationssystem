using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class donorfundscommitmentsManager : IdonorfundscommitmentsManager
    {
        private DbContext _dbContext = null;

        private donorfundscommitmentsRepository AllocateRepo()
        {
            donorfundscommitmentsRepository repo = new donorfundscommitmentsRepository(_dbContext);
            return repo;
        }
        public donorfundscommitmentsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(donorfundscommitments newdonorfundscommitments)
        {
            return AllocateRepo().Create(newdonorfundscommitments);
        }
        public int Update(donorfundscommitments existingdonorfundscommitments)
        {
            return AllocateRepo().Update(existingdonorfundscommitments);
        }
        public int Delete(donorfundscommitments existingdonorfundscommitments)
        {
            return AllocateRepo().Delete(existingdonorfundscommitments);
        }
        public donorfundscommitments RetrieveByID(long existingdonorfundscommitmentsid)
        {
            return AllocateRepo().RetrieveByID(existingdonorfundscommitmentsid);
        }
        public List<donorfundscommitments> RetrieveWithWhereClausedonorfundscommitments(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClausedonorfundscommitments(WhereClause);
        }
    }
}
