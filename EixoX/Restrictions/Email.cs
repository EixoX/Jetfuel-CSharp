using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Restrictions
{
    /// <summary>
    /// Represents an e-mail pattern restriction.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class Email : Attribute, Restriction
    {
        private readonly System.Text.RegularExpressions.Regex _Regex;

        /// <summary>
        /// The E-mail restriction regular expression pattern.
        /// </summary>
        public const string RegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        /// <summary>
        /// Constructs the email restriction.
        /// </summary>
        public Email()
        {
            this._Regex = new System.Text.RegularExpressions.Regex(RegexPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }


        /// <summary>
        /// Checks if the given input is an email.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True if the given input is an e-mail.</returns>
        public static bool IsEmail(string input)
        {

            return
                !string.IsNullOrEmpty(input) &&
                Singleton<Email>.Instance._Regex.IsMatch(input);
        }

        /// <summary>
        /// Validates the given input as an e-mail address or empty (use Required to make sure things are required).
        /// </summary>
        /// <param name="input">The input to validate.</param>
        /// <returns>True if the input is an e-mail address or empty.</returns>
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


        /// <summary>
        /// Gets the restriction message format for the invalid e-mail address.
        /// </summary>
        public string RestrictionMessageFormat
        {
            get { return "Not a valid e-mail address"; }
        }
    }
}
