using EixoX.RocketLauncher.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    public class RocketLauncherFacade
    {
        private readonly IRocketLauncherView View;

        public RocketLauncherFacade(IRocketLauncherView view)
        {
            this.View = view;
        }

        public void Main()
        {
            this.View.ShowWelcomeMessage();
            ICommand commandToRun = null;

            do
            {
                this.View.ShowCommandMenu();

                Commands selectedCommand = Commands.Quit;
                bool tryGetCommand = true;

                while (tryGetCommand)
                {
                    try
                    {
                        selectedCommand = this.View.GetMenuCommand();
                        tryGetCommand = false;
                    }
                    catch (CommandNotFoundException)
                    {
                        this.View.DisplayMessage("Command key not found. Please, try again.");
                    }
                    catch (Exception ex)
                    {
                        this.View.DisplayMessage("Unknow exception: " + ex.Message + "\n Exiting program.");
                        selectedCommand = Commands.Quit;
                    }
                }

                // Stops infite loop
                if (selectedCommand == Commands.Quit)
                    break;

                List<object> args = new List<object>();

                switch (selectedCommand)
                {
                    case Commands.ClassesFromDatabase:
                        ProgrammingLanguage language = this.View.GetProgrammingLanguage();
                        string directory = this.View.GetDirectory();
                        commandToRun = new Command.ClassesFromDatabaseCommand(language, directory, this.View);
                        commandToRun.Run(this.View.YesOrNo("Run command in verbose mode?"));
                        break;
                    case Commands.MVCScaffold:
                        commandToRun = new MVCScaffoldCommand();
                        break;
                    case Commands.GlobalizationFiles:
                        // commandToRun = new GlobalizationFilesCommand();
                        break;
                }

            }
            while (true);
        }
    }
}