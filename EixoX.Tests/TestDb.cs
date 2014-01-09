using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EixoX.Tests
{
    public class TestDb<T> : DatabaseStorage<T>
    {
        private static TestDb<T> _instance;
        public static TestDb<T> Instance
        {
            get { return _instance ?? (_instance = new TestDb<T>()); }
        }

        private TestDb()
            : base(new SqlServer("Server=glamdb.ckbaeznfuhex.sa-east-1.rds.amazonaws.com;Database=TestDb;User Id=glamdev;Password=dev@glambox"), DatabaseAspect<T>.Instance)
        {

        }
    }
}
