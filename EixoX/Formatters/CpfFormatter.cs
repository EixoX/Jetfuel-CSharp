using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Formatters
{
    public class CpfFormatter
        : Formatter
    {
        private CpfFormatter() { }

        public static string Format(long value)
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

        public string Format(object input, IFormatProvider formatProvider)
        {
            return Format(Convert.ToInt64(input));
        }

        public static readonly CpfFormatter INSTANCE = new CpfFormatter();
    }
}
