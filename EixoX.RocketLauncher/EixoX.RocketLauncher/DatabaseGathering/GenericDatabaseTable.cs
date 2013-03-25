using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    /// <summary>
    /// A generic representation of a database table
    /// </summary>
    public class GenericDatabaseTable
    {
        public string DatabaseName { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int ObjectId { get; set; }
        public List<GenericDatabaseColumn> Columns { get; set; }
    }
}
