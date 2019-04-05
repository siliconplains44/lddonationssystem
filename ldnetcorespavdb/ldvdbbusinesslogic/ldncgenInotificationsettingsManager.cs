using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface InotificationsettingsManager
    {
        long Create(notificationsettings newnotificationsettings);
        int Update(notificationsettings existingnotificationsettings);
        int Delete(notificationsettings existingnotificationsettings);
        notificationsettings RetrieveByID(long existingnotificationsettingsid);
        List<notificationsettings> RetrieveWithWhereClausenotificationsettings(string WhereClause);
    }
}
