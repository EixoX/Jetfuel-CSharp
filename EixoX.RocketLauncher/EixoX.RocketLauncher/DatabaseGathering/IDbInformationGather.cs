using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    public interface IDbInformationGather
    {
        IEnumerable<GenericDatabaseColumn> GetColumns(string table);
        IEnumerable<GenericDatabaseTable> GetTables(string database);
    }
}
