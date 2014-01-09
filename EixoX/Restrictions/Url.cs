using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    /// <summary>
    /// Represents a URL pattern restriction.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=true, Inherited=true)]
    public class Url: Attribute, Restriction
    {
        private readonly System.Text.RegularExpressions.Regex _Regex;

        /// <summary>
        /// The url regular expression pattern.
        /// </summary>
        public const string RegexPattern = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";

        /// <summary>
        /// Constructs a new url restriction.
        /// </summary>
        public Url()
        {
            this._Regex = new System.Text.RegularExpressions.Regex(RegexPattern);
        }

        public bool Validate(object input)
        {
            if (input == null)
                return true;
            else
            {
                string str = input.ToString();
                if (string.IsNullOrEmpty(str))
                    return true;
                else
                    return _Regex.IsMatch(str);

            }

        }


        public string RestrictionMessageFormat
        {
            get { return "Not a valid URL"; }
        }
    }
}
