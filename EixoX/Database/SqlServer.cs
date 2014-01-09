using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public class SqlServer : Database
    {
        public SqlServer(string connectionString) : base(new SqlServerDialect(), connectionString) { }

    }
}
