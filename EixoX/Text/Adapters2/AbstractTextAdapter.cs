using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public abstract class AbstractTextAdapter<T>
        : TextAdapter<T>
    {

        protected abstract T Parse(string text, IFormatProvider formatProvider);
        protected abstract string Format(T value, IFormatProvider formatProvider);

        public T ParseValue(string text)
        {
            if (string.IsNullOrEmpty(text))
                return default(T);
            else
                return Parse(text, System.Globalization.CultureInfo.InvariantCulture);
        }

        public T ParseValue(string text, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(text))
                return default(T);
            else
                return Parse(text, formatProvider);
        }

        public string FormatValue(T value)
        {
            return Format(value, System.Globalization.CultureInfo.InvariantCulture);
        }

        public string FormatValue(T value, IFormatProvider formatProvider)
        {
            return Format(value, formatProvider);
        }

        public object ParseObject(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            else
                return Parse(text, System.Globalization.CultureInfo.InvariantCulture);
        }

        public object ParseObject(string text, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            else
                return Parse(text, formatProvider);
        }

        public Type DataType
        {
            get { return typeof(T); }
        }

        public string FormatObject(object input)
        {
            if (input == null)
                return Format(default(T), System.Globalization.CultureInfo.InvariantCulture);
            else
                return Format((T)input, System.Globalization.CultureInfo.InvariantCulture);
        }

        public string FormatObject(object input, IFormatProvider formatProvider)
        {
            if (input == null)
                return Format(default(T), formatProvider);
            else
                return Format((T)input, formatProvider);
        }
    }
}
