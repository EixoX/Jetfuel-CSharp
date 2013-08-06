using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MaxExclusive : Attribute, Restriction
    {
        private readonly IComparable _Value;

        public MaxExclusive(int value) { this._Value = value; }
        public MaxExclusive(long value) { this._Value = value; }
        public MaxExclusive(float value) { this._Value = value; }
        public MaxExclusive(double value) { this._Value = value; }
        public MaxExclusive(decimal value) { this._Value = value; }
        public MaxExclusive(IComparable value)
        {
            this._Value = value;
        }

        public IComparable Value { get { return this._Value; } }

        public bool Validate(object input)
        {
            return _Value.CompareTo(Convert.ChangeType(input, _Value.GetType())) > 0;
        }
        
        public string RestrictionMessageFormat
        {
            get { return "Value is below the maximum (exclusive) of " + _Value.ToString(); }
        }

        public override string ToString()
        {
            return this.RestrictionMessageFormat;
        }

        
    }
}
