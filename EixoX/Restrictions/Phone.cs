using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EixoX.Restrictions
{
    /// <summary>
    /// Represents a phone number restriction.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class Phone : Attribute, Restriction
    {
        /// <summary>
        /// Validates an input object as a valid phone number or an empty value.
        /// </summary>
        /// <param name="input">The input to validate.</param>
        /// <returns>True if the value is a valid phone or empty.</returns>
        public bool Validate(object input)
        {
            if (ValidationHelper.IsNullOrEmpty(input))
                return true;
            else if (input is string)
                return IsValid((string)input);
            else
                return IsValid(input.ToString());
        }

        /// <summary>
        /// Checks if a given value is a valid Phone number.
        /// </summary>
        /// <param name="value">The value to check for a valid Phone number.</param>
        /// <returns>True if the given number is a valid phone number.</returns>
        public static bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            if (!Regex.IsMatch(value, @"^[\(\) \-\+0-9_]+$"))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the restriction message format for an invalid phone number.
        /// </summary>
        public string RestrictionMessageFormat
        {
            get { return "Not a valid phone number"; }
        }
    }
}
