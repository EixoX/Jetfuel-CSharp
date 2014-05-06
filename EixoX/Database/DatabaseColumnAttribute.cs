using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class DatabaseColumnAttribute : Attribute
    {
        public DatabaseColumnAttribute()
        {
            this.ColumnKind = DatabaseColumnKind.Normal;
        }

        public DatabaseColumnAttribute(DatabaseColumnKind columnKind)
        {
            this.ColumnKind = columnKind;
        }

        public DatabaseColumnAttribute(DatabaseColumnKind columnKind, string storedName)
        {
            this.ColumnKind = columnKind;
            this.StoredName = storedName;
        }

        public DatabaseColumnAttribute(DatabaseColumnKind columnKind, string storedName, bool nullable)
        {
            this.ColumnKind = columnKind;
            this.StoredName = storedName;
            this.Nullable = nullable;
        }

        public DatabaseColumnAttribute(DatabaseColumnKind columnKind, bool nullable)
        {
            this.ColumnKind = columnKind;
            this.Nullable = nullable;
        }


        public DatabaseColumnAttribute(bool nullable)
        {
            this.ColumnKind = DatabaseColumnKind.Normal;
            this.Nullable = nullable;
        }

        public DatabaseColumnAttribute(string name)
        {
            this.ColumnKind = DatabaseColumnKind.Normal;
            this.Nullable = true;
            this.StoredName = name;
        }


        public DatabaseColumnKind ColumnKind { get; set; }
        public string StoredName { get; set; }
        public bool Nullable { get; set; }
        public string Caption { get; set; }
    }
}
