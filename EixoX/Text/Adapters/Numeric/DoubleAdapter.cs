using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EixoX.Text.Adapters
{
    /// <summary>
    /// Represents a Double class or struct adapter.
    /// </summary>
    public class DoubleAdapter : NumericAdapter<Double>
    {

        /// <summary>
        /// Creates a new Double adapter.
        /// </summary>
        /// <param name="numberStyles">The number styles to apply.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="formatString">The format string.</param>
        public DoubleAdapter(NumberStyles numberStyles, IFormatProvider formatProvider, string formatString)
            : base(numberStyles, formatProvider, formatString) { }

        /// <summary>
        /// Creates a new Double adapter.
        /// </summary>
        /// <param name="numberStyles">The number stypes to apply.</param>
        /// <param name="formatProvider">The format provider.</param>
        public DoubleAdapter(NumberStyles numberStyles, IFormatProvider formatProvider)
            : base(numberStyles, formatProvider) { }

        /// <summary>
        /// Creates a new Double adapter.
        /// </summary>
        /// <param name="numberStyles">The number stles to apply.</param>
        /// <param name="formatString">The format string.</param>
        public DoubleAdapter(NumberStyles numberStyles, string formatString)
            : base(numberStyles, formatString) { }


        /// <summary>
        /// Creates a new Double adapter.
        /// </summary>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="formatString">The format string.</param>
        public DoubleAdapter(IFormatProvider formatProvider, string formatString)
            : base(formatProvider, formatString) { }

        /// <summary>
        /// Creates a new Double adapter.
        /// </summary>
        /// <param name="formatString">The format string.</param>
        public DoubleAdapter(string formatString)
            : base(formatString) { }


        /// <summary>
        /// Creates a new Double adapter.
        /// </summary>
        public DoubleAdapter()
            : base() { }

        /// <summary>
        /// Parses a Double value from a string.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="numberStyles">The number styles to apply.</param>
        /// <returns>The parsed number.</returns>
        public override Double ParseValue(string input, IFormatProvider formatProvider, NumberStyles numberStyles)
        {
            return Double.Parse(input, numberStyles, formatProvider);
        }

        /// <summary>
        /// Formats the Double value to a string representation.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="formatString">The format string.</param>
        /// <returns>A formatted number string.</returns>
        public override string FormatValue(Double input, IFormatProvider formatProvider, string formatString)
        {
            return input.ToString(formatString, formatProvider);
        }

        /// <summary>
        /// Checks if an Double is empty.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True if empty.</returns>
        public override bool IsEmpty(Double input)
        {
            return input == 0.0;
        }

    }
}
