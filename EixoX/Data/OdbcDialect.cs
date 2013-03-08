using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public class OdbcDialect : AnsiDialect
    {
        public OdbcDialect() : base('[', ']') { }

        public override System.Data.IDbConnection CreateConnection(string connectionString)
        {
            return new System.Data.Odbc.OdbcConnection(connectionString);
        }
    }
}
