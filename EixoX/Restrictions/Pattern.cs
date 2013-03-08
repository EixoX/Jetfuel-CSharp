using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class Pattern : Attribute
    {
        private readonly System.Text.RegularExpressions.Regex _Regex;

        public Pattern(string pattern)
        {
            this._Regex = new System.Text.RegularExpressions.Regex(pattern);
        }

        public Pattern(string pattern, System.Text.RegularExpressions.RegexOptions options)
        {
            this._Regex = new System.Text.RegularExpressions.Regex(pattern, options);
        }

        public System.Text.RegularExpressions.Regex Regex { get { return this._Regex; } }

        public bool IsMatch(string input)
        {
            return this._Regex.IsMatch(input);
        }


        public bool Validate(object input)
        {
            if (input == null || string.IsNullOrEmpty(input.ToString()))
                return true;
            else
                return _Regex.IsMatch(input.ToString());
        }

    }
}
