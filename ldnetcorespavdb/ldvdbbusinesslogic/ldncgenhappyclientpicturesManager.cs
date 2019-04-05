using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class happyclientpicturesManager : IhappyclientpicturesManager
    {
        private DbContext _dbContext = null;

        private happyclientpicturesRepository AllocateRepo()
        {
            happyclientpicturesRepository repo = new happyclientpicturesRepository(_dbContext);
            return repo;
        }
        public happyclientpicturesManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(happyclientpictures newhappyclientpictures)
        {
            return AllocateRepo().Create(newhappyclientpictures);
        }
        public int Update(happyclientpictures existinghappyclientpictures)
        {
            return AllocateRepo().Update(existinghappyclientpictures);
        }
        public int Delete(happyclientpictures existinghappyclientpictures)
        {
            return AllocateRepo().Delete(existinghappyclientpictures);
        }
        public happyclientpictures RetrieveByID(long existinghappyclientpicturesid)
        {
            return AllocateRepo().RetrieveByID(existinghappyclientpicturesid);
        }
        public List<happyclientpictures> RetrieveWithWhereClausehappyclientpictures(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClausehappyclientpictures(WhereClause);
        }
    }
}
