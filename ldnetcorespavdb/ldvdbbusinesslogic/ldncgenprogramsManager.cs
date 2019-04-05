using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class programsManager : IprogramsManager
    {
        private DbContext _dbContext = null;

        private programsRepository AllocateRepo()
        {
            programsRepository repo = new programsRepository(_dbContext);
            return repo;
        }
        public programsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(programs newprograms)
        {
            return AllocateRepo().Create(newprograms);
        }
        public int Update(programs existingprograms)
        {
            return AllocateRepo().Update(existingprograms);
        }
        public int Delete(programs existingprograms)
        {
            return AllocateRepo().Delete(existingprograms);
        }
        public programs RetrieveByID(long existingprogramsid)
        {
            return AllocateRepo().RetrieveByID(existingprogramsid);
        }
        public List<programs> RetrieveWithWhereClauseprograms(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClauseprograms(WhereClause);
        }
    }
}
