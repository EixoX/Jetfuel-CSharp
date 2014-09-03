using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Formatters
{
    public class GenericFormatter
        : Formatter
    {
        public string FormatString { get; set; }

        public GenericFormatter(string formatString)
        {
            this.FormatString = formatString;
        }


        public string Format(object input, IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, this.FormatString, input);
        }


        public static readonly GenericFormatter DEFAULT = new GenericFormatter("{0}");
        public static readonly GenericFormatter SHORT_DATE = new GenericFormatter("{0:d}");
        public static readonly GenericFormatter LONG_DATE = new GenericFormatter("{0:D}");
        public static readonly GenericFormatter DECIMAL = new GenericFormatter("{0:N2}");
        public static readonly GenericFormatter CURRENCY = new GenericFormatter("{0:C2}");
        public static readonly GenericFormatter INTEGER = new GenericFormatter("{0:N0}");
    }
}
