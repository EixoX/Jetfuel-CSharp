using System;

namespace EixoX.Data
{
    /// <summary>
    /// Represents a column;
    /// </summary>
    public interface Column
    {

        /// <summary>
        /// Gets the column id. The identifier of the column on the data source.
        /// </summary>
        public object ColumnId { get; }

        /// <summary>
        /// Gets the data name of the column. As represented on the data source.
        /// </summary>
        string ColumnName { get; }

        /// <summary>
        /// Gets the type of the column: normal, identity etc.
        /// </summary>
        ColumnType ColumnType { get; }


    }
}
