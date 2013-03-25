using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    public interface ICommand
    {
        Commands Command { get; }
        void Run(params object[] args);
    }
}
