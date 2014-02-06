using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    /// <summary>
    /// Represents a table stored on a data source.
    /// </summary>
    public interface Table
    {
        object TableId { get; }
        string TableName { get; }

    }
}
