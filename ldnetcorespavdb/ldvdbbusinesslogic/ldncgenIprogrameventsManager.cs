using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface IprogrameventsManager
    {
        long Create(programevents newprogramevents);
        int Update(programevents existingprogramevents);
        int Delete(programevents existingprogramevents);
        programevents RetrieveByID(long existingprogrameventsid);
        List<programevents> RetrieveWithWhereClauseprogramevents(string WhereClause);
    }
}
