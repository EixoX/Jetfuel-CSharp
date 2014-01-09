using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace EixoX.Adapters
{
    /// <summary>
    /// Represents a simple type adapter for a Double.
    /// </summary>
    public class DoubleAdapter : SimpleAdapterBase<Double>
    {
        /// <summary>
        /// Creates a new Double adapter base.
        /// </summary>
        public DoubleAdapter() { }

        /// <summary>
        /// Creates a new Double adapter base with a given format string.
        /// </summary>
        /// <param name="formatString">The format string to use.</param>
        public DoubleAdapter(string formatString)
            : base(formatString) { }

        /// <summary>
        /// Creates a new Double adapter base with a given format provider.
        /// </summary>
        /// <param name="formatProvider">The format provider to use.</param>
        public DoubleAdapter(IFormatProvider formatProvider)
            : base(formatProvider) { }

        /// <summary>
        /// Creates a simple Double adapter base.
        /// </summary>
        /// <param name="formatString">The format string to use.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        public DoubleAdapter(string formatString, IFormatProvider formatProvider)
            : base(formatString, formatProvider) { }


        /// <summary>
        /// Gets the data db type for the simple item.
        /// </summary>
        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.Double; }
        }
        /// <summary>
        /// Gets the sql db type for the simple item.
        /// </summary>
        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.Float; }
        }

        /// <summary>
        /// Checks if a given typed object is empty.
        /// </summary>
        /// <param name="input">The object to check.</param>
        /// <returns>True if empty.</returns>
        public override bool IsEmpty(Double input)
        {
            return input == 0D;
        }

        /// <summary>
        /// Formats a typed value to a string.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatString">The format string to use.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A formatted string value.</returns>
        public override string FormatValue(Double input, string formatString, IFormatProvider formatProvider)
        {
            return input.ToString(formatString, formatProvider);
        }

        /// <summary>
        /// Parses a string into a typed value.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A parsed value.</returns>
        public override Double ParseValue(string input, IFormatProvider formatProvider)
        {
            return string.IsNullOrEmpty(input) ?
                0D :
                Double.Parse(input, formatProvider);
        }

        /// <summary>
        /// Marshallizes a value to a sql string.
        /// </summary>
        /// <param name="input">The value to marshalize.</param>
        /// <param name="nullable">Indicates that the input is nullable.</param>
        /// <returns>The marshalled sql string.</returns>
        public override string SqlMarshallValue(Double input, bool nullable)
        {
            if (input == 0D)
                if (nullable)
                    return "NULL";


            return input.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Appends a marshalled sql string to a string builder.
        /// </summary>
        /// <param name="builder">The builder to append to.</param>
        /// <param name="input">The object to marshall.</param>
        /// <param name="nullable">Indicates that the input is nullable.</param>
        public override void SqlMarshallValue(StringBuilder builder, Double input, bool nullable)
        {
            if (input == Double.MinValue)
                if (nullable)
                {
                    builder.Append("NULL");
                    return;
                }


            builder.Append(input.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Binary reads an object from a binary reader.
        /// </summary>
        /// <param name="reader">The binary reader to read from.</param>
        /// <returns>The object read.</returns>
        public override Double BinaryReadValue(BinaryReader reader)
        {
            return reader.ReadDouble();
        }

        /// <summary>
        /// Binary writes an object to a binary writer.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to writer.</param>
        public override void BinaryWriteValue(BinaryWriter writer, Double value)
        {
            writer.Write(value);
        }
    }
}
