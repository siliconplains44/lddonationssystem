using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface IusersManager
    {
        long Create(users newusers);
        int Update(users existingusers);
        int Delete(users existingusers);
        users RetrieveByID(long existingusersid);
        List<users> RetrieveWithWhereClauseusers(string WhereClause);
    }
}
