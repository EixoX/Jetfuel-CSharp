using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class GuidAttribute : Attribute, Restriction
    {

        public bool Validate(object input)
        {
            if (input == null)
                return true;
            string s = input.ToString();
            int l = s.Length;
            if (l == 0)
                return true;
            else
            {
                for (int i = 0; i < l; i++)
                    if (!char.IsLetterOrDigit(s, i) && s[i] != '-')
                        return false;

                return true;
            }
        }

        public string RestrictionMessageFormat
        {
            get { return "O valor informado não é um guid válido"; }
        }
    }
}
