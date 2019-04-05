using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface IaccounttypesRepository
    {
        long Create(accounttypes newaccounttypes);
        int Update(accounttypes existingaccounttypes);
        int Delete(accounttypes existingaccounttypes);
        accounttypes RetrieveByID(long existingaccounttypesid);
        List<accounttypes> RetrieveWithWhereClauseaccounttypes(string WhereClause);
    }
}
