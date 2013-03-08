using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class Email : Pattern, Restriction
    {
        public const string RegexPattern = @"\w+([-+.']\w+)*_?@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        public Email() : base(RegexPattern) { }

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
