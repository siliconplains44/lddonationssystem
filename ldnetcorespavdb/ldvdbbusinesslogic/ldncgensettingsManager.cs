using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class settingsManager : IsettingsManager
    {
        private DbContext _dbContext = null;

        private settingsRepository AllocateRepo()
        {
            settingsRepository repo = new settingsRepository(_dbContext);
            return repo;
        }
        public settingsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(settings newsettings)
        {
            return AllocateRepo().Create(newsettings);
        }
        public int Update(settings existingsettings)
        {
            return AllocateRepo().Update(existingsettings);
        }
        public int Delete(settings existingsettings)
        {
            return AllocateRepo().Delete(existingsettings);
        }
        public settings RetrieveByID(long existingsettingsid)
        {
            return AllocateRepo().RetrieveByID(existingsettingsid);
        }
        public List<settings> RetrieveWithWhereClausesettings(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClausesettings(WhereClause);
        }
    }
}
