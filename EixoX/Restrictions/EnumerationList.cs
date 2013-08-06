using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Restrictions
{
    /// <summary>
    /// Represents the enumeration list restriction.
    /// </summary>
    [Serializable]
    public class EnumerationList : LinkedList<Enumeration>, Restriction
    {

        /// <summary>
        /// Constructs an empty enumeration list.
        /// </summary>
        public EnumerationList() { }
        /// <summary>
        /// Constructs an enumeration list from and enumerable of enumerations.
        /// </summary>
        /// <param name="enumeration">The enumeration of enumerations ;-)</param>
        public EnumerationList(IEnumerable<Enumeration> enumeration) : base(enumeration) { }

        /// <summary>
        /// Checks if a given input is on a list of enumerations.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True if the input value is on the list of enumerations.</returns>
        public bool Validate(object input)
        {
            foreach (Enumeration e in this)
                if (e.Key.Equals(input))
                    return true;

            return false;
        }

        /// <summary>
        /// Gets the class string representations.
        /// </summary>
        /// <returns>The string representation of the class.</returns>
        public override string ToString()
        {
            return "Enumeration Restriction";
        }

        /// <summary>
        /// Gets the restriction message format for the enumeration list restriction.
        /// </summary>
        public string RestrictionMessageFormat
        {
            get { return "The value provided is not enumerated."; }
        }
    }
}
