using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;

namespace ldvdbdal
{
    public abstract class Repository<TEntity> where TEntity : new()
    {
        DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        protected DbContext Context
        {
            get
            {
                return this._context;
            }
        }

        protected IEnumerable<TEntity> ToList(IDbCommand command)
        {
            using (var record = command.ExecuteReader())
            {
                List<TEntity> items = new List<TEntity>();
                while (record.Read())
                {

                    items.Add(Map<TEntity>(record));
                }
                return items;
            }
        }


        protected TEntityType Map<TEntityType>(IDataRecord record)
        {
            var objT = Activator.CreateInstance<TEntityType>();
            foreach (var property in typeof(TEntityType).GetProperties())
            {
                if (!record.IsDBNull(record.GetOrdinal(property.Name)))
                    property.SetValue(objT, record[property.Name]);


            }
            return objT;
        }


    }
}
