using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using EixoX.Data;

namespace EixoX.Database
{
    public class DbEngine : ClassStorageEngine
    {
        private readonly DbDialect _dialect;
        private readonly string _ConnectionString;

        public DbEngine(DbDialect dialect, string connectionString)
        {
            this._dialect = dialect;
            this._ConnectionString = connectionString;
        }

        public DbDialect Dialect { get { return this._dialect; } }
        public string ConnectionString { get { return this._ConnectionString; } }

        public int ExecuteNonQuery(CommandType commandType, string commandText, params object[] commandParameters)
        {
            using (IDbConnection conn = _dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    using (IDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = commandType;
                        cmd.CommandText = commandText;
                        for (int i = 0; i < commandParameters.Length; i++)
                            cmd.Parameters.Add(commandParameters[i]);

                        return cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public int ExecuteNonQueryText(string commandText, params object[] commandParameters)
        {
            return ExecuteNonQuery(CommandType.Text, commandText, commandParameters);
        }
        public int ExecuteNonQueryProc(string procName, params object[] commandParameters)
        {
            return ExecuteNonQuery(CommandType.StoredProcedure, procName, commandParameters);
        }

        public object ExecuteScalar(CommandType commandType, string commandText, params object[] commandParameters)
        {
            using (IDbConnection conn = _dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    using (IDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = commandType;
                        cmd.CommandText = commandText;
                        for (int i = 0; i < commandParameters.Length; i++)
                            cmd.Parameters.Add(commandParameters[i]);

                        return cmd.ExecuteScalar();
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public object ExecuteScalarText(string comamndText, params object[] commandParameters)
        {
            return ExecuteScalar(CommandType.Text, comamndText, commandParameters);
        }
        public object ExecuteScalarProc(string procName, params object[] commandParameters)
        {
            return ExecuteScalar(CommandType.StoredProcedure, procName, commandParameters);
        }

        public IEnumerable<IDataRecord> ExecuteQuery(CommandType commandType, string commandText, params object[] commandParameters)
        {
            using (IDbConnection conn = _dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    using (IDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = commandType;
                        cmd.CommandText = commandText;
                        for (int i = 0; i < commandParameters.Length; i++)
                            cmd.Parameters.Add(commandParameters[i]);

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
                finally
                {
                    conn.Close();
                }
            }
        }
        public IEnumerable<IDataRecord> ExecuteQueryText(string commandText, params object[] commandParameters)
        {
            return ExecuteQuery(CommandType.Text, commandText, commandParameters);
        }
        public IEnumerable<IDataRecord> ExecuteQueryProc(string procName, params object[] commandParameters)
        {
            return ExecuteQuery(CommandType.StoredProcedure, procName, commandParameters);
        }

        public DataTable ExecuteTable(CommandType commandType, string commandText, params object[] commandParameters)
        {
            using (IDbConnection conn = _dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    return new DatabaseCommand(commandType, commandText, commandParameters).ExecuteTable(conn);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public DataTable ExecuteTableText(string commandText, params object[] commandParameters)
        {
            return ExecuteTable(CommandType.Text, commandText, commandParameters);
        }
        public DataTable ExecuteTableProc(string procName, params object[] commandParameters)
        {
            return ExecuteTable(CommandType.StoredProcedure, procName, commandParameters);
        }

        public virtual DataTable GetSchema()
        {
            using (System.Data.Common.DbConnection conn = (System.Data.Common.DbConnection)_dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.GetSchema();
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public virtual DataTable GetSchema(string collectionName, params string[] restrictionValues)
        {
            using (System.Data.Common.DbConnection conn = (System.Data.Common.DbConnection)_dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    return restrictionValues != null && restrictionValues.Length > 0 ?
                        conn.GetSchema(collectionName, restrictionValues) :
                        conn.GetSchema(collectionName);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public int Delete(DataAspect aspect, ClassFilter filter)
        {
            using (IDbConnection conn = _dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    return _dialect.CreateDelete(aspect, filter).ExecuteNonQuery(conn);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public int Insert(DataAspect aspect, IEnumerable<AspectMemberValue> values, out object identityValue)
        {
            using (IDbConnection conn = _dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    bool hasIdentity;
                    DatabaseCommand cmd = _dialect.CreateInsert(aspect, values, out hasIdentity);
                    if (hasIdentity)
                    {
                        identityValue = cmd.ExecuteScalar(conn);
                        return 1;
                    }
                    else
                    {
                        identityValue = null;
                        return cmd.ExecuteNonQuery(conn);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public int Insert(DataAspect aspect, System.Collections.IEnumerable entities)
        {
            using (IDbConnection conn = _dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    return _dialect.CreateInsert(aspect, entities).ExecuteNonQuery(conn);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public int Update(DataAspect aspect, IEnumerable<AspectMemberValue> values, ClassFilter filter)
        {
            using (IDbConnection conn = _dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    return _dialect.CreateUpdate(aspect, values, filter).ExecuteNonQuery(conn);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IEnumerable<T> Select<T>(DataAspect aspect, ClassFilter filter, ClassSort sort, int pageSize, int pageOrdinal)
        {
            using (IDbConnection conn = _dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    IEnumerable<IDataRecord> records = _dialect.CreateSelect(aspect, filter, sort, pageSize, pageOrdinal).ExecuteQuery(conn);
                    if (pageSize > 0 && pageOrdinal >= 0)
                    {
                        if (!_dialect.CanOffsetRecords)
                        {
                            if (!_dialect.CanLimitRecords)
                                records = new Collections.EnumerablePager<IDataRecord>(records, pageSize, pageOrdinal);
                            else
                                records = new Collections.EnumerableOffset<IDataRecord>(records, pageSize * pageOrdinal);
                        }
                        else if (!_dialect.CanLimitRecords)
                            records = new Collections.EnumerableLimit<IDataRecord>(records, pageSize);
                    }

                    using (IEnumerator<IDataRecord> e = records.GetEnumerator())
                    {
                        if (e.MoveNext())
                        {
                            bool initializable = typeof(Initializable).IsAssignableFrom(aspect.DataType);
                            int count = e.Current.FieldCount;

                            do
                            {
                                T entity = (T)aspect.NewInstance();
                                for (int i = 0; i < count; i++)
                                    if (!e.Current.IsDBNull(i))
                                        aspect[i].SetValue(entity, e.Current.GetValue(i));
                                if (initializable)
                                    ((Initializable)entity).Initialize();
                                yield return entity;
                            } while (e.MoveNext());
                        }
                    }

                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IEnumerable<object> SelectMember(DataAspect aspect, int ordinal, ClassFilter filter, ClassSort sort, int pageSize, int pageOrdinal)
        {
            using (IDbConnection conn = _dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    IEnumerable<IDataRecord> records = _dialect.CreateSelectMember(aspect, ordinal, filter, sort, pageSize, pageOrdinal).ExecuteQuery(conn);
                    if (pageSize > 0 && pageOrdinal >= 0)
                    {
                        if (!_dialect.CanOffsetRecords)
                        {
                            if (!_dialect.CanLimitRecords)
                                records = new Collections.EnumerablePager<IDataRecord>(records, pageSize, pageOrdinal);
                            else
                                records = new Collections.EnumerableOffset<IDataRecord>(records, pageSize * pageOrdinal);
                        }
                        else if (!_dialect.CanLimitRecords)
                            records = new Collections.EnumerableLimit<IDataRecord>(records, pageSize);
                    }

                    foreach (IDataRecord record in records)
                        yield return record.GetValue(0);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public object GetMemberValue(DataAspect aspect, int ordinal, ClassFilter filter)
        {
            using (IDbConnection conn = _dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    return _dialect.CreateSelectMember(aspect, ordinal, filter, null, 0, 0).ExecuteScalar(conn);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool Exists(DataAspect aspect, ClassFilter filter)
        {
            using (IDbConnection conn = _dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    return Convert.ToInt32(_dialect.CreateSelectExists(aspect, filter).ExecuteScalar(conn)) == 1;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public long Count(DataAspect aspect, ClassFilter filter)
        {
            using (IDbConnection conn = _dialect.CreateConnection(_ConnectionString))
            {
                conn.Open();
                try
                {
                    return Convert.ToInt64(_dialect.CreateSelectCount(aspect, filter).ExecuteScalar(conn));
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public ClassFilter CreateSearchFilter(DataAspect aspect, string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return null;
            else if (filter.Length < 3)
                filter = filter + "%";
            else
                filter = "%" + filter.Replace(' ', '%') + "%";

            Type stringType = typeof(string);
            ClassFilterExpression expression = null;
            DataAspect dataAspect = aspect;
            int count = dataAspect.Count;
            for (int i = 0; i < count; i++)
            {
                //if (dataAspect[i].DataType == stringType)
                expression =
                    expression == null ?
                    new ClassFilterExpression(dataAspect, i, FilterComparison.Like, filter) :
                    expression.Or(i, FilterComparison.Like, filter);
            }

            return expression;
        }
    }
}
