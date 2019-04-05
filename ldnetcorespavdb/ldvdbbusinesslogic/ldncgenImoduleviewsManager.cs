using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface ImoduleviewsManager
    {
        long Create(moduleviews newmoduleviews);
        int Update(moduleviews existingmoduleviews);
        int Delete(moduleviews existingmoduleviews);
        moduleviews RetrieveByID(long existingmoduleviewsid);
        List<moduleviews> RetrieveWithWhereClausemoduleviews(string WhereClause);
    }
}
