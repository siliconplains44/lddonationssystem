using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface IclientprogramenlistmentsRepository
    {
        long Create(clientprogramenlistments newclientprogramenlistments);
        int Update(clientprogramenlistments existingclientprogramenlistments);
        int Delete(clientprogramenlistments existingclientprogramenlistments);
        clientprogramenlistments RetrieveByID(long existingclientprogramenlistmentsid);
        List<clientprogramenlistments> RetrieveWithWhereClauseclientprogramenlistments(string WhereClause);
    }
}
