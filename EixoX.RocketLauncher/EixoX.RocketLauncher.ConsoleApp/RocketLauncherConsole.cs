using EixoX.RocketLauncher.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher.ConsoleApp
{
    class RocketLauncherConsole : IRocketLauncherView
    {
        public RocketLauncherFacade RocketLaucherApp { get; set; }

        public RocketLauncherConsole()
        {
            this.RocketLaucherApp = new RocketLauncherFacade(this);
        }

        public void Start()
        {
            this.RocketLaucherApp.Main();
        }

        static void Main(string[] args)
        {
            RocketLauncherConsole consoleLauncher = new RocketLauncherConsole();
            consoleLauncher.Start();
        }

        public Commands GetMenuCommand()
        {
            ConsoleKeyInfo selectedCommand = Console.ReadKey();

            switch (Convert.ToChar(selectedCommand.KeyChar))
            {
                case '1':
                    return Commands.ClassesFromDatabase;
                case '2':
                    return Commands.MVCScaffold;
                case '3':
                    return Commands.GlobalizationFiles;
                case '4':
                    return Commands.Quit;
            }

            throw new CommandNotFoundException();
        }

        public void ShowCommandMenu()
        {
            Console.WriteLine("\n\nPlease, select one:");
            Console.WriteLine("  1. Generate classes from a database source");
            Console.WriteLine("  2. Generate MVC scaffold files");
            Console.WriteLine("  3. Generate globalization files");
            Console.WriteLine("");
            Console.WriteLine("  4. Quit Rocket Launcher");
            Console.WriteLine("---------------------------------------------");
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine("\n" + message);
        }

        public void Log(string logMessage)
        {
            Console.WriteLine("\n" + logMessage);
        }

        public ProgrammingLanguage GetProgrammingLanguage()
        {
            DisplayMessage("Choose the programming language your coding with: ");

            foreach (var enumValue in Enum.GetValues(typeof(ProgrammingLanguage)))
                Console.WriteLine("   " + (int) enumValue + "- " + Enum.GetName(typeof(ProgrammingLanguage), enumValue));

            return (ProgrammingLanguage) Enum.Parse(typeof(ProgrammingLanguage), Console.ReadKey().KeyChar.ToString());
        }

        public void ShowWelcomeMessage()
        {
            Console.WriteLine("============================================");
            Console.WriteLine("     Welcome to EixoX Rocket Launcher!");
            Console.WriteLine("       > App developed by Guilherme Rey and Rodrigo Portela");
            Console.WriteLine("       > Last modified in 03/24/2013");
            Console.WriteLine("============================================");
        }

        public string GetDirectory()
        {
            string selectedDir = string.Empty;

            if (YesOrNo("Do you want to use the directory you are in?"))
            {
                selectedDir = AppDomain.CurrentDomain.BaseDirectory;
            }
            else 
            {
                Console.Write("  Type the directory path: ");
                selectedDir = Console.ReadLine();
            }
            
            return selectedDir;
        }

        public bool YesOrNo(string message)
        {
            DisplayMessage(message + " [y/n]:");
            return Console.ReadKey().KeyChar.Equals('y');
        }
    }
}