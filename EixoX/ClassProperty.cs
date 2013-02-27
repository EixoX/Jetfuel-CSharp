using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace EixoX
{
    public class ClassProperty : ClassAcessor
    {
        private readonly PropertyInfo _Property;

        public ClassProperty(PropertyInfo property)
        {
            this._Property = property;
        }

        public Type DataType
        {
            get { return this._Property.PropertyType; }
        }

        public string Name
        {
            get { return this._Property.Name; }
        }

        public MemberInfo MemberInfo
        {
            get { return this._Property; }
        }

        public object GetValue(object entity)
        {
            return this._Property.GetValue(entity, null);
        }

        public void SetValue(object entity, object value)
        {
            this._Property.SetValue(entity, value, null);
        }
    }
}
