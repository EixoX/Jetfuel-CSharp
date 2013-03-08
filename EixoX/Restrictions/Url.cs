using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=true, Inherited=true)]
    public class Url: Pattern, Restriction
    {
        public const string RegexPattern = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        public Url() : base(RegexPattern) { }

        public string GetRestrictionMessage(object input)
        {
            return "Invalid URL";
        }
    }
}
