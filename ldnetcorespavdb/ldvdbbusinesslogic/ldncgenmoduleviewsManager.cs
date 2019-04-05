using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class moduleviewsManager : ImoduleviewsManager
    {
        private DbContext _dbContext = null;

        private moduleviewsRepository AllocateRepo()
        {
            moduleviewsRepository repo = new moduleviewsRepository(_dbContext);
            return repo;
        }
        public moduleviewsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(moduleviews newmoduleviews)
        {
            return AllocateRepo().Create(newmoduleviews);
        }
        public int Update(moduleviews existingmoduleviews)
        {
            return AllocateRepo().Update(existingmoduleviews);
        }
        public int Delete(moduleviews existingmoduleviews)
        {
            return AllocateRepo().Delete(existingmoduleviews);
        }
        public moduleviews RetrieveByID(long existingmoduleviewsid)
        {
            return AllocateRepo().RetrieveByID(existingmoduleviewsid);
        }
        public List<moduleviews> RetrieveWithWhereClausemoduleviews(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClausemoduleviews(WhereClause);
        }
    }
}
