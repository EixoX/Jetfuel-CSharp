using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public static class FormatHelper
    {



        public static string Cep(int value)
        {
            return string.Concat((value / 1000).ToString("00000"), "-", (value % 1000).ToString("000"));
        }

        public static string Cpf(long value)
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
    }
}
