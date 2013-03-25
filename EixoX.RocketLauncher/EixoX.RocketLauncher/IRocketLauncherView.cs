using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    public interface IRocketLauncherView
    {
        Commands GetMenuCommand();
        ProgrammingLanguage GetProgrammingLanguage();
        String GetDirectory();
        void ShowCommandMenu();
        void ShowWelcomeMessage();
        void DisplayMessage(string message);
        void Log(string logMessage);
        bool YesOrNo(string message);
    }
}
