using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.Data.Database
{
    /// <summary>
    /// Represents any database.
    /// </summary>
    public abstract class Db
    {
        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the database connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Creates a database connection.
        /// </summary>
        /// <returns>A database connection.</returns>
        public abstract DbConnection CreateConnection();

        /// <summary>
        /// Executes a non query command.
        /// </summary>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="commandText">The text of the command.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText, params object[] commandParameters)
        {
            using (DbConnection conn = CreateConnection())
            {
                conn.Open();
                try
                {
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = commandText;
                            cmd.CommandType = commandType;
                            cmd.Parameters.AddRange(commandParameters);

                            return cmd.ExecuteNonQuery();
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Executes a scalar command.
        /// </summary>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <returns>The scalar result.</returns>
        public object ExecuteScalar(CommandType commandType, string commandText, params object[] commandParameters)
        {
            using (DbConnection conn = CreateConnection())
            {
                conn.Open();
                try
                {
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = commandText;
                            cmd.CommandType = commandType;
                            cmd.Parameters.AddRange(commandParameters);

                            return cmd.ExecuteScalar();
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Executes a query against the database.
        /// </summary>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <returns></returns>
        public IEnumerable<IDataRecord> ExecuteQuery(CommandType commandType, string commandText, params object[] commandParameters)
        {
            using (DbConnection conn = CreateConnection())
            {
                conn.Open();
                try
                {
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = commandText;
                            cmd.CommandType = commandType;
                            cmd.Parameters.AddRange(commandParameters);

                            using (DbDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    while (reader.Read())
                                        yield return reader;
                                }
                                finally
                                {
                                    reader.Close();
                                }

                            }
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Executes a query against the database.
        /// </summary>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <returns>A data table with the result.</returns>
        public DataTable ExecuteQueryAsTable(CommandType commandType, string commandText, params object[] commandParameters)
        {
            using (DbConnection conn = CreateConnection())
            {
                conn.Open();
                try
                {
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = commandText;
                            cmd.CommandType = commandType;
                            cmd.Parameters.AddRange(commandParameters);

                            using (DbDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    DataTable table = new DataTable();

                                    int fieldCount = reader.FieldCount;
                                    for (int i = 0; i < fieldCount; i++)
                                        table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));

                                    if (reader.Read())
                                    {
                                        object[] rowData = new object[fieldCount];

                                        do
                                        {
                                            reader.GetValues(rowData);
                                            table.Rows.Add(rowData);

                                        } while (reader.Read());
                                    }
                                    
                                    return table;
                                }
                                finally
                                {
                                    reader.Close();
                                }
                            }
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }

                }
                finally
                {
                    conn.Close();
                }
            }
        }
    
    


    }
}
