using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface IhappyclientpicturesRepository
    {
        long Create(happyclientpictures newhappyclientpictures);
        int Update(happyclientpictures existinghappyclientpictures);
        int Delete(happyclientpictures existinghappyclientpictures);
        happyclientpictures RetrieveByID(long existinghappyclientpicturesid);
        List<happyclientpictures> RetrieveWithWhereClausehappyclientpictures(string WhereClause);
    }
}
