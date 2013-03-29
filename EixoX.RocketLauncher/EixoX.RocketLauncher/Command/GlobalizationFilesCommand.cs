using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher.Command
{
    public class GlobalizationFilesCommand : ICommand
    {
        public Commands Command
        {
            get { return Commands.GlobalizationFiles; }
        }

        public void Run(params object[] args)
        {
            throw new NotImplementedException();

            bool fromDatabase = (bool)args[0];

            if (fromDatabase)
            {
                /// TODO: Implement globalization files generation from database
            }
            else
            {
                /// TODO: Implement globalizatio files generation from classes
            }
        }

        private List<GenericDatabaseTable> GetTables(string connectionString)
        {
            SQLServerGatherer gatherer = new SQLServerGatherer(connectionString);
            return gatherer.GetTables().ToList();
        }
    }
}
