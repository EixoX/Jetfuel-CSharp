using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public class OleDb: Database
    {
        public OleDb(string connectionString) : base(new OleDbDialect(), connectionString) { }
    }
}
