using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EixoX.Text.Adapters
{
    /// <summary>
    /// Represents a UInt16 class or struct adapter.
    /// </summary>
    public class UInt16Adapter : NumericAdapter<UInt16>
    {

        /// <summary>
        /// Creates a new UInt16 adapter.
        /// </summary>
        /// <param name="numberStyles">The number styles to apply.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="formatString">The format string.</param>
        public UInt16Adapter(NumberStyles numberStyles, IFormatProvider formatProvider, string formatString)
            : base(numberStyles, formatProvider, formatString) { }

        /// <summary>
        /// Creates a new UInt16 adapter.
        /// </summary>
        /// <param name="numberStyles">The number stypes to apply.</param>
        /// <param name="formatProvider">The format provider.</param>
        public UInt16Adapter(NumberStyles numberStyles, IFormatProvider formatProvider)
            : base(numberStyles, formatProvider) { }

        /// <summary>
        /// Creates a new UInt16 adapter.
        /// </summary>
        /// <param name="numberStyles">The number stles to apply.</param>
        /// <param name="formatString">The format string.</param>
        public UInt16Adapter(NumberStyles numberStyles, string formatString)
            : base(numberStyles, formatString) { }


        /// <summary>
        /// Creates a new UInt16 adapter.
        /// </summary>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="formatString">The format string.</param>
        public UInt16Adapter(IFormatProvider formatProvider, string formatString)
            : base(formatProvider, formatString) { }

        /// <summary>
        /// Creates a new UInt16 adapter.
        /// </summary>
        /// <param name="formatString">The format string.</param>
        public UInt16Adapter(string formatString)
            : base(formatString) { }


        /// <summary>
        /// Creates a new UInt16 adapter.
        /// </summary>
        public UInt16Adapter()
            : base() { }

        /// <summary>
        /// Parses a UInt16 value from a string.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="numberStyles">The number styles to apply.</param>
        /// <returns>The parsed number.</returns>
        public override UInt16 ParseValue(string input, IFormatProvider formatProvider, NumberStyles numberStyles)
        {
            return UInt16.Parse(input, numberStyles, formatProvider);
        }

        /// <summary>
        /// Formats the UInt16 value to a string representation.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="formatString">The format string.</param>
        /// <returns>A formatted number string.</returns>
        public override string FormatValue(UInt16 input, IFormatProvider formatProvider, string formatString)
        {
            return input.ToString(formatString, formatProvider);
        }

        /// <summary>
        /// Checks if an UInt16 is empty.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True if empty.</returns>
        public override bool IsEmpty(UInt16 input)
        {
            return input == 0;
        }

    }
}
