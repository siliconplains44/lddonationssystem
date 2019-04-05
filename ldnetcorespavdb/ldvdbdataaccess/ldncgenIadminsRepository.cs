using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface IadminsRepository
    {
        long Create(admins newadmins);
        int Update(admins existingadmins);
        int Delete(admins existingadmins);
        admins RetrieveByID(long existingadminsid);
        List<admins> RetrieveWithWhereClauseadmins(string WhereClause);
    }
}
