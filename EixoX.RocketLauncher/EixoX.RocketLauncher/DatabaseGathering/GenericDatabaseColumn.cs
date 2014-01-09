using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    /// <summary>
    /// A generic representation of a database column
    /// </summary>
    public class GenericDatabaseColumn
    {
        public GenericDatabaseTable Table { get; set; }
        public string Name { get; set; }
        public int OrdinalPosition { get; set; }
        public string ColumnDefault { get; set; }
        public bool IsNullable { get; set; }
        public bool IsIdentity { get; set; }
        public Type DataType { get; set; }
        public int MaxLength { get; set; }
    }
}
