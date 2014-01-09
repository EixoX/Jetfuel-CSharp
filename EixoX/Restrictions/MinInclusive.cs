using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MinInclusive : Attribute, Restriction
    {
        private readonly IComparable _Value;

        public MinInclusive(int value) { this._Value = value; }
        public MinInclusive(long value) { this._Value = value; }
        public MinInclusive(float value) { this._Value = value; }
        public MinInclusive(double value) { this._Value = value; }
        public MinInclusive(decimal value) { this._Value = value; }
        public MinInclusive(IComparable value) { this._Value = value; }

        public IComparable Value { get { return this._Value; } }

        public bool Validate(object input)
        {
            return _Value.CompareTo(Convert.ChangeType(input, _Value.GetType())) <= 0;
        }

        public string RestrictionMessageFormat
        {
            get { return "Value lower than the minimum (inclusive) of " + _Value; }
        }

        public override string ToString()
        {
            return this.RestrictionMessageFormat;
        }
    }
}
