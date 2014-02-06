using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EixoX.Data
{
    public class ColumnMappingField : ColumnMapping
    {
        private readonly FieldInfo _Field;
        private readonly string _ColumnName;
        private readonly ColumnType _ColumnType;
        private readonly object _ColumnId;

        public ColumnMappingField(FieldInfo field, ColumnAttribute attribute)
        {
            this._Field = field;
            this._ColumnName = string.IsNullOrEmpty(attribute.ColumnName) ? field.Name : attribute.ColumnName;
            this._ColumnType = attribute.ColumnType;
            this._ColumnId = attribute.ColumnId;
        }


        public object GetValue(object entity)
        {
            return this._Field.GetValue(entity);
        }

        public void SetValue(object entity, object value)
        {
            this._Field.SetValue(entity, value);
        }

        public object ColumnId
        {
            get { return this._ColumnId; }
        }

        public string ColumnName
        {
            get { return this._ColumnName; }
        }

        public ColumnType ColumnType
        {
            get { return this._ColumnType; }
        }
    }
}
