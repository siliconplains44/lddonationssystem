using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface IclientprogrameventenlistmentsManager
    {
        long Create(clientprogrameventenlistments newclientprogrameventenlistments);
        int Update(clientprogrameventenlistments existingclientprogrameventenlistments);
        int Delete(clientprogrameventenlistments existingclientprogrameventenlistments);
        clientprogrameventenlistments RetrieveByID(long existingclientprogrameventenlistmentsid);
        List<clientprogrameventenlistments> RetrieveWithWhereClauseclientprogrameventenlistments(string WhereClause);
    }
}
