using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class clientprogrameventenlistmentsManager : IclientprogrameventenlistmentsManager
    {
        private DbContext _dbContext = null;

        private clientprogrameventenlistmentsRepository AllocateRepo()
        {
            clientprogrameventenlistmentsRepository repo = new clientprogrameventenlistmentsRepository(_dbContext);
            return repo;
        }
        public clientprogrameventenlistmentsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(clientprogrameventenlistments newclientprogrameventenlistments)
        {
            return AllocateRepo().Create(newclientprogrameventenlistments);
        }
        public int Update(clientprogrameventenlistments existingclientprogrameventenlistments)
        {
            return AllocateRepo().Update(existingclientprogrameventenlistments);
        }
        public int Delete(clientprogrameventenlistments existingclientprogrameventenlistments)
        {
            return AllocateRepo().Delete(existingclientprogrameventenlistments);
        }
        public clientprogrameventenlistments RetrieveByID(long existingclientprogrameventenlistmentsid)
        {
            return AllocateRepo().RetrieveByID(existingclientprogrameventenlistmentsid);
        }
        public List<clientprogrameventenlistments> RetrieveWithWhereClauseclientprogrameventenlistments(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClauseclientprogrameventenlistments(WhereClause);
        }
    }
}
