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


        public ClassSelect<T> Search(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                filter = "%";
            else if (filter.Length < 3)
                filter = filter + "%";
            else
                filter = "%" + filter.Replace(' ', '%') + "%";

            Type stringType = typeof(string);
            ClassFilterExpression expression = null;
            DataAspect dataAspect = base.Aspect;
            int count = dataAspect.Count;
            for (int i = 0; i < count; i++)
            {
                //if (dataAspect[i].DataType == stringType)
                    expression =
                        expression == null ?
                        new ClassFilterExpression(dataAspect, i, FilterComparison.Like, filter) :
                        expression.Or(i, FilterComparison.Like, filter);
            }


            return Select().Where(expression);
        }

    }
}
