using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EixoX.Data
{
    public class ColumnMappingProperty : ColumnMapping
    {
        private readonly PropertyInfo _Property;
        private readonly string _ColumnName;
        private readonly ColumnType _ColumnType;
        private readonly object _ColumnId;

        public ColumnMappingProperty(PropertyInfo Property, ColumnAttribute attribute)
        {
            this._Property = Property;
            this._ColumnName = string.IsNullOrEmpty(attribute.ColumnName) ? Property.Name : attribute.ColumnName;
            this._ColumnType = attribute.ColumnType;
            this._ColumnId = attribute.ColumnId;
        }


        public object GetValue(object entity)
        {
            return this._Property.GetValue(entity, null);
        }

        public void SetValue(object entity, object value)
        {
            this._Property.SetValue(entity, value, null);
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
