using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using EixoX.Database;

namespace EixoX.Data
{
    public class DatabaseStorage<T>
        : ClassStorage<T>
    {
        private readonly Database.DbEngine _Database;

        public DatabaseStorage(Database.DbEngine database, DataAspect aspect)
            : base(database, aspect)
        {
            this._Database = database;
        }

        public Database.DbEngine Database { get { return this._Database; } }


        public IEnumerable<T> Query(CommandType commandType, string commandText, params object[] commandParameters)
        {
            return Aspect.Transform<T>(_Database.ExecuteQuery(commandType, commandText, commandParameters));
        }


    }
}
