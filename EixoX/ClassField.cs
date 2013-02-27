using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
namespace EixoX
{
    public struct ClassField : ClassAcessor
    {
        private readonly FieldInfo _Field;

        public ClassField(FieldInfo field)
        {
            this._Field = field;
        }

        public Type DataType
        {
            get { return this._Field.FieldType; }
        }

        public string Name
        {
            get { return this._Field.Name; }
        }

        public MemberInfo MemberInfo
        {
            get { return this._Field; }
        }

        public object GetValue(object entity)
        {
            return this._Field.GetValue(entity);
        }

        public void SetValue(object entity, object value)
        {
            this._Field.SetValue(entity, value);
        }
    }
}
