using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{
    /// <summary>
    /// Represents a required item restriction.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class Required : Attribute, Restriction
    {
        /// <summary>
        /// Validates an input object.
        /// </summary>
        /// <param name="input">The input object to validate.</param>
        /// <returns>True if the input is not null or empty.</returns>
        public bool Validate(object input)
        {
            return !ValidationHelper.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Gets the restriction message format.
        /// </summary>
        public string RestrictionMessageFormat
        {
            get { return "Required"; }
        }
    }
}
