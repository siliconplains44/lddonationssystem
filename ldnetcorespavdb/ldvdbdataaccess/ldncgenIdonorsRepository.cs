using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface IdonorsRepository
    {
        long Create(donors newdonors);
        int Update(donors existingdonors);
        int Delete(donors existingdonors);
        donors RetrieveByID(long existingdonorsid);
        List<donors> RetrieveWithWhereClausedonors(string WhereClause);
    }
}
