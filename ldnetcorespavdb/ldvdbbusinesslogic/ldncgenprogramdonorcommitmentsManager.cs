using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class programdonorcommitmentsManager : IprogramdonorcommitmentsManager
    {
        private DbContext _dbContext = null;

        private programdonorcommitmentsRepository AllocateRepo()
        {
            programdonorcommitmentsRepository repo = new programdonorcommitmentsRepository(_dbContext);
            return repo;
        }
        public programdonorcommitmentsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(programdonorcommitments newprogramdonorcommitments)
        {
            return AllocateRepo().Create(newprogramdonorcommitments);
        }
        public int Update(programdonorcommitments existingprogramdonorcommitments)
        {
            return AllocateRepo().Update(existingprogramdonorcommitments);
        }
        public int Delete(programdonorcommitments existingprogramdonorcommitments)
        {
            return AllocateRepo().Delete(existingprogramdonorcommitments);
        }
        public programdonorcommitments RetrieveByID(long existingprogramdonorcommitmentsid)
        {
            return AllocateRepo().RetrieveByID(existingprogramdonorcommitmentsid);
        }
        public List<programdonorcommitments> RetrieveWithWhereClauseprogramdonorcommitments(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClauseprogramdonorcommitments(WhereClause);
        }
    }
}
