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
    }
}
