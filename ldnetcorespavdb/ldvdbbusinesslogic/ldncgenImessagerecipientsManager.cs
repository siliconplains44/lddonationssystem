using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface ImessagerecipientsManager
    {
        long Create(messagerecipients newmessagerecipients);
        int Update(messagerecipients existingmessagerecipients);
        int Delete(messagerecipients existingmessagerecipients);
        messagerecipients RetrieveByID(long existingmessagerecipientsid);
        List<messagerecipients> RetrieveWithWhereClausemessagerecipients(string WhereClause);
    }
}
