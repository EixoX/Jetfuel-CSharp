using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EixoX.Data
{
    /// <summary>
    /// Used to add column attributes to a given field or property;
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnAttribute : Attribute, Column
    {

        /// <summary>
        /// Gets or set the column id.
        /// </summary>
        public object ColumnId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the column name.
        /// </summary>
        public string ColumnName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the column type.
        /// </summary>
        public ColumnType ColumnType
        {
            get;
            set;
        }

    }
}
