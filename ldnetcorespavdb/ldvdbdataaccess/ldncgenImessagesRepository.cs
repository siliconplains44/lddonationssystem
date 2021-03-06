using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface ImessagesRepository
    {
        long Create(messages newmessages);
        int Update(messages existingmessages);
        int Delete(messages existingmessages);
        messages RetrieveByID(long existingmessagesid);
        List<messages> RetrieveWithWhereClausemessages(string WhereClause);
    }
}
