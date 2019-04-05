using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface IsettingsManager
    {
        long Create(settings newsettings);
        int Update(settings existingsettings);
        int Delete(settings existingsettings);
        settings RetrieveByID(long existingsettingsid);
        List<settings> RetrieveWithWhereClausesettings(string WhereClause);
    }
}