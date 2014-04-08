using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher.DatabaseGathering
{
    public class DatabaseTableLister
    {
        public IEnumerable<GenericDatabaseTable> Execute(Viewee viewee, string connectionString)
        {
            SQLServerGatherer sqlGatherer = null;
            try
            {
                if (!string.IsNullOrEmpty(connectionString))
                    sqlGatherer = new SQLServerGatherer(connectionString);
            }
            catch (Exception ex)
            {
                viewee.OnException(ex);
                Enumerable.Empty<GenericDatabaseTable>();
            }

            List<GenericDatabaseTable> tables = sqlGatherer.GetTables().ToList();

            if (tables.Count <= 0)
                Enumerable.Empty<GenericDatabaseTable>();

            foreach (GenericDatabaseTable table in tables)
                yield return table;
        }
    }
}
