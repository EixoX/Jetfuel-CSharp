using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
namespace EixoX
{
    public interface ClassAcessor
    {
        Type DataType { get; }
        string Name { get; }
        MemberInfo MemberInfo { get; }
        object GetValue(object entity);
        void SetValue(object entity, object value);
    }
}
