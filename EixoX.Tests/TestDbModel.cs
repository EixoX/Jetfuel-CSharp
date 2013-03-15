using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Data;

namespace EixoX.Tests
{
    public abstract class TestDbModel<T>
    {
        public static ClassSelect<T> Select()
        {
            return TestDb<T>.Instance.Select();
        }

        public static T WithIdentity(object identity)
        {
            return TestDb<T>.Instance.WithIdentity(identity);
        }

        public static T WithMember(string memberName, object memberValue)
        {
            return TestDb<T>.Instance.WithMember(memberName, memberValue);
        }

        public static ClassSelect<T> Search(string filter)
        {
            return TestDb<T>.Instance.Search(filter);
        }
    }
}
