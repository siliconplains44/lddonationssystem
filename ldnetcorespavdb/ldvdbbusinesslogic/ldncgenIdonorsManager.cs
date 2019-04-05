using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface IdonorsManager
    {
        long Create(donors newdonors);
        int Update(donors existingdonors);
        int Delete(donors existingdonors);
        donors RetrieveByID(long existingdonorsid);
        List<donors> RetrieveWithWhereClausedonors(string WhereClause);
    }
}
