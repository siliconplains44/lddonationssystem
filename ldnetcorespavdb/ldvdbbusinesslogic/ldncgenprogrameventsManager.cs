using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class programeventsManager : IprogrameventsManager
    {
        private DbContext _dbContext = null;

        private programeventsRepository AllocateRepo()
        {
            programeventsRepository repo = new programeventsRepository(_dbContext);
            return repo;
        }
        public programeventsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(programevents newprogramevents)
        {
            return AllocateRepo().Create(newprogramevents);
        }
        public int Update(programevents existingprogramevents)
        {
            return AllocateRepo().Update(existingprogramevents);
        }
        public int Delete(programevents existingprogramevents)
        {
            return AllocateRepo().Delete(existingprogramevents);
        }
        public programevents RetrieveByID(long existingprogrameventsid)
        {
            return AllocateRepo().RetrieveByID(existingprogrameventsid);
        }
        public List<programevents> RetrieveWithWhereClauseprogramevents(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClauseprogramevents(WhereClause);
        }
    }
}
