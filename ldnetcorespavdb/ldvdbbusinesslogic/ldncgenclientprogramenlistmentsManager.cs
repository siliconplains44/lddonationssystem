using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class clientprogramenlistmentsManager : IclientprogramenlistmentsManager
    {
        private DbContext _dbContext = null;

        private clientprogramenlistmentsRepository AllocateRepo()
        {
            clientprogramenlistmentsRepository repo = new clientprogramenlistmentsRepository(_dbContext);
            return repo;
        }
        public clientprogramenlistmentsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(clientprogramenlistments newclientprogramenlistments)
        {
            return AllocateRepo().Create(newclientprogramenlistments);
        }
        public int Update(clientprogramenlistments existingclientprogramenlistments)
        {
            return AllocateRepo().Update(existingclientprogramenlistments);
        }
        public int Delete(clientprogramenlistments existingclientprogramenlistments)
        {
            return AllocateRepo().Delete(existingclientprogramenlistments);
        }
        public clientprogramenlistments RetrieveByID(long existingclientprogramenlistmentsid)
        {
            return AllocateRepo().RetrieveByID(existingclientprogramenlistmentsid);
        }
        public List<clientprogramenlistments> RetrieveWithWhereClauseclientprogramenlistments(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClauseclientprogramenlistments(WhereClause);
        }
    }
}
