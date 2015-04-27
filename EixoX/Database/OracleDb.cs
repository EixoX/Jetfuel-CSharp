using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public class OracleDb : Database
    {
        public OracleDb(string connectionString) : base(new OracleDialect(), connectionString) { }

    }
}
