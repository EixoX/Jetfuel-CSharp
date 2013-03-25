using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    public class MVCScaffoldCommand : ICommand
    {
        public Commands Command
        {
            get { return Commands.MVCScaffold; }
        }

        public void Run(params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
