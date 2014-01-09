using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Restrictions
{
    /// <summary>
    /// A list of restrictions that also implements Restriction.
    /// </summary>
    public class RestrictionList : LinkedList<Restriction>, Restriction
    {
        public RestrictionList() { }
        public RestrictionList(IEnumerable<Restriction> restrictions) : base(restrictions) { }
        public RestrictionList(AspectMember member) : base(member.GetAttributes<Restriction>(true)) { }

        /// <summary>
        /// Validates the input object.
        /// </summary>
        /// <param name="input">The object to validate.</param>
        /// <returns>True if no restrictions fail to validate the object.</returns>
        public bool Validate(object input)
        {
            foreach (Restriction r in this)
                if (!r.Validate(input))
                    return false;
            return true;
        }

        /// <summary>
        /// Asserts that a given object is valid.
        /// </summary>
        /// <param name="input">The object to validate.</param>
        public void AssertValid(object input)
        {
            foreach (Restriction r in this)
                if (!r.Validate(input))
                    throw new RestrictionException(string.Format(r.RestrictionMessageFormat, input));
        }

        /// <summary>
        /// Asserts that a given object is valid.
        /// </summary>
        /// <param name="input">The input object to validate.</param>
        /// <param name="lcid">The locale culture id to look up localized restriction messages.</param>
        public void AssertValid(object input, int lcid)
        {
            foreach (Restriction r in this)
                if (!r.Validate(input))
                    throw new RestrictionException(RestrictionMessages.GetMessage(r, lcid, input));
        }

        /// <summary>
        /// Gets the restriction message for a given input object.
        /// </summary>
        /// <param name="input">The input object to validate.</param>
        /// <returns>The restriction message for the input object or null if it validates.</returns>
        public string GetRestrictionMessage(object input)
        {
            foreach (Restriction r in this)
            {
                if (!r.Validate(input))
                    return string.Format(r.RestrictionMessageFormat, input);
            }

            return null;
        }

        /// <summary>
        /// Gets the restriction message for a given input object.
        /// </summary>
        /// <param name="input">The input object to validate.</param>
        /// <param name="lcid">The locale culture id to look up localized restriction messages.</param>
        /// <returns></returns>
        public string GetRestrictionMessage(object input, int lcid)
        {
            foreach (Restriction r in this)
                if (!r.Validate(input))
                    return RestrictionMessages.GetMessage(r, lcid, input);

            return null;
        }


        /// <summary>
        /// Gets the restriction message format.
        /// </summary>
        public string RestrictionMessageFormat
        {
            get { return "Failed validation"; }
        }
    }
}
