using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.MySql
{
    public class MySqlDatabase : Database
    {
        public MySqlDatabase(string connectionString) : base(new MySqlDialect(), connectionString) { }
    }
}
