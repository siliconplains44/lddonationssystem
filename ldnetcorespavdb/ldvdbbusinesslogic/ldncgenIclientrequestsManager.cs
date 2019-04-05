using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface IclientrequestsManager
    {
        long Create(clientrequests newclientrequests);
        int Update(clientrequests existingclientrequests);
        int Delete(clientrequests existingclientrequests);
        clientrequests RetrieveByID(long existingclientrequestsid);
        List<clientrequests> RetrieveWithWhereClauseclientrequests(string WhereClause);
    }
}
