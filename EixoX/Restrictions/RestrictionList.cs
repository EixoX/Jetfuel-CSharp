using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Restrictions
{
    public class RestrictionList : LinkedList<Restriction>, Restriction
    {
        public RestrictionList() { }
        public RestrictionList(IEnumerable<Restriction> restrictions) : base(restrictions) { }
        public RestrictionList(AspectMember member) : base(member.GetAttributes<Restriction>(true)) { }

        public bool Validate(object input)
        {
            foreach (Restriction r in this)
                if (!r.Validate(input))
                    return false;
            return true;
        }

        public void AssertValid(object input)
        {
            foreach (Restriction r in this)
                if (!r.Validate(input))
                    throw new RestrictionException(r.GetRestrictionMessage(input));
        }

        public void AssertValid(object input, int lcid)
        {
            foreach (Restriction r in this)
                if (!r.Validate(input))
                    throw new RestrictionException(RestrictionMessages.GetMessage(r, lcid, input));
        }

        public string GetRestrictionMessage(object input)
        {
            foreach (Restriction r in this)
            {
                if (!r.Validate(input))
                    return r.GetRestrictionMessage(input);
            }

            return null;
        }

        public string GetRestrictionMessage(object input, int lcid)
        {
            foreach (Restriction r in this)
                if (!r.Validate(input))
                    return RestrictionMessages.GetMessage(r, lcid, input);

            return null;
        }
    }
}
