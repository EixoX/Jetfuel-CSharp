using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    /// <summary>
    /// Represents all application's commands.
    /// 0 - Create classes from database
    /// 1 - Create MVC Scaffold project
    /// 2 - Create Globalization files for a class library project
    /// </summary>
    public enum Commands
    {
        Quit = -1,
        ClassesFromDatabase = 0,
        MVCScaffold = 1,
        GlobalizationFiles = 2
    }
}
