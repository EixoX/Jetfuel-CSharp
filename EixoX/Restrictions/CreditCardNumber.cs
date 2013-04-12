using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Restrictions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CreditCardNumber :
        Attribute,
        Restriction
    {
        public bool Validate(object input)
        {
            if (input == null)
                return false;

            string digits = StringHelper.DigitsOnly(input.ToString());

            return IsValid(digits);
            
        }


        public string GetRestrictionMessage(object input)
        {
            return "Invalid credit card number";
        }


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
    }
}
