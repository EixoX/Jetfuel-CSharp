using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public class DatabaseAspect
        : DataAspect
    {

        public DatabaseAspect(Type dataType) : base(dataType) { }

        protected override string GetStoredName(Type dataType)
        {
            object[] obj = dataType.GetCustomAttributes(typeof(DatabaseTableAttribute), true);
            if (obj == null || obj.Length < 1)
                throw new ArgumentException(dataType.FullName + " needs to be annotated with a database table attribute.");

            string name = ((DatabaseTableAttribute)obj[0]).Name;
            return string.IsNullOrEmpty(name) ? dataType.Name : name;

        }

        protected override bool CreateAspectFor(ClassAcessor acessor, out DataAspectMember member)
        {
            DatabaseColumnAttribute dca = acessor.GetAttribute<DatabaseColumnAttribute>(true);
            if (dca == null)
            {
                member = null;
                return false;
            }
            else
            {
                member = new DataAspectMember(
                    acessor,
                    dca.StoredName,
                    dca.ColumnKind == DatabaseColumnKind.Identity,
                    dca.ColumnKind == DatabaseColumnKind.Unique,
                    dca.ColumnKind == DatabaseColumnKind.PrimaryKey,
                    dca.IsNullable,
                    acessor.GetAttribute<Generator>(true));

                return true;
            }
        }
    }

    public class DatabaseAspect<T> : DatabaseAspect
    {
        private static DatabaseAspect<T> _Instance;
        private DatabaseAspect() : base(typeof(T)) { }

        public static DatabaseAspect<T> Instance
        {
            get { return _Instance ?? (_Instance = new DatabaseAspect<T>()); }
        }
    }
}
