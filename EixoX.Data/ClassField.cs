using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EixoX.Data
{
    public sealed class ClassField : ClassAcessor
    {
        private readonly FieldInfo _Field;

        public ClassField(FieldInfo field)
        {
            this._Field = field;
        }

        public object GetValue(object entity)
        {
            return _Field.GetValue(entity);
        }

        public void SetValue(object entity, object value)
        {
            _Field.SetValue(entity, value);
        }

        public object[] GetCustomAttributes(bool inherit)
        {
            return _Field.GetCustomAttributes(inherit);
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return _Field.GetCustomAttributes(attributeType, inherit);
        }

        public bool IsDefined(Type attributeType, bool inherit)
        {
            return _Field.IsDefined(attributeType, inherit);
        }
    }
}
