using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    [Serializable]
    public class EnumerationList : LinkedList<Enumeration>, Restriction
    {

        public EnumerationList() { }
        public EnumerationList(IEnumerable<Enumeration> enumeration) : base(enumeration) { }

        public bool Validate(object input)
        {
            foreach (Enumeration e in this)
                if (e.Key.Equals(input))
                    return true;

            return false;
        }

        public override string ToString()
        {
            return "Enumeration Restriction";
        }


        public string GetRestrictionMessage(object input)
        {
            return string.Format("The value provided ({0}) is not enumerated.", input);
        }
    }
}
