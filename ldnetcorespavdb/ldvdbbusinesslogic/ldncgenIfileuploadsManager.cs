using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbbusinesslogic
{
    public interface IfileuploadsManager
    {
        long Create(fileuploads newfileuploads);
        int Update(fileuploads existingfileuploads);
        int Delete(fileuploads existingfileuploads);
        fileuploads RetrieveByID(long existingfileuploadsid);
        List<fileuploads> RetrieveWithWhereClausefileuploads(string WhereClause);
    }
}
