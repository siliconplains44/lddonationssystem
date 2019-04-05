using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface IdonorfundscommitmentsRepository
    {
        long Create(donorfundscommitments newdonorfundscommitments);
        int Update(donorfundscommitments existingdonorfundscommitments);
        int Delete(donorfundscommitments existingdonorfundscommitments);
        donorfundscommitments RetrieveByID(long existingdonorfundscommitmentsid);
        List<donorfundscommitments> RetrieveWithWhereClausedonorfundscommitments(string WhereClause);
    }
}
