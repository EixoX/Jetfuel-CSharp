using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EixoX.Adapters
{
    /// <summary>
    /// Represents a simple adapter that can handle io for text and known marshalls.
    /// </summary>
    /// <typeparam name="T">The type of element to adapt.</typeparam>
    public abstract class SimpleAdapterBase<T>
        : SimpleAdapter<T>
    {
        private readonly string _FormatString;
        private readonly IFormatProvider _FormatProvider;

        /// <summary>
        /// Creates a new adapter base.
        /// </summary>
        public SimpleAdapterBase()
        {
            this._FormatProvider = System.Globalization.CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Creates a new adapter base with a given format string.
        /// </summary>
        /// <param name="formatString">The format string to use.</param>
        public SimpleAdapterBase(string formatString)
        {
            this._FormatProvider = System.Globalization.CultureInfo.InvariantCulture;
            this._FormatString = formatString;
        }

        /// <summary>
        /// Creates a new adapter base with a given format provider.
        /// </summary>
        /// <param name="formatProvider">The format provider to use.</param>
        public SimpleAdapterBase(IFormatProvider formatProvider)
        {
            this._FormatProvider = formatProvider;
        }

        /// <summary>
        /// Creates a simple adapter base.
        /// </summary>
        /// <param name="formatString">The format string to use.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        public SimpleAdapterBase(string formatString, IFormatProvider formatProvider)
        {
            this._FormatString = formatString;
            this._FormatProvider = formatProvider;
        }


        /// <summary>
        /// Checks if a given input is empty.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True if the input is empty.</returns>
        public bool IsEmpty(object input) { return input == null || IsEmpty((T)input); }

        /// <summary>
        /// Gets the data db type for the simple item.
        /// </summary>
        public abstract System.Data.DbType DbType { get; }
        /// <summary>
        /// Gets the sql db type for the simple item.
        /// </summary>
        public abstract System.Data.SqlDbType SqlDbType { get; }

        /// <summary>
        /// Formats an object to a string.
        /// </summary>
        /// <param name="input">The input object to format.</param>
        /// <param name="formatString">The format string to use.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A formatted string object.</returns>
        public string FormatObject(object input, string formatString, IFormatProvider formatProvider)
        {
            return input == null ? null : FormatValue((T)input, formatString, formatProvider);
        }
        /// <summary>
        /// Formats an object to a astring.
        /// </summary>
        /// <param name="input">The input object to format.</param>
        /// <param name="formatString">The format string to use.</param>
        /// <returns>A formatted string object.</returns>
        public string FormatObject(object input, string formatString)
        {
            return input == null ? null : FormatValue((T)input, formatString, _FormatProvider);
        }

        /// <summary>
        /// Formats an object to a string.
        /// </summary>
        /// <param name="input">The input object to format.</param>
        /// <param name="formatProvider">The format privoder to use.</param>
        /// <returns>A formatted string object.</returns>
        public string FormatObject(object input, IFormatProvider formatProvider)
        {
            return input == null ? null : FormatValue((T)input, _FormatString, formatProvider);
        }

        /// <summary>
        /// Formats an object to a string.
        /// </summary>
        /// <param name="input">The input object to format.</param>
        /// <returns>A formatted string object.</returns>
        public string FormatObject(object input)
        {
            return input == null ? null : FormatValue((T)input, _FormatString, _FormatProvider);
        }

        /// <summary>
        /// Parses a string into an object.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A parsed object.</returns>
        public object ParseObject(string input, IFormatProvider formatProvider)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                    return null;
                else
                    return ParseValue(input, formatProvider);
            }
            catch (Exception e)
            {
                throw new FormatException(e.Message + " on \"" + input + "\".", e);
            }
        }


        /// <summary>
        /// Parses an input string to an object.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        /// <returns>A parsed object.</returns>
        public object ParseObject(string input)
        {
            return ParseObject(input, _FormatProvider);
        }

        /// <summary>
        /// Marshalls an input object to a sql statement.
        /// </summary>
        /// <param name="input">The input object to marshall.</param>
        /// <param name="nullable">Indicates that the input is nullable.</param>
        /// <returns>The formatted sql string.</returns>
        public string SqlMarshallObject(object input, bool nullable)
        {
            if (input == null)
                return nullable ? "NULL" : SqlMarshallValue((T)input, nullable);
            else
                return SqlMarshallValue((T)input, nullable);
        }
        /// <summary>
        /// Appends a marshalled sql string to a string builder.
        /// </summary>
        /// <param name="builder">The builder to append to.</param>
        /// <param name="input">The object to marshall.</param>
        /// <param name="nullable">Indicates that the input is nullable.</param>
        public void SqlMarshallObject(StringBuilder builder, object input, bool nullable)
        {
            if (input == null)
            {
                if (nullable)
                    builder.Append("NULL");
                else
                    SqlMarshallValue(builder, (T)input, nullable);
            }
            else
            {
                SqlMarshallValue(builder, (T)input, nullable);
            }
        }

        /// <summary>
        /// Binary reads an object from a binary reader.
        /// </summary>
        /// <param name="reader">The binary reader to read from.</param>
        /// <returns>The object read.</returns>
        public object BinaryReadObject(BinaryReader reader)
        {
            return BinaryReadValue(reader);
        }

        /// <summary>
        /// Binary writes an object to a binary writer.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to writer.</param>
        public void BinaryWriteObject(BinaryWriter writer, object value)
        {
            BinaryWriteValue(writer, value == null ? default(T) : (T)value);
        }

        /// <summary>
        /// Checks if a given typed object is empty.
        /// </summary>
        /// <param name="input">The object to check.</param>
        /// <returns>True if empty.</returns>
        public abstract bool IsEmpty(T input);

        /// <summary>
        /// Formats a typed value to a string.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatString">The format string to use.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A formatted string value.</returns>
        public abstract string FormatValue(T input, string formatString, IFormatProvider formatProvider);
        /// <summary>
        /// Formats a typed value to a string.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatString">The format string to use.</param>
        /// <returns>The formatted string.</returns>
        public string FormatValue(T input, string formatString)
        {
            return FormatValue(input, formatString, _FormatProvider);
        }
        /// <summary>
        /// Formats a typed value to a string.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A formatted string value.</returns>
        public string FormatValue(T input, IFormatProvider formatProvider)
        {
            return FormatValue(input, _FormatString, formatProvider);
        }
        /// <summary>
        /// Formats a typed value to a string.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <returns>A formatted string value.</returns>
        public string FormatValue(T input)
        {
            return FormatValue(input, _FormatString, _FormatProvider);
        }

        /// <summary>
        /// Parses a string into a typed value.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A parsed value.</returns>
        public abstract T ParseValue(string input, IFormatProvider formatProvider);

        /// <summary>
        /// Parses a stringt into a typed value.
        /// </summary>
        /// <param name="input">The input to parse.</param>
        /// <returns>The parse value.</returns>
        public T ParseValue(string input)
        {
            return ParseValue(input, _FormatProvider);
        }

        /// <summary>
        /// Marshallizes a value to a sql string.
        /// </summary>
        /// <param name="input">The value to marshalize.</param>
        /// <param name="nullable">Indicates that the input is nullable.</param>
        /// <returns>The marshalled sql string.</returns>
        public abstract string SqlMarshallValue(T input, bool nullable);

        /// <summary>
        /// Appends a marshalled sql string to a string builder.
        /// </summary>
        /// <param name="builder">The builder to append to.</param>
        /// <param name="input">The object to marshall.</param>
        /// <param name="nullable">Indicates that the input is nullable.</param>
        public abstract void SqlMarshallValue(StringBuilder builder, T input, bool nullable);

        /// <summary>
        /// Binary reads an object from a binary reader.
        /// </summary>
        /// <param name="reader">The binary reader to read from.</param>
        /// <returns>The object read.</returns>
        public abstract T BinaryReadValue(BinaryReader reader);

        /// <summary>
        /// Binary writes an object to a binary writer.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to writer.</param>
        public abstract void BinaryWriteValue(BinaryWriter writer, T value);
    }
}
