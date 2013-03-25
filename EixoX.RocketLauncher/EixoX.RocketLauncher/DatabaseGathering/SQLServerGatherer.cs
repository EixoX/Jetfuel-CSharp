using EixoX.RocketLauncher.DatabaseGathering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    public class SQLServerGatherer : IDbInformationGather
    {
        private readonly DatabaseCredentials Credentials;
        private EixoX.Data.SqlServer SqlServer;

        public static DatabaseCredentials Parse(string connectionString)
        {
            DatabaseCredentials credentials = new DatabaseCredentials();
            string[] configs = connectionString.Split(';');
            
            foreach (string config in configs)
            {
                string[] keyValue = config.Split('=');
                if (keyValue[0].Equals("server", StringComparison.OrdinalIgnoreCase))
                    credentials.Server = keyValue[1];
                else if (keyValue[0].Equals("database", StringComparison.OrdinalIgnoreCase))
                    credentials.Database = keyValue[1];
                else if (keyValue[0].Equals("user id", StringComparison.OrdinalIgnoreCase))
                    credentials.UserId = keyValue[1];
                else if (keyValue[0].Equals("password", StringComparison.OrdinalIgnoreCase))
                    credentials.Password = keyValue[1];
                else if (keyValue[0].Equals("initial catalog", StringComparison.OrdinalIgnoreCase))
                    credentials.Database = keyValue[1];
                else if (keyValue[0].Equals("data source", StringComparison.OrdinalIgnoreCase))
                    credentials.Server = keyValue[1];
            }

            return credentials;
        }

        public SQLServerGatherer(string connectionString)
        {
            this.Credentials = Parse(connectionString);
            this.SqlServer = new Data.SqlServer(connectionString);
        }

        public IEnumerable<GenericDatabaseColumn> GetColumns(string table)
        {
            StringBuilder query = new StringBuilder();
            query.Append("USE [");
            query.Append(this.Credentials.Database);
            query.Append("] SELECT *, columnproperty(object_id(table_name), column_name, 'IsIdentity') as is_identity FROM information_schema.columns WHERE TABLE_CATALOG = '");
            query.Append(this.Credentials.Database);
            query.Append("' AND TABLE_NAME='");
            query.Append(table);
            query.Append("'");

            foreach (System.Data.IDataRecord record in SqlServer.ExecuteQueryText(query.ToString()))
            {
                GenericDatabaseColumn column = new GenericDatabaseColumn();
                column.Name = GetValue("COLUMN_NAME", record).ToString();
                column.ColumnDefault = (string) GetValue("COLUMN_DEFAULT", record);
                column.DataType = InferType((string) GetValue("DATA_TYPE", record));
                column.IsNullable = ((string) GetValue("IS_NULLABLE", record)).Equals("yes", StringComparison.OrdinalIgnoreCase);
                column.MaxLength = Convert.ToInt32(GetValue("CHARACTER_MAXIMUM_LENGTH", record));
                column.OrdinalPosition = Convert.ToInt32(GetValue("ORDINAL_POSITION", record));
                column.IsIdentity = Convert.ToBoolean(GetValue("is_identity", record));

                yield return column;
            }
        }

        private Type InferType(string dataType)
        {
            if (dataType.Equals("nvarchar", StringComparison.OrdinalIgnoreCase) ||
                dataType.Equals("varchar", StringComparison.OrdinalIgnoreCase) ||
                dataType.Equals("text", StringComparison.OrdinalIgnoreCase))
                return typeof(String);
            else if (dataType.Equals("int", StringComparison.OrdinalIgnoreCase))
                return typeof(int);
            else if (dataType.Equals("datetime", StringComparison.OrdinalIgnoreCase))
                return typeof(DateTime);
            else if (dataType.Equals("double", StringComparison.OrdinalIgnoreCase) ||
                     dataType.Equals("money", StringComparison.OrdinalIgnoreCase))
                return typeof(double);
            else if (dataType.Equals("bit", StringComparison.OrdinalIgnoreCase))
                return typeof(bool);
            else if (dataType.Equals("sql_variant", StringComparison.OrdinalIgnoreCase))
                return typeof(object);

            return null;
        }

        private object GetValue(string columnName, System.Data.IDataRecord record)
        {
            object value = record.GetValue(record.GetOrdinal(columnName));

            if (value is DBNull)
                return null;
            
            return value;
        }

        public IEnumerable<GenericDatabaseTable> GetTables(string database)
        {
            StringBuilder query = new StringBuilder();
            query.Append("USE [");
            query.Append(this.Credentials.Database);
            query.Append("] SELECT object_id(table_name) as object_id, * FROM information_schema.tables");

            foreach (System.Data.IDataRecord record in SqlServer.ExecuteQueryText(query.ToString()))
            {
                yield return new GenericDatabaseTable()
                {
                    Name = record.GetString(record.GetOrdinal("TABLE_NAME")),
                    DatabaseName = record.GetString(record.GetOrdinal("TABLE_CATALOG")),
                    Schema = record.GetString(record.GetOrdinal("TABLE_SCHEMA")),
                    Type = record.GetString(record.GetOrdinal("TABLE_TYPE")),
                    ObjectId = record.GetInt32(record.GetOrdinal("object_id"))
                };
            }
        }

        public IEnumerable<GenericDatabaseTable> GetTables()
        {
            return GetTables(this.Credentials.Database);
        }
    }
}
