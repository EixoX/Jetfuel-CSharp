using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Restrictions
{
    /// <summary>
    /// Represents a credit card lunh digit verifier.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CreditCardNumber :
        Attribute,
        Restriction
    {
        /// <summary>
        /// Validates an input as a valid credit card number or empty.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True if the given input is a valid credit card number or empty.</returns>
        public bool Validate(object input)
        {
            if (input == null)
                return false;
            string digits = StringHelper.DigitsOnly(input.ToString());
            return IsValid(digits);
            
        }

        /// <summary>
        /// Checks if the given credit card number digits are valid according to the luhn verifier.
        /// </summary>
        /// <param name="digits">The string containing the credit card digits.</param>
        /// <returns>True if the credit card has a valid number according to the Luhn verifier.</returns>
        public static bool IsValid(string digits)
        {
            if (digits.Length < 12 | digits.Length > 19)
                return false;

            char[] digitsReversed = new char[digits.Length];
            for (int i = 0; i < digitsReversed.Length; i++)
                digitsReversed[i] = digits[digits.Length - i - 1];

            int luhn = 0;
            for (int i = 0; i < digitsReversed.Length; i++)
            {
                int d = i % 2 == 1 ? 2 * int.Parse(digitsReversed[i].ToString()) : int.Parse(digitsReversed[i].ToString());
                luhn += (d / 10) + (d % 10);
            }
            return (luhn != 0) && (luhn % 10 == 0);
        }


        /// <summary>
        /// Gets the restriction message for an invalid credit card number.
        /// </summary>
        public string RestrictionMessageFormat
        {
            get { return "Invalid credit card number"; }
        }
    }
}
