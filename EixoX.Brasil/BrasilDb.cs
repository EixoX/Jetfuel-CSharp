using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Data;
using System.Configuration;
namespace EixoX.Brasil
{
    public class BrasilDb<T> :
        DatabaseStorage<T>
    {
        public BrasilDb(string connectionString) : base(new SqlServer(connectionString), DatabaseAspect<T>.Instance) { }
        public BrasilDb() : this(ConfigurationManager.ConnectionStrings["BrasilDbSqlServer"].ConnectionString) { }
        private static BrasilDb<T> _Instance;
        public static BrasilDb<T> Instance { get { return _Instance ?? (_Instance = new BrasilDb<T>()); } }
    }
}
