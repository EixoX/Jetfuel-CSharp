using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EixoX.Text.Adapters
{
    public class DateTimeAdapter : TextAdapterBase<DateTime>
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



        public override bool IsEmpty(DateTime value)
        {
            return value == DateTime.MinValue;
        }

        public override DateTime ParseValue(string input)
        {
            return string.IsNullOrEmpty(input) ?
                DateTime.MinValue :
                this._FormatProvider == null ?
                DateTime.Parse(input) :
                DateTime.Parse(input, this._FormatProvider, this._DateTimeStyles);
        }

        public override string FormatValue(DateTime input)
        {
            if (input == DateTime.MinValue)
                return null;

            if (this._FormatProvider == null)
            {
                return this._FormatString == null ?
                    input.ToString() : input.ToString(_FormatString);
            }
            else
            {
                return this._FormatString == null ?
                    input.ToString(_FormatProvider) :
                    input.ToString(_FormatString, _FormatProvider);
            }
        }

        public override DateTime ParseValue(string input, IFormatProvider formatProvider)
        {
            return string.IsNullOrEmpty(input) ?
                DateTime.MinValue :
                DateTime.Parse(input, formatProvider);
        }

        public override string FormatValue(DateTime input, IFormatProvider formatProvider)
        {
            if (input == DateTime.MinValue)
                return null;

            return _FormatString == null ?
                input.ToString(formatProvider) :
                input.ToString(_FormatString, _FormatProvider);

        }
    }
}
