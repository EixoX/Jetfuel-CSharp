using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class Required : Attribute, Restriction
    {
        public bool Validate(object input)
        {
            return !ValidationHelper.IsNullOrEmpty(input);
        }

        public string GetRestrictionMessage(object input)
        {
            return "Required";
        }
    }
}
