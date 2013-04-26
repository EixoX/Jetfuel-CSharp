using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EixoX.Text.Adapters
{
    /// <summary>
    /// The base class for a numeric adapter.
    /// </summary>
    /// <typeparam name="T">The type of values adapted by the class.</typeparam>
    public abstract class NumericAdapter<T> : TextAdapter<T>
    {
        private readonly NumberStyles _NumberStyles;
        private readonly IFormatProvider _FormatProvider;
        private readonly string _FormatString;

        /// <summary>
        /// Creates a new numeric adapter.
        /// </summary>
        /// <param name="numberStyles">The number styles to apply.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="formatString">The format string.</param>
        public NumericAdapter(NumberStyles numberStyles, IFormatProvider formatProvider, string formatString)
        {
            this._NumberStyles = numberStyles;
            this._FormatProvider = formatProvider;
            this._FormatString = formatString;
        }

        /// <summary>
        /// Creates a new numeric adapter.
        /// </summary>
        /// <param name="numberStyles">The number stypes to apply.</param>
        /// <param name="formatProvider">The format provider.</param>
        public NumericAdapter(NumberStyles numberStyles, IFormatProvider formatProvider)
        {
            this._NumberStyles = numberStyles;
            this._FormatProvider = formatProvider;
        }

        /// <summary>
        /// Creates a new numeric adapter.
        /// </summary>
        /// <param name="numberStyles">The number stles to apply.</param>
        /// <param name="formatString">The format string.</param>
        public NumericAdapter(NumberStyles numberStyles, string formatString)
        {
            this._NumberStyles = numberStyles;
            this._FormatProvider = CultureInfo.CurrentUICulture;
            this._FormatString = formatString;
        }

        /// <summary>
        /// Creates a new numeric adapter.
        /// </summary>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="formatString">The format string.</param>
        public NumericAdapter(IFormatProvider formatProvider, string formatString)
        {
            this._NumberStyles = NumberStyles.Any;
            this._FormatProvider = formatProvider;
            this._FormatString = formatString;
        }

        /// <summary>
        /// Creates a new numeric adapter.
        /// </summary>
        /// <param name="formatString">The format string.</param>
        public NumericAdapter(string formatString)
        {
            this._NumberStyles = NumberStyles.Any;
            this._FormatProvider = CultureInfo.CurrentUICulture;
            this._FormatString = formatString;
        }

        /// <summary>
        /// Creates a new numeric adapter.
        /// </summary>
        public NumericAdapter()
        {
            this._NumberStyles = NumberStyles.Any;
            this._FormatProvider = CultureInfo.CurrentUICulture;
        }

        /// <summary>
        /// Parses a numeric value from a string.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="numberStyles">The number styles to apply.</param>
        /// <returns>The parsed number.</returns>
        public abstract T ParseValue(string input, IFormatProvider formatProvider, NumberStyles numberStyles);

        /// <summary>
        /// Parses a numeric value from a string.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>The parsed number.</returns>
        public T ParseValue(string input, IFormatProvider formatProvider)
        {
            return ParseValue(input, formatProvider, _NumberStyles);
        }

        /// <summary>
        /// Parses a numeric value from a string.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="numberStyles">The number styles to apply.</param>
        /// <returns>The parsed number.</returns>
        public T ParseValue(string input, NumberStyles numberStyles)
        {
            return ParseValue(input, _FormatProvider, _NumberStyles);
        }

        /// <summary>
        /// Parses a numeric value from a string.
        /// </summary>
        /// <returns>The parsed number.</returns>
        public T ParseValue(string input)
        {
            if (string.IsNullOrEmpty(input))
                return default(T);
            else
                return ParseValue(input, _FormatProvider, _NumberStyles);
        }

        /// <summary>
        /// Formats the value to a string representation.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="formatString">The format string.</param>
        /// <returns>A formatted number string.</returns>
        public abstract string FormatValue(T input, IFormatProvider formatProvider, string formatString);

        /// <summary>
        /// Formats the value to a string representation.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A formatted number string.</returns>
        public string FormatValue(T input, IFormatProvider formatProvider)
        {
            return FormatValue(input, formatProvider, _FormatString);
        }

        /// <summary>
        /// Formats the value to a string representation.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatString">The format string.</param>
        /// <returns>A formatted number string.</returns>
        public string FormatValue(T input, string formatString)
        {
            return FormatValue(input, _FormatProvider, formatString);
        }

        /// <summary>
        /// Formats the value to a string representation.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <returns>A formatted number string.</returns>
        public string FormatValue(T input)
        {
            return FormatValue(input, _FormatProvider, _FormatString);
        }

        /// <summary>
        /// Checks if an input is empty.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True if empty.</returns>
        public abstract bool IsEmpty(T input);

        /// <summary>
        /// Checks if a given object is null or empty.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True if null or empty.</returns>
        public bool IsEmpty(object input)
        {
            if (input == null)
                return true;
            else
                return IsEmpty((T)input);
        }

        /// <summary>
        /// Parses an input string to an object.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The parsed object.</returns>
        public object ParseObject(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            else
                return ParseValue(input, _FormatProvider, _NumberStyles);
        }

        /// <summary>
        /// Formats an object to a string.
        /// </summary>
        /// <param name="input">The object to format.</param>
        /// <returns>The formatted object or null.</returns>
        public string FormatObject(object input)
        {
            if (input == null)
                return null;
            else
                return FormatValue((T)input, _FormatProvider, _FormatString);
        }
    }
}
