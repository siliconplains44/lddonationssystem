using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface ImessagerecipientsRepository
    {
        long Create(messagerecipients newmessagerecipients);
        int Update(messagerecipients existingmessagerecipients);
        int Delete(messagerecipients existingmessagerecipients);
        messagerecipients RetrieveByID(long existingmessagerecipientsid);
        List<messagerecipients> RetrieveWithWhereClausemessagerecipients(string WhereClause);
    }
}
