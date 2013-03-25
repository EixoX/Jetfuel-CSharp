using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher.DatabaseGathering
{
    /// <summary>
    /// A generic database credential
    /// </summary>
    public class DatabaseCredentials
    {
        public string Database { get; set; }
        public string Server { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
