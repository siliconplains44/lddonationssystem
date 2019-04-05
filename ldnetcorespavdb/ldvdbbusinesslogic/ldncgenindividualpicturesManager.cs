using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class individualpicturesManager : IindividualpicturesManager
    {
        private DbContext _dbContext = null;

        private individualpicturesRepository AllocateRepo()
        {
            individualpicturesRepository repo = new individualpicturesRepository(_dbContext);
            return repo;
        }
        public individualpicturesManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(individualpictures newindividualpictures)
        {
            return AllocateRepo().Create(newindividualpictures);
        }
        public int Update(individualpictures existingindividualpictures)
        {
            return AllocateRepo().Update(existingindividualpictures);
        }
        public int Delete(individualpictures existingindividualpictures)
        {
            return AllocateRepo().Delete(existingindividualpictures);
        }
        public individualpictures RetrieveByID(long existingindividualpicturesid)
        {
            return AllocateRepo().RetrieveByID(existingindividualpicturesid);
        }
        public List<individualpictures> RetrieveWithWhereClauseindividualpictures(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClauseindividualpictures(WhereClause);
        }
    }
}
