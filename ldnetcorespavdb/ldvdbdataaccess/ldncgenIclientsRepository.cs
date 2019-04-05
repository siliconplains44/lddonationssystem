using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface IclientsRepository
    {
        long Create(clients newclients);
        int Update(clients existingclients);
        int Delete(clients existingclients);
        clients RetrieveByID(long existingclientsid);
        List<clients> RetrieveWithWhereClauseclients(string WhereClause);
    }
}
