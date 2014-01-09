using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MinLength : Attribute, Restriction
    {
        private readonly int _Value;

        public MinLength(int value)
        {
            this._Value = value;
        }

        public int Value { get { return this._Value; } }

        public bool Validate(object input)
        {
            return input == null ? true : input.ToString().Length >= _Value;
        }

        public override string ToString()
        {
            return "Min length of " + _Value.ToString();
        }


        public string RestrictionMessageFormat
        {
            get { return "Text length below Minimum of " + _Value; }
        }
    }
}
