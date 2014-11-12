using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Restrictions
{
    /// <summary>
    /// Represents a CPF number restriction.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class Cpf : Attribute, Restriction
    {
        /// <summary>
        /// Validates an input object as a valid CPF number or an empty value.
        /// </summary>
        /// <param name="input">The input to validate.</param>
        /// <returns>True if the value is a valid CPF or empty.</returns>
        public bool Validate(object input)
        {
            if (ValidationHelper.IsNullOrEmpty(input))
                return true;
            else if (input is long)
                return IsValid((long)input);
            else
                return IsValid(long.Parse(input.ToString()));
        }

        /// <summary>
        /// Checks if a given value is a valid CPF number.
        /// </summary>
        /// <param name="value">The value to check for a valid CPF number.</param>
        /// <returns>True if the given number is a valid cpf number.</returns>
        public static bool IsValid(long value)
        {

            if (value == 11111111111L ||
                value == 22222222222L ||
                value == 33333333333L ||
                value == 44444444444L ||
                value == 55555555555L ||
                value == 66666666666L ||
                value == 77777777777L ||
                value == 88888888888L ||
                value == 99999999999L ||
                value < 100000L)
                return false;


            long a = ((value / 10000000000L) % 10);
            long b = ((value / 1000000000L) % 10);
            long c = ((value / 100000000L) % 10);
            long d = ((value / 10000000L) % 10);
            long e = ((value / 1000000L) % 10);
            long f = ((value / 100000L) % 10);
            long g = ((value / 10000L) % 10);
            long h = ((value / 1000L) % 10);
            long i = ((value / 100L) % 10);

            //Note: compute 1st verification digit.
            long d1 = (a * 1 + b * 2 + c * 3 + d * 4 + e * 5 + f * 6 + g * 7 + h * 8 + i * 9) % 11;
            if (d1 == 10)
                d1 = 0;

            //d1 = (d1 >= 10 ? 0 : 11 - d1);

            //Note: compute 2nd verification digit.
            long d2 = (b * 1 + c * 2 + d * 3 + e * 4 + f * 5 + g * 6 + h * 7 + i * 8 + d1 * 9) % 11;
            if (d2 == 10)
                d2 = 0;
            //d2 = (d2 >= 10 ? 0 : 11 - d2);

            return (d1 == ((value / 10) % 10) && d2 == (value % 10));
        }

        /// <summary>
        /// Formats the given input number as a CPF.
        /// </summary>
        /// <param name="value">The input number to format.</param>
        /// <returns>The formatted CPF number.</returns>
        public static string Format(long value)
        {
            return
                string.Concat(
                    ((value / 100000000) % 1000).ToString("000"),
                    ".",
                    ((value / 100000) % 1000).ToString("000"),
                    ".",
                    ((value / 100) % 1000).ToString("000"),
                    "-",
                    (value % 100).ToString("00"));
        }

        /// <summary>
        /// Gets the restriction message format for an invalid CPF number.
        /// </summary>
        public string RestrictionMessageFormat
        {
            get { return "Not a valid cpf number"; }
        }
    }
}
