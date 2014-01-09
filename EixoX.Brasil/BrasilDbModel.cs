using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Data;

namespace EixoX.Brasil
{
    public abstract class BrasilDbModel<T>
    {

        public static ClassSelect<T> Select()
        {
            return BrasilDb<T>.Instance.Select();
        }

        public static T WithIdentity(object identityValue)
        {
            return BrasilDb<T>.Instance.WithIdentity(identityValue);
        }

        public static T WithMember(string memberName, object memberValue)
        {
            return BrasilDb<T>.Instance.WithMember(memberName, memberValue);
        }
    }
}
