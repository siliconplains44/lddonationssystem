using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface IclientprogramenlistmentsManager
    {
        long Create(clientprogramenlistments newclientprogramenlistments);
        int Update(clientprogramenlistments existingclientprogramenlistments);
        int Delete(clientprogramenlistments existingclientprogramenlistments);
        clientprogramenlistments RetrieveByID(long existingclientprogramenlistmentsid);
        List<clientprogramenlistments> RetrieveWithWhereClauseclientprogramenlistments(string WhereClause);
    }
}
