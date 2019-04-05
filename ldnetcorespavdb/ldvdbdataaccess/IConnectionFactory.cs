using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;

namespace ldvdbdal
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
