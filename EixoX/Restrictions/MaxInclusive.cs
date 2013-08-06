using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MaxInclusive : Attribute, Restriction
    {
        private readonly IComparable _Value;

        public MaxInclusive(int value) { this._Value = value; }
        public MaxInclusive(long value) { this._Value = value; }
        public MaxInclusive(float value) { this._Value = value; }
        public MaxInclusive(double value) { this._Value = value; }
        public MaxInclusive(decimal value) { this._Value = value; }
        public MaxInclusive(IComparable value)
        {
            this._Value = value;
        }

        public IComparable Value { get { return this._Value; } }

        public bool Validate(object input)
        {
            return _Value.CompareTo(Convert.ChangeType(input, _Value.GetType())) >= 0;
        }

        public string RestrictionMessageFormat
        {
            get { return "Value is below the maximum (Inclusive) of " + _Value.ToString(); }
        }

        public override string ToString()
        {
            return this.RestrictionMessageFormat;
        }



    }
}
