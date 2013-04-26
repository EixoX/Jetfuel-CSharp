using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EixoX.Text.Adapters
{
    /// <summary>
    /// Represents a Decimal class or struct adapter.
    /// </summary>
    public class DecimalAdapter : NumericAdapter<Decimal>
    {

        /// <summary>
        /// Creates a new Decimal adapter.
        /// </summary>
        /// <param name="numberStyles">The number styles to apply.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="formatString">The format string.</param>
        public DecimalAdapter(NumberStyles numberStyles, IFormatProvider formatProvider, string formatString)
            : base(numberStyles, formatProvider, formatString) { }

        /// <summary>
        /// Creates a new Decimal adapter.
        /// </summary>
        /// <param name="numberStyles">The number stypes to apply.</param>
        /// <param name="formatProvider">The format provider.</param>
        public DecimalAdapter(NumberStyles numberStyles, IFormatProvider formatProvider)
            : base(numberStyles, formatProvider) { }

        /// <summary>
        /// Creates a new Decimal adapter.
        /// </summary>
        /// <param name="numberStyles">The number stles to apply.</param>
        /// <param name="formatString">The format string.</param>
        public DecimalAdapter(NumberStyles numberStyles, string formatString)
            : base(numberStyles, formatString) { }


        /// <summary>
        /// Creates a new Decimal adapter.
        /// </summary>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="formatString">The format string.</param>
        public DecimalAdapter(IFormatProvider formatProvider, string formatString)
            : base(formatProvider, formatString) { }

        /// <summary>
        /// Creates a new Decimal adapter.
        /// </summary>
        /// <param name="formatString">The format string.</param>
        public DecimalAdapter(string formatString)
            : base(formatString) { }


        /// <summary>
        /// Creates a new Decimal adapter.
        /// </summary>
        public DecimalAdapter()
            : base() { }

        /// <summary>
        /// Parses a Decimal value from a string.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="numberStyles">The number styles to apply.</param>
        /// <returns>The parsed number.</returns>
        public override Decimal ParseValue(string input, IFormatProvider formatProvider, NumberStyles numberStyles)
        {
            return Decimal.Parse(input, numberStyles, formatProvider);
        }

        /// <summary>
        /// Formats the Decimal value to a string representation.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="formatString">The format string.</param>
        /// <returns>A formatted number string.</returns>
        public override string FormatValue(Decimal input, IFormatProvider formatProvider, string formatString)
        {
            return input.ToString(formatString, formatProvider);
        }

        /// <summary>
        /// Checks if an Decimal is empty.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True if empty.</returns>
        public override bool IsEmpty(Decimal input)
        {
            return input == 0M;
        }

    }
}
