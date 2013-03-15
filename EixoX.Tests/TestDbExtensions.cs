using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.Tests
{
    public static class TestDbExtensions
    {
        public static int Save<T>(this T entity)
            where T : TestDbModel<T>
        {
            return TestDb<T>.Instance.Save(entity);
        }

        public static int Delete<T>(this T entity)
            where T : TestDbModel<T>
        {
            return TestDb<T>.Instance.Delete(entity);
        }

        public static int Update<T>(this T entity)
            where T : TestDbModel<T>
        {
            return TestDb<T>.Instance.Update(entity);
        }

        public static int Insert<T>(this T entity)
            where T : TestDbModel<T>
        {
            return TestDb<T>.Instance.Insert(entity);
        }

        public static int Insert<T>(this IEnumerable<T> entities)
            where T : TestDbModel<T>
        {
            return TestDb<T>.Instance.Insert(entities);
        }

    }
}
