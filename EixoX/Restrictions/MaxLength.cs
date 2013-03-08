using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MaxLength : Attribute, Restriction
    {
        private readonly int _Value;

        public MaxLength(int value)
        {
            this._Value = value;
        }

        public int Value { get { return this._Value; } }

        public bool Validate(object input)
        {
            return input == null ? true : input.ToString().Length <= _Value;
        }

        public string GetRestrictionMessage(object input)
        {
            return "Text length above maximum of " + _Value; 
        }

        public override string ToString()
        {
            return "Max length of " + _Value.ToString();
        }
    }
}
