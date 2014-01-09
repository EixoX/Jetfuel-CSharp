using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    /// <summary>
    /// Represents a pair with a key and a value.
    /// </summary>
    public interface Pair
    {
        /// <summary>
        /// The key of the pair.
        /// </summary>
        object Key { get; }
        /// <summary>
        /// The value of the pair.
        /// </summary>
        object Value { get; }
    }

    [Serializable]
    public class Pair<TKey, TValue>: Pair
    {
        private TKey _Key;
        private TValue _Value;

        public Pair() { }
        public Pair(TKey name, TValue value)
        {
            this._Key = name;
            this._Value = value;
        }


        public TKey Key
        {
            get { return this._Key; }
            set { this._Key = value; }
        }

        public TValue Value
        {
            get { return this._Value; }
            set { this._Value = value; }
        }

        public override string ToString()
        {
            return string.Concat(this._Key, " = ", this._Value);
        }

        object Pair.Key
        {
            get { return this._Key; }
        }

        object Pair.Value
        {
            get { return this._Value; }
        }
    }
}
