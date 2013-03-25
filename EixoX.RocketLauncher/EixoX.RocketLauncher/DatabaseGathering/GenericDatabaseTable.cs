using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    public class GenericDatabaseTable
    {
        public string DatabaseName { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<GenericDatabaseColumn> Columns { get; set; }
    }
}
