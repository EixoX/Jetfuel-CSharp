using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public class OleDbDialect: AnsiDialect
    {
        public OleDbDialect() : base('[', ']') { }



        public override System.Data.IDbConnection CreateConnection(string connectionString)
        {
            return new System.Data.OleDb.OleDbConnection(connectionString);
        }
    }
}
