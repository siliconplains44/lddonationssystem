using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface IprogrameventsRepository
    {
        long Create(programevents newprogramevents);
        int Update(programevents existingprogramevents);
        int Delete(programevents existingprogramevents);
        programevents RetrieveByID(long existingprogrameventsid);
        List<programevents> RetrieveWithWhereClauseprogramevents(string WhereClause);
    }
}
