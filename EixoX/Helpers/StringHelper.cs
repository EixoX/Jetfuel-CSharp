using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public static class StringHelper
    {
        public static string DigitsOnly(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            StringBuilder builder = new StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++)
                if (Char.IsDigit(value, i))
                    builder.Append(value[i]);
            return builder.ToString();
        }

        public static string Right(string input, int length)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            else if (length >= input.Length)
                return input;
            else
                return input.Substring(input.Length - length, length);
        }

        public static string Left(string input, int length)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            else if (length >= input.Length)
                return input;
            else
                return input.Substring(0, length);
        }
    }
}
