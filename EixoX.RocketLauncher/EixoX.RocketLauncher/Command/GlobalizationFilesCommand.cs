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

        public bool GatherClassesFromDb { get; set; }

        public GlobalizationFilesCommand(string directory, ProgrammingLanguage language, List<ClassFile> preGeneratedClasses)
        {
            
        }

        public void Run(params object[] args)
        {

        }
    }
}
