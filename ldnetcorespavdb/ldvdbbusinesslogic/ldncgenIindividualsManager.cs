using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface IindividualsManager
    {
        long Create(individuals newindividuals);
        int Update(individuals existingindividuals);
        int Delete(individuals existingindividuals);
        individuals RetrieveByID(long existingindividualsid);
        List<individuals> RetrieveWithWhereClauseindividuals(string WhereClause);
    }
}
