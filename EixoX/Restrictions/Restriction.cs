using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Restrictions
{
    /// <summary>
    /// Represents a restriction  capable of validating an input object.
    /// </summary>
    public interface Restriction
    {
        /// <summary>
        /// Validates an object.
        /// </summary>
        /// <param name="input">The object to validate.</param>
        /// <returns>True if the object is validated.</returns>
        bool Validate(object input);

        /// <summary>
        /// Gets the restriction message format.
        /// </summary>
        string RestrictionMessageFormat { get; }
    }
}
