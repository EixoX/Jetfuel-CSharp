using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Restrictions
{

    /// <summary>
    /// Represents a CPF or CNPJ restriction.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CpfOrCnpj : Attribute, Restriction
    {
        /// <summary>
        /// Checks if a given number value is a valid CPF or CNPJ.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if the number is a valid CPF or CNPJ.</returns>
        public static bool IsCpfOrCnpj(long value) { return Cpf.IsValid(value) || Cnpj.IsValid(value); }
        /// <summary>
        /// Checks if a given string is a valid CPF or CNPJ.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if the string represents a valid CPF or CNPJ.</returns>
        public static bool IsCpfOrCnpj(string value) { return IsCpfOrCnpj(long.Parse(Interceptors.DigitsOnly.Intercept(value))); }

        /// <summary>
        /// Checks if a given input object is a valid CPF or a valid CNPJ or Empty.
        /// </summary>
        /// <param name="input">The input object to check.</param>
        /// <returns>True if the given input is a valid CPF, CNPJ or Empty.</returns>
        public bool Validate(object input)
        {
            if (ValidationHelper.IsNullOrEmpty(input))
                return true;
            else if (input is long)
                return IsCpfOrCnpj((long)input);
            else
                return IsCpfOrCnpj(long.Parse(input.ToString()));
        }

        /// <summary>
        /// Gets the restriction message format for invalid CPF or CNPJ.
        /// </summary>
        public string RestrictionMessageFormat
        {
            get { return "Not a valid number for CPF or CNPJ"; }
        }
    }
}
