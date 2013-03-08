using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class Enumeration : Attribute
    {
        private readonly object _Key;
        private readonly string _Name;

        public Enumeration(object key, string name)
        {
            this._Key = key;
            this._Name = name;
        }

        public Enumeration(object key)
            : this(key, key.ToString()) { }

        public object Key { get { return this._Key; } }
        public string Name { get { return this._Name; } }
    }
}
