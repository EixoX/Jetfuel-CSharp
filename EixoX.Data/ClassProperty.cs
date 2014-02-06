using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EixoX.Data
{
    public sealed class ClassProperty : ClassAcessor
    {
        private readonly PropertyInfo _Property;

        public ClassProperty(PropertyInfo property)
        {
            this._Property = property;
        }

        public object GetValue(object entity)
        {
            return _Property.GetValue(entity, null);
        }

        public void SetValue(object entity, object value)
        {
            _Property.SetValue(entity, value, null);
        }

        public object[] GetCustomAttributes(bool inherit)
        {
            return _Property.GetCustomAttributes(inherit);
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return _Property.GetCustomAttributes(attributeType, inherit);
        }

        public bool IsDefined(Type attributeType, bool inherit)
        {
            return _Property.IsDefined(attributeType, inherit);
        }
    }
}
