using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ldvdbdal
{
    public interface IUnitOfWork
    {
        void Dispose();

        void SaveChanges();
    }
}
