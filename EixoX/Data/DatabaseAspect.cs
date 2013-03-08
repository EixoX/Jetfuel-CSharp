using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public class DatabaseAspect
        : DataAspect
    {
        public DatabaseAspect(Type dataType) : base(dataType) { }

        protected override bool CreateAspectFor(Reflection.ClassAcessor acessor, out DataMember member)
        {
            DatabaseColumnAttribute dca = acessor.GetAttribute<DatabaseColumnAttribute>(true);
            if (dca == null)
            {
                member = null;
                return false;
            }
            else
            {
                member = new DataMember(
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
