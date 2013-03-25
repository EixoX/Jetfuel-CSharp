using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher.Command
{
    public class ClassesFromDatabaseCommand : ICommand
    {
        public ClassGenerator ClassGenerator { get; set; }
        public ProgrammingLanguage Language { get; set; }
        public IRocketLauncherView View { get; set; }

        public Commands Command
        {
            get { return Commands.ClassesFromDatabase; }
        }

        public ClassesFromDatabaseCommand(ProgrammingLanguage language, string directory, IRocketLauncherView view)
        {
            this.Language = language;
            this.View = view;

            switch (language)
            {
                case ProgrammingLanguage.Csharp:
                    this.ClassGenerator = new CSharpClassGenerator(directory);
                    break;
                case ProgrammingLanguage.Java:
                    break;
                case ProgrammingLanguage.VBNET:
                    break;
                default:
                    break;
            }
        }

        public void Run(params object[] args)
        {
            bool verbose = (bool) args[0];
            SQLServerGatherer sqlGatherer = new SQLServerGatherer(System.Configuration.ConfigurationSettings.AppSettings["DefaultConnectionString"].ToString());

            this.View.DisplayMessage("\n -- Running Command: Classes from database ---");
            DateTime tStart = DateTime.Now;

            if (verbose)
                this.View.DisplayMessage("Fetching database information...");

            List<GenericDatabaseTable> tables = sqlGatherer.GetTables().ToList();

            if (tables.Count <= 0)
            {
                if (verbose)
                    this.View.DisplayMessage("No tables found on this database");

                return;
            }

            if (verbose)
                this.View.DisplayMessage("Found " + tables.Count + " tables (" + DateTime.Now.Subtract(tStart).TotalSeconds + " seconds). Fetching columns information...");

            tStart = DateTime.Now;

            foreach (GenericDatabaseTable table in tables)
            {
                table.Columns = sqlGatherer.GetColumns(table.Name).ToList();
                if (verbose)
                    this.View.DisplayMessage(" [" + table.Name + "]: " + table.Columns.Count + " columns found");
            }

            if (verbose)
            {
                this.View.DisplayMessage("Found all columns (" + DateTime.Now.Subtract(tStart).TotalSeconds + " seconds)");
                this.View.DisplayMessage("Generating classes...");
            }

            tStart = DateTime.Now;
            foreach (GenericDatabaseTable table in tables)
            {
                if (verbose)
                    this.View.DisplayMessage("Creating " + table.Name + " class (using " + Enum.GetName(typeof(ProgrammingLanguage), this.Language) + ")");

                this.ClassGenerator.CreateClass(table, this.Language).Save(this.ClassGenerator._Directory);
            }

            if (verbose)
                this.View.DisplayMessage("Created all files (" + DateTime.Now.Subtract(tStart).TotalSeconds + " seconds)");

            this.View.DisplayMessage("Command run succesfully\n");
        }
    }
}
