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

        public static string CpfOrCnpj(long value)
        {
            return value > 99999999999L ? Cnpj(value) : Cpf(value);
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

        public static string Cnpj(long value)
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


        public static string Filesize(long size, int digits)
        {

            double sz = size;
            string term = " B";
            if (sz > 1024.0)
            {
                sz /= 1024.0;
                if (sz > 1024.0)
                {
                    sz /= 1024.0;
                    if (sz > 1024)
                    {
                        sz /= 1024.0;
                        term = " GB";
                    }
                    else
                    {
                        term = " MB";
                    }
                }
                else
                {
                    term = " KB";
                }
            }

            return Math.Round(sz, digits) + term;

        }


        public static string DateOrDash(DateTime date, string datePattern)
        {
            if (date == DateTime.MinValue)
                return "-";
            else
                return date.ToString(datePattern);
        }



        public static string FixedLength(string input, int length)
        {
            char[] chars = new char[length];

            if (string.IsNullOrEmpty(input))
            {
                for (int i = 0; i < length; i++)
                    chars[i] = ' ';

            }
            else if (input.Length > length)
            {
                for (int i = 0; i < length; i++)
                    chars[i] = input[i];
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                    chars[i] = input[i];
                for (int i = input.Length; i < length; i++)
                    chars[i] = ' ';
            }

            return new string(chars, 0, length);
        }
    }
}
