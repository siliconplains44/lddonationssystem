using System;
using System.Collections.Generic;
using System.Text;

namespace ldnetcorerestapitodbgen
{
    public enum ColumnType
    {
        String = 0,
        DateTime = 1,
        Long = 2,
        Numeric = 3,
        Blob = 4,
    }

    class Column
    {
        public string Name { get; set; }
        public ColumnType columnType { get; set; }
        public bool AllowsNull { get; set; }
    }
}
