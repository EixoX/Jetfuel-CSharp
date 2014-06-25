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
            return Aspect.Transform<T>(_Database.ExecuteQuery(commandType, commandText, commandParameters));
        }


        public DataTable QueryTable(CommandType commandType, string commandText, params object[] commandParameters)
        {
            return _Database.ExecuteTable(commandType, commandText, commandParameters);
        }

    }
}
