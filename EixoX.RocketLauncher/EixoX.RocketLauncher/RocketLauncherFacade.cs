using EixoX.RocketLauncher.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    /// <summary>
    /// An object that controls all of the Rocket Launcher execution
    /// </summary>
    public class RocketLauncherFacade
    {
        /// <summary>
        /// The view, used to get and send messages
        /// </summary>
        private readonly IRocketLauncherView View;

        public RocketLauncherFacade(IRocketLauncherView view)
        {
            this.View = view;
        }

        /// <summary>
        /// Executes the program, starting an infinite loop, that'll break only with command Quit.
        /// </summary>
        public void Main()
        {
            this.View.ShowWelcomeMessage();
            ICommand commandToRun = null;

            do
            {
                #region Getting command action
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
                #endregion


                List<object> args = new List<object>();

                ProgrammingLanguage language;
                string directory = null;
                List<ClassFile> classesGenerated;
                
                switch (selectedCommand)
                {
                    case Commands.ClassesFromDatabase:
                        language = this.View.GetProgrammingLanguage();
                        directory = this.View.GetDirectory();

                        commandToRun = new Command.ClassesFromDatabaseCommand(language, directory, this.View);
                        commandToRun.Run(this.View.YesOrNo("Run command in verbose mode?"));
                        break;
                    case Commands.GlobalizationFiles:
                        //language  = this.View.GetProgrammingLanguage();
                        //directory = this.View.GetDirectory();
                        //commandToRun = new GlobalizationFilesCommand(language, directory, classesGenerated);
                        break;
                }

            }
            while (true);
        }
    }
}