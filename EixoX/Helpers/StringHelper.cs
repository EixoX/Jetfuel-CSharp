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


        public static IEnumerable<object> Split(string input, Type conversionType, params char[] separator)
        {
            return Split(input, conversionType, System.Globalization.CultureInfo.CurrentUICulture, separator);
        }

        public static IEnumerable<object> Split(string input, Type conversionType, IFormatProvider formatProvider, params char[] separator)
        {
            string[] spl = input.Split(separator);
            for (int i = 0; i < spl.Length; i++)
                yield return Convert.ChangeType(spl[i], conversionType, formatProvider);
        }


        public static int NextNonWhiteSpaceIndex(string input, int start, int maxlength)
        {
            while (char.IsWhiteSpace(input, start) && start < maxlength)
                start++;
            return start;
        }

        public static int NextIndex(string input, int start, int maxlength, char marker, char escapeChar)
        {
            while (input[start] != marker && start < maxlength && (start > 0 && input[start - 1] != escapeChar))
                start++;
            return start;
        }
    }
}
