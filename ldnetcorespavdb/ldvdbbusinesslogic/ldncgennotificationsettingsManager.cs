using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class notificationsettingsManager : InotificationsettingsManager
    {
        private DbContext _dbContext = null;

        private notificationsettingsRepository AllocateRepo()
        {
            notificationsettingsRepository repo = new notificationsettingsRepository(_dbContext);
            return repo;
        }
        public notificationsettingsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(notificationsettings newnotificationsettings)
        {
            return AllocateRepo().Create(newnotificationsettings);
        }
        public int Update(notificationsettings existingnotificationsettings)
        {
            return AllocateRepo().Update(existingnotificationsettings);
        }
        public int Delete(notificationsettings existingnotificationsettings)
        {
            return AllocateRepo().Delete(existingnotificationsettings);
        }
        public notificationsettings RetrieveByID(long existingnotificationsettingsid)
        {
            return AllocateRepo().RetrieveByID(existingnotificationsettingsid);
        }
        public List<notificationsettings> RetrieveWithWhereClausenotificationsettings(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClausenotificationsettings(WhereClause);
        }
    }
}
