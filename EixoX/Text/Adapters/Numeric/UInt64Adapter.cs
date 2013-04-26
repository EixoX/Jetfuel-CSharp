using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EixoX.Text.Adapters
{
    /// <summary>
    /// Represents a UInt64 class or struct adapter.
    /// </summary>
    public class UInt64Adapter : NumericAdapter<UInt64>
    {

        /// <summary>
        /// Creates a new UInt64 adapter.
        /// </summary>
        /// <param name="numberStyles">The number styles to apply.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="formatString">The format string.</param>
        public UInt64Adapter(NumberStyles numberStyles, IFormatProvider formatProvider, string formatString)
            : base(numberStyles, formatProvider, formatString) { }

        /// <summary>
        /// Creates a new UInt64 adapter.
        /// </summary>
        /// <param name="numberStyles">The number stypes to apply.</param>
        /// <param name="formatProvider">The format provider.</param>
        public UInt64Adapter(NumberStyles numberStyles, IFormatProvider formatProvider)
            : base(numberStyles, formatProvider) { }

        /// <summary>
        /// Creates a new UInt64 adapter.
        /// </summary>
        /// <param name="numberStyles">The number stles to apply.</param>
        /// <param name="formatString">The format string.</param>
        public UInt64Adapter(NumberStyles numberStyles, string formatString)
            : base(numberStyles, formatString) { }


        /// <summary>
        /// Creates a new UInt64 adapter.
        /// </summary>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="formatString">The format string.</param>
        public UInt64Adapter(IFormatProvider formatProvider, string formatString)
            : base(formatProvider, formatString) { }

        /// <summary>
        /// Creates a new UInt64 adapter.
        /// </summary>
        /// <param name="formatString">The format string.</param>
        public UInt64Adapter(string formatString)
            : base(formatString) { }


        /// <summary>
        /// Creates a new UInt64 adapter.
        /// </summary>
        public UInt64Adapter()
            : base() { }

        /// <summary>
        /// Parses a UInt64 value from a string.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="numberStyles">The number styles to apply.</param>
        /// <returns>The parsed number.</returns>
        public override UInt64 ParseValue(string input, IFormatProvider formatProvider, NumberStyles numberStyles)
        {
            return UInt64.Parse(input, numberStyles, formatProvider);
        }

        /// <summary>
        /// Formats the UInt64 value to a string representation.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="formatString">The format string.</param>
        /// <returns>A formatted number string.</returns>
        public override string FormatValue(UInt64 input, IFormatProvider formatProvider, string formatString)
        {
            return input.ToString(formatString, formatProvider);
        }

        /// <summary>
        /// Checks if an UInt64 is empty.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True if empty.</returns>
        public override bool IsEmpty(UInt64 input)
        {
            return input == 0L;
        }

    }
}
