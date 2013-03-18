using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public class Odbc : Database
    {

        public Odbc(string connectionString) : base(new OdbcDialect(), connectionString) { }
            

    }
}
