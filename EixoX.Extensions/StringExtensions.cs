using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class StringExtensions
    {

        public static string Left(this string input, int length)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            else if (length > input.Length)
                return input;
            else
                return input.Substring(0, length);
        }

        public static string ToStringOrEmpty<T>(this T entity)
        {
            return entity == null || default(T).Equals(entity) ? "" : entity.ToString();
        }

        public static string ToStringOrEmpty(this DateTime dateTime, string formatString)
        {
            return dateTime == DateTime.MinValue ? "" : dateTime.ToString(formatString);
        }

        public static string ToCpf(this long value)
        {
            return EixoX.FormatHelper.Cpf(value);
        }

        public static string ToCnpj(this long value)
        {
            return EixoX.FormatHelper.Cnpj(value);
        }
    }
}
