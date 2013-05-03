using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Database
{
    public class DatabaseAspectMember
        : EixoX.Data.DataAspectMember
    {
        private readonly DatabaseAspectAdapter _Adapter;
        private readonly System.Data.DbType _DbType;

        public DatabaseAspectMember(ClassAcessor acessor, string storedName, bool identity, bool unique, bool primaryKey, bool nullable, Data.Generator generator, DatabaseAspectAdapter adapter)
            : base(acessor, storedName, identity, unique, primaryKey, nullable, generator)
        {
            this._Adapter = adapter;
        }


        public System.Data.DbType DbType
        {
            get { return this._DbType; }
        }
    }
}
