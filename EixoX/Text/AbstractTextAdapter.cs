using System;
using System.Collections.Generic;

using System.Text;


namespace EixoX.Text
{
    public abstract class AbstractTextAdapter<T> : TextAdapter<T>
    {

        public abstract T ParseValue(string input, IFormatProvider formatProvider);

        public T ParseValue(string input)
        {
            return ParseValue(input, System.Globalization.CultureInfo.InvariantCulture);
        }

        public abstract string FormatValue(T input, IFormatProvider formatProvider);

        public string FormatValue(T input)
        {
            return FormatValue(input, System.Globalization.CultureInfo.InvariantCulture);
        }

        public abstract bool TryParseValue(string input, IFormatProvider formatProvider, out T value);

        public bool TryParseValue(string input, out T value)
        {
            return TryParseValue(input, System.Globalization.CultureInfo.InvariantCulture, out value);
        }

        public object ParseObject(string input, IFormatProvider formatProvider)
        {
            return ParseValue(input, formatProvider);
        }

        public object ParseObject(string input)
        {
            return ParseValue(input);
        }

        public Type DataType
        {
            get { return typeof(T); }
        }

        public string FormatObject(object input, IFormatProvider formatProvider)
        {
            return FormatValue((T)input, formatProvider);
        }

        public string FormatObject(object input)
        {
            return FormatValue((T)input);
        }

        public bool TryParseObject(string input, IFormatProvider formatProvider, out object value)
        {
            T item;
            if (TryParseValue(input, formatProvider, out item))
            {
                value = item;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        public bool TryParseObject(string input, out object value)
        {
            return TryParseObject(input, System.Globalization.CultureInfo.InvariantCulture, out value);
        }
    }
}
