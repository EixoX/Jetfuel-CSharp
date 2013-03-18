using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace EixoX.Data
{
    public struct DatabaseCommand
    {
        private readonly CommandType _CommandType;
        private readonly string _CommandText;
        private readonly IEnumerable<object> _CommandParameters;

        public DatabaseCommand(CommandType commandType, string commandText, IEnumerable<object> commandParameters)
        {
            this._CommandType = commandType;
            this._CommandText = commandText;
            this._CommandParameters = commandParameters;
        }
        public DatabaseCommand(CommandType commandType, string commandText, params object[] commandParameters)
        {
            this._CommandType = commandType;
            this._CommandText = commandText;
            this._CommandParameters = commandParameters;
        }


        public CommandType CommandType
        {
            get { return this._CommandType; }
        }

        public string CommandText
        {
            get { return this._CommandText; }
        }

        public IEnumerable<object> CommandParameters
        {
            get { return this._CommandParameters; }
        }


        public int ExecuteNonQuery(IDbConnection connection)
        {
            using (IDbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandType = this._CommandType;
                cmd.CommandText = this._CommandText;
                if (_CommandParameters != null)
                    foreach (object par in _CommandParameters)
                        cmd.Parameters.Add(par);

                return cmd.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(IDbConnection connection)
        {
            using (IDbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandType = this._CommandType;
                cmd.CommandText = this._CommandText;
                if (_CommandParameters != null)
                    foreach (object par in _CommandParameters)
                        cmd.Parameters.Add(par);

                return cmd.ExecuteScalar();
            }
        }

        public IEnumerable<IDataRecord> ExecuteQuery(IDbConnection connection)
        {
            using (IDbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandType = this._CommandType;
                cmd.CommandText = this._CommandText;
                if (_CommandParameters != null)
                    foreach (object par in _CommandParameters)
                        cmd.Parameters.Add(par);

                using (IDataReader reader = cmd.ExecuteReader())
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
        }

        public DataTable ExecuteTable(IDbConnection connection)
        {
            DataTable table = null;

            using (IDbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandType = this._CommandType;
                cmd.CommandText = this._CommandText;
                if (_CommandParameters != null)
                    foreach (object par in _CommandParameters)
                        cmd.Parameters.Add(par);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    try
                    {
                        table = new DataTable();
                        for (int i = 0; i < reader.FieldCount; i++)
                            table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                        object[] row = new object[reader.FieldCount];

                        while (reader.Read())
                        {
                            reader.GetValues(row);
                            table.Rows.Add(row);
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }

            return table;
        }
    }
}
