using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.MySql
{
    public class MySqlDialect : AnsiDialect
    {
        public MySqlDialect() : base('`', '`') { }

        public override System.Data.IDbConnection CreateConnection(string connectionString)
        {
            return null;
        }
    }
}
