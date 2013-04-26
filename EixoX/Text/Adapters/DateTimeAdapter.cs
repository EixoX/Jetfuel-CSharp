using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EixoX.Text.Adapters
{
    public class DateTimeAdapter : TextAdapter<DateTime>
    {
        private readonly IFormatProvider _FormatProvider;
        private readonly DateTimeStyles _DateTimeStyles;
        private readonly string _FormatString;

        public DateTimeAdapter()
        {
            this._FormatProvider = System.Globalization.CultureInfo.CurrentUICulture;
            this._DateTimeStyles = DateTimeStyles.None;
        }
        public DateTimeAdapter(IFormatProvider formatProvider)
        {
            this._FormatProvider = formatProvider;
            this._DateTimeStyles = DateTimeStyles.None;
        }
        public DateTimeAdapter(IFormatProvider formatProvider, string formatString)
        {
            this._FormatProvider = formatProvider;
            this._FormatString = formatString;
            this._DateTimeStyles = DateTimeStyles.None;
        }
        public DateTimeAdapter(IFormatProvider formatProvider, string formatString, DateTimeStyles styles)
        {
            this._FormatProvider = formatProvider;
            this._FormatString = formatString;
            this._DateTimeStyles = styles;
        }
        public DateTimeAdapter(IFormatProvider formatProvider, DateTimeStyles styles)
        {
            this._FormatProvider = formatProvider;
            this._DateTimeStyles = styles;
        }




        public bool IsEmpty(DateTime value)
        {
            return value == DateTime.MinValue;
        }

        public DateTime ParseValue(string input)
        {
            return DateTime.Parse(input, _FormatProvider, _DateTimeStyles);
        }

        public string FormatValue(DateTime input)
        {
            return input.ToString(_FormatString, _FormatProvider);
        }

        public bool IsEmpty(object input)
        {
            if (input == null)
                return true;
            else
                return IsEmpty((DateTime)input);
        }

        public object ParseObject(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            else
                return DateTime.Parse(input, _FormatProvider, _DateTimeStyles);
        }

        public string FormatObject(object input)
        {
            if (input == null)
                return null;
            else
                return FormatValue((DateTime)input);
        }
    }
}
