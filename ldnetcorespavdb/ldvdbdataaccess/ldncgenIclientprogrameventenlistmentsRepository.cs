using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface IclientprogrameventenlistmentsRepository
    {
        long Create(clientprogrameventenlistments newclientprogrameventenlistments);
        int Update(clientprogrameventenlistments existingclientprogrameventenlistments);
        int Delete(clientprogrameventenlistments existingclientprogrameventenlistments);
        clientprogrameventenlistments RetrieveByID(long existingclientprogrameventenlistmentsid);
        List<clientprogrameventenlistments> RetrieveWithWhereClauseclientprogrameventenlistments(string WhereClause);
    }
}
