using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Restrictions
{
    /// <summary>
    /// Represents a CNPJ restriction.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class Cnpj : Attribute, Restriction
    {
        private readonly string _RestrictionMessageFormat;

        /// <summary>
        /// Constructs a CNPJ validator with a custom restriction message format.
        /// </summary>
        /// <param name="restrictionMessageFormat">The custom CNPJ restriction message format.</param>
        public Cnpj(string restrictionMessageFormat)
        {
            this._RestrictionMessageFormat = restrictionMessageFormat;
        }

        /// <summary>
        /// Constructs a CNPJ restriction with a default english restriction message format.
        /// </summary>
        public Cnpj() : this("Not a valid CNPJ number") { }

        /// <summary>
        /// Checks if a given value is a valid CNPJ.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if the given value is a valid CNPJ.</returns>
        public static bool IsCnpj(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            else
                return IsValid(long.Parse(Interceptors.DigitsOnly.Intercept(value)));
        }

        /// <summary>
        /// Checks if a given object is a valid CNPJ or an empty string or number.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True if the given value is a valid CNPJ or an empty value.</returns>
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
        /// Checks if a given number is a valid CNPJ.
        /// </summary>
        /// <param name="value">The number to check.</param>
        /// <returns>True if the value is a valid CNPJ.</returns>
        public static bool IsValid(long value)
        {

            if (value == 11111111111111L ||
                value == 22222222222222L ||
                value == 33333333333333L ||
                value == 44444444444444L ||
                value == 55555555555555L ||
                value == 66666666666666L ||
                value == 77777777777777L ||
                value == 88888888888888L ||
                value == 99999999999999L)
                return false;


            long a = ((value / 10000000000000L) % 10);
            long b = ((value / 1000000000000L) % 10);
            long c = ((value / 100000000000L) % 10);
            long d = ((value / 10000000000L) % 10);
            long e = ((value / 1000000000L) % 10);
            long f = ((value / 100000000L) % 10);
            long g = ((value / 10000000L) % 10);
            long h = ((value / 1000000L) % 10);
            long i = ((value / 100000L) % 10);
            long j = ((value / 10000L) % 10);
            long k = ((value / 1000L) % 10);
            long l = ((value / 100L) % 10);

            //Note: compute 1st verification digit.
            long d1 = ((a * 6 + b * 7 + c * 8 + d * 9) + (e * 2 + f * 3 + g * 4 + h * 5) + (i * 6 + j * 7 + k * 8 + l * 9)) % 11;
            if (d1 == 10)
                d1 = 0;
            //d1 = (d1 == 10 ? 0 : 11 - d1);

            //Note: compute 2nd verification digit.
            long d2 = ((a * 5 + b * 6 + c * 7 + d * 8) + (e * 9 + f * 2 + g * 3 + h * 4) + (i * 5 + j * 6 + k * 7 + l * 8) + (9 * d1)) % 11;
            if (d2 == 10)
                d2 = 0;
            //d2 = (d2 == 10  ? 0 : 11 - d2);

            return (d1 == ((value / 10) % 10) && d2 == (value % 10));
        }


        /// <summary>
        /// Formats a numeric value as a CNPJ.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>The formatted CNPJ.</returns>
        public static string Format(long value)
        {
            return string.Concat(
                ((value / 1000000000000) % 100).ToString("00"),
                ".",
                ((value / 1000000000) % 1000).ToString("000"),
                ".",
                ((value / 1000000) % 1000).ToString("000"),
                "/",
                ((value / 100) % 10000).ToString("0000"),
                "-",
                (value % 100).ToString("00"));
        }

        /// <summary>
        /// Gets the restriction message format for a CNPJ number.
        /// </summary>
        public string RestrictionMessageFormat
        {
            get { return this._RestrictionMessageFormat; }
        }
    }
}
