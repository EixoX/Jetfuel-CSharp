using EixoX.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EixoX.MySql
{
    public class MySqlDialect : AnsiDialect
    {
        public MySqlDialect() : base('`','`')
        {

        }

        public override System.Data.IDbConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        
    }
}
