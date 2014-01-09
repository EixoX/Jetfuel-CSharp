using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MinExclusive : Attribute, Restriction
    {
        private readonly IComparable _Value;

        public MinExclusive(int value) { this._Value = value; }
        public MinExclusive(long value) { this._Value = value; }
        public MinExclusive(float value) { this._Value = value; }
        public MinExclusive(double value) { this._Value = value; }
        public MinExclusive(decimal value) { this._Value = value; }
        public MinExclusive(IComparable value)
        {
            this._Value = value;
        }

        public IComparable Value { get { return this._Value; } }

        public bool Validate(object input)
        {
            return _Value.CompareTo(Convert.ChangeType(input, _Value.GetType())) < 0;
        }

        public string RestrictionMessageFormat
        {
            get { return "Value is below the Minimum (Exclusive) of " + _Value.ToString(); }
        }


        public override string ToString()
        {
            return this.RestrictionMessageFormat;
        }

    }
}
