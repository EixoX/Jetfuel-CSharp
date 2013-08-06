using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class Length : Attribute, Restriction
    {
        private readonly int _Min;
        private readonly int _Max;

        public Length(int min, int max)
        {
            this._Min = min;
            this._Max = max;
        }

        public Length(int value)
        {
            this._Min = value;
            this._Max = value;
        }

        public int Min { get { return this._Min; } }
        public int Max { get { return this._Max; } }

        public bool Validate(object input)
        {
            if (input == null)
                return _Min == 0;
            else
            {
                int l = input.ToString().Length;
                return l >= _Min && l <= _Max;
            }
        }


        public string RestrictionMessageFormat
        {
            get { return "Text length not in [" + _Min + ", " + _Max + "]"; }
        }


        public override string ToString()
        {
            return this.RestrictionMessageFormat;
        }




    }
}
