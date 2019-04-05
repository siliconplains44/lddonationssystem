using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface IclientrequestsRepository
    {
        long Create(clientrequests newclientrequests);
        int Update(clientrequests existingclientrequests);
        int Delete(clientrequests existingclientrequests);
        clientrequests RetrieveByID(long existingclientrequestsid);
        List<clientrequests> RetrieveWithWhereClauseclientrequests(string WhereClause);
    }
}
