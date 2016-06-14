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
    public class ChaveNFe : Attribute, Restriction
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
            else
                return IsValid(input.ToString());
        }

        /// <summary>
        /// Checks if a given value is a valid CPF number.
        /// </summary>
        /// <param name="value">The value to check for a valid CPF number.</param>
        /// <returns>True if the given number is a valid cpf number.</returns>
        public static bool IsValid(string chaveNfe)
        {
            if (chaveNfe == null || chaveNfe.Length != 44)
                return false;

            int[] pesos = new int[] { 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 0 };

            int soma = 0;

            for (int i = 0; i < 43; i++)
                soma += (pesos[i] * CharToInt(chaveNfe[i]));

            if (soma == 0)
                return false;

            int digito = soma - (11 * (soma / 11));
            digito = (digito == 0 || digito == 1) ? 0 : 11 - digito;

            return digito == CharToInt(chaveNfe[43]);

        }

        private static int CharToInt(char c)
        {
            switch (c)
            {
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                default: return 0;
            }
        }

        /// <summary>
        /// Gets the restriction message format for an invalid CPF number.
        /// </summary>
        public string RestrictionMessageFormat
        {
            get { return "Não é uma chave NFe válida"; }
        }


    }
}
