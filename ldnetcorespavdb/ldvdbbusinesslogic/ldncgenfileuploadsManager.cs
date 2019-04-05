using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class fileuploadsManager : IfileuploadsManager
    {
        private DbContext _dbContext = null;

        private fileuploadsRepository AllocateRepo()
        {
            fileuploadsRepository repo = new fileuploadsRepository(_dbContext);
            return repo;
        }
        public fileuploadsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(fileuploads newfileuploads)
        {
            return AllocateRepo().Create(newfileuploads);
        }
        public int Update(fileuploads existingfileuploads)
        {
            return AllocateRepo().Update(existingfileuploads);
        }
        public int Delete(fileuploads existingfileuploads)
        {
            return AllocateRepo().Delete(existingfileuploads);
        }
        public fileuploads RetrieveByID(long existingfileuploadsid)
        {
            return AllocateRepo().RetrieveByID(existingfileuploadsid);
        }
        public List<fileuploads> RetrieveWithWhereClausefileuploads(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClausefileuploads(WhereClause);
        }
    }
}
