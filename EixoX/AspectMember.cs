using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public class AspectMember : ClassAcessor
    {
        private readonly ClassAcessor _Acessor;

        public AspectMember(ClassAcessor acessor)
        {
            this._Acessor = acessor;
        }

        public Type DataType
        {
            get { return _Acessor.DataType; }
        }

        public string Name
        {
            get { return _Acessor.Name; }
        }

        public System.Reflection.MemberInfo MemberInfo
        {
            get { return _Acessor.MemberInfo; }
        }

        public object GetValue(object entity)
        {
            return _Acessor.GetValue(entity);
        }

        public void SetValue(object entity, object value)
        {
            _Acessor.SetValue(entity, value);
        }

        public override string ToString()
        {
            return _Acessor.Name;
        }

        public bool HasAttribute(Type attributeType, bool inherit)
        {
            object[] obj = _Acessor.MemberInfo.GetCustomAttributes(attributeType, inherit);
            return obj != null && obj.Length > 0;
        }

        public bool HasAttribute<TAttribute>(bool inherit)
        {
            return HasAttribute(typeof(TAttribute), inherit);
        }

        public object GetAttribute(Type attributeType, bool inherit)
        {
            object[] obj = _Acessor.MemberInfo.GetCustomAttributes(attributeType, inherit);
            return obj != null && obj.Length > 0 ? obj[0] : null;
        }

        public TAttribute GetAttribute<TAttribute>(bool inherit)
        {
            object[] obj = _Acessor.MemberInfo.GetCustomAttributes(typeof(TAttribute), inherit);
            return obj != null && obj.Length > 0 ? (TAttribute)obj[0] : default(TAttribute);
        }

        public object[] GetAttributes(bool inherit)
        {
            return _Acessor.MemberInfo.GetCustomAttributes(inherit);
        }

        public object[] GetAttributes(Type attributeType, bool inherit)
        {
            return _Acessor.MemberInfo.GetCustomAttributes(attributeType, inherit);
        }

        public TAttribute[] GetAttributes<TAttribute>(bool inherit)
        {
            object[] obj = _Acessor.MemberInfo.GetCustomAttributes(typeof(TAttribute), inherit);
            TAttribute[] arr = new TAttribute[obj.Length];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = (TAttribute)obj[i];
            return arr;
        }

    }
}
