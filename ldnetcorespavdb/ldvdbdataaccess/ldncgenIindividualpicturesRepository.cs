using System;
using System.Collections.Generic;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public interface IindividualpicturesRepository
    {
        long Create(individualpictures newindividualpictures);
        int Update(individualpictures existingindividualpictures);
        int Delete(individualpictures existingindividualpictures);
        individualpictures RetrieveByID(long existingindividualpicturesid);
        List<individualpictures> RetrieveWithWhereClauseindividualpictures(string WhereClause);
    }
}
