using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface IprogramsManager
    {
        long Create(programs newprograms);
        int Update(programs existingprograms);
        int Delete(programs existingprograms);
        programs RetrieveByID(long existingprogramsid);
        List<programs> RetrieveWithWhereClauseprograms(string WhereClause);
    }
}
