using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    /// <summary>
    /// Interface for database gatherer's classes
    /// </summary>
    public interface IDbInformationGather
    {
        /// <summary>
        /// Lists all columns of a table
        /// </summary>
        /// <param name="table">the table's name</param>
        /// <returns>enumeration of all columns</returns>
        IEnumerable<GenericDatabaseColumn> GetColumns(string table);
        /// <summary>
        /// Lists all tables of a given database
        /// </summary>
        /// <param name="database">tha database name</param>
        /// <returns>an enumeration of all tables found</returns>
        IEnumerable<GenericDatabaseTable> GetTables(string database);
    }
}
