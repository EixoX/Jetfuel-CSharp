using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class DatabaseColumnAttribute : Attribute
    {
        private readonly DatabaseColumnKind _columnKind;
        private readonly string _storedName;
        private readonly bool _nullable;

        public DatabaseColumnAttribute()
        {
            this._columnKind = DatabaseColumnKind.Normal;
        }

        public DatabaseColumnAttribute(DatabaseColumnKind columnKind)
        {
            this._columnKind = columnKind;
        }

        public DatabaseColumnAttribute(DatabaseColumnKind columnKind, string storedName)
        {
            this._columnKind = columnKind;
            this._storedName = storedName;
        }

        public DatabaseColumnAttribute(DatabaseColumnKind columnKind, string storedName, bool nullable)
        {
            this._columnKind = columnKind;
            this._storedName = storedName;
            this._nullable = nullable;
        }

        public DatabaseColumnAttribute(DatabaseColumnKind columnKind, bool nullable)
        {
            this._columnKind = columnKind;
            this._nullable = nullable;
        }


        public DatabaseColumnAttribute(bool nullable)
        {
            this._columnKind = DatabaseColumnKind.Normal;
            this._nullable = nullable;
        }


        public DatabaseColumnKind ColumnKind
        {
            get { return this._columnKind; }
        }

        public string StoredName
        {
            get { return this._storedName; }
        }

        public bool IsNullable
        {
            get { return this._nullable; }
        }
    }
}
