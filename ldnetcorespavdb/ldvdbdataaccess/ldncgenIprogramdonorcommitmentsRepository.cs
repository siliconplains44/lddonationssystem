using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface IprogramdonorcommitmentsRepository
    {
        long Create(programdonorcommitments newprogramdonorcommitments);
        int Update(programdonorcommitments existingprogramdonorcommitments);
        int Delete(programdonorcommitments existingprogramdonorcommitments);
        programdonorcommitments RetrieveByID(long existingprogramdonorcommitmentsid);
        List<programdonorcommitments> RetrieveWithWhereClauseprogramdonorcommitments(string WhereClause);
    }
}
