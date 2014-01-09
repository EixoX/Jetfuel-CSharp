using System;
using System.Collections.Generic;
using System.Text;

/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Data
{
    /// <summary>
    /// An enumeration of filter comparisons.
    /// </summary>
    public enum FilterComparison
    {
        /// <summary>
        /// The value must equal to the other
        /// </summary>
        EqualTo,
        /// <summary>
        /// The value must not equal to the other.
        /// </summary>
        NotEqualTo,
        /// <summary>
        /// The value must be greater than the other.
        /// </summary>
        GreaterThan,
        /// <summary>
        /// The value must be greater or equal to the other.
        /// </summary>
        GreaterOrEqual,
        /// <summary>
        /// The value must be lower (smaller) then the other.
        /// </summary>
        LowerThan,
        /// <summary>
        /// The value must be lower (smaller) or equal to the other.
        /// </summary>
        LowerOrEqual,
        /// <summary>
        /// The value must contain or resamble the ohter.
        /// </summary>
        Like,
        /// <summary>
        /// The value must not contain or resemble the other.
        /// </summary>
        NotLike,
        /// <summary>
        /// The value must be part of a collection.
        /// </summary>
        InCollection,
        /// <summary>
        /// The value must not be part of a collection.
        /// </summary>
        NotInCollection

    }
}
