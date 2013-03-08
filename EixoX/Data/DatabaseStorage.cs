using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace EixoX.Data
{
    public class DatabaseStorage<T>
        : ClassStorage<T>
    {
        private readonly Database _Database;

        public DatabaseStorage(Database database, DataAspect aspect)
            : base(database, aspect)
        {
            this._Database = database;
        }

        public Database Database { get { return this._Database; } }


        public IEnumerable<T> Query(CommandType commandType, string commandText, params object[] commandParameters)
        {
            using (IEnumerator<IDataRecord> records = _Database.ExecuteQuery(commandType, commandText, commandParameters).GetEnumerator())
            {
                if (records.MoveNext())
                {
                    DataAspect aspect = this.Aspect;
                    int fieldCount = records.Current.FieldCount;
                    bool initializable = typeof(Initializable).IsAssignableFrom(aspect.DataType);
                    DataMember[] members = new DataMember[fieldCount];

                    for (int i = 0; i < fieldCount; i++)
                    {
                        int ordinal = aspect.GetStoredNameOrdinal(records.Current.GetName(i));
                        if (ordinal >= 0)
                            members[i] = aspect[ordinal];
                    }

                    do
                    {
                        T entity = (T)aspect.NewInstance();
                        for (int i = 0; i < fieldCount; i++)
                            if (members[i] != null && !records.Current.IsDBNull(i))
                                members[i].SetValue(entity, records.Current.GetValue(i));

                        if (initializable)
                            ((Initializable)entity).Initialize();

                        yield return entity;

                    } while (records.MoveNext());
                }
            }
        }

    }
}
