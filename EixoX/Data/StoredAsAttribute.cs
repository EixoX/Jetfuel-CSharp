using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class StoredAsAttribute: Attribute
    {
        private readonly string _Name;

        public StoredAsAttribute(string name)
        {
            this._Name = name;
        }

        public string Name { get { return this._Name; } }

        public override string ToString()
        {
            return "Stored as " + _Name;
        }
    }
}
