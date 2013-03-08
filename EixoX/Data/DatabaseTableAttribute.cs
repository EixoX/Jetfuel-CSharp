using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
    public class DatabaseTableAttribute : Attribute
    {
        private readonly string _Name;

        public DatabaseTableAttribute(string name)
        {
            this._Name = name;
        }

        public DatabaseTableAttribute() { }

        public string Name { get { return this._Name; } }
    }
}
