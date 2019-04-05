using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface IprogramdonorcommitmentsManager
    {
        long Create(programdonorcommitments newprogramdonorcommitments);
        int Update(programdonorcommitments existingprogramdonorcommitments);
        int Delete(programdonorcommitments existingprogramdonorcommitments);
        programdonorcommitments RetrieveByID(long existingprogramdonorcommitmentsid);
        List<programdonorcommitments> RetrieveWithWhereClauseprogramdonorcommitments(string WhereClause);
    }
}
