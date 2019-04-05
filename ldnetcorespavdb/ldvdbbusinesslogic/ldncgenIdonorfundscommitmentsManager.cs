using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface IdonorfundscommitmentsManager
    {
        long Create(donorfundscommitments newdonorfundscommitments);
        int Update(donorfundscommitments existingdonorfundscommitments);
        int Delete(donorfundscommitments existingdonorfundscommitments);
        donorfundscommitments RetrieveByID(long existingdonorfundscommitmentsid);
        List<donorfundscommitments> RetrieveWithWhereClausedonorfundscommitments(string WhereClause);
    }
}
