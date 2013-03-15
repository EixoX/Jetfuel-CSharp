using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class Email : Pattern, Restriction
    {
        public const string RegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        public Email() : base(RegexPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase) { }

        public string GetRestrictionMessage(object input)
        {
            return "Invalid e-mail address";
        }

        public static bool IsEmail(string input)
        {

            return
                !string.IsNullOrEmpty(input) &&
                Singleton<Email>.Instance.IsMatch(input);
        }
    }
}
