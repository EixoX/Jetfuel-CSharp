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

        public string ToSqlServerConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Initial Catalog=");
            sb.Append(this.Database);
            sb.Append(";Data Source=");
            sb.Append(this.Server);
            sb.Append(";User Id=");
            sb.Append(this.UserId);
            sb.Append(";Password=");
            sb.Append(this.Password);

            return sb.ToString();
        }
    }
}
