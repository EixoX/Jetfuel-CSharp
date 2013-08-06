using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Restrictions
{
    /// <summary>
    /// Represents the annotation of an enumeration value.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class Enumeration : Attribute
    {
        private readonly object _Key;
        private readonly string _Name;

        /// <summary>
        /// Constructs the enumeration item.
        /// </summary>
        /// <param name="key">The key to compare to.</param>
        /// <param name="name">The name of the item.</param>
        public Enumeration(object key, string name)
        {
            this._Key = key;
            this._Name = name;
        }

        /// <summary>
        /// Constructs the enumeration item.
        /// </summary>
        /// <param name="key">The key to use as both comparison and name.</param>
        public Enumeration(object key)
            : this(key, key.ToString()) { }

        /// <summary>
        /// Gets the key of the enumeration item.
        /// </summary>
        public object Key { get { return this._Key; } }
        /// <summary>
        /// Gets the name of the enumeration item.
        /// </summary>
        public string Name { get { return this._Name; } }

        /// <summary>
        /// Gets a string representation of the enumeration item.
        /// </summary>
        /// <returns>The string representation of the enumeration item.</returns>
        public override string ToString()
        {
            return string.Concat(_Key, " (", _Name, ")");
        }
    }
}
