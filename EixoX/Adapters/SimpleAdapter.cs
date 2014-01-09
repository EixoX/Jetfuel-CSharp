using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EixoX.Adapters
{
    /// <summary>
    /// An adapter that can marshall simple items.
    /// </summary>
    public interface SimpleAdapter
    {
        /// <summary>
        /// Checks if a given input is empty.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True if the input is empty.</returns>
        bool IsEmpty(object input);

        /// <summary>
        /// Gets the data db type for the simple item.
        /// </summary>
        System.Data.DbType DbType { get; }
        /// <summary>
        /// Gets the sql db type for the simple item.
        /// </summary>
        System.Data.SqlDbType SqlDbType { get; }

        /// <summary>
        /// Formats an object to a string.
        /// </summary>
        /// <param name="input">The input object to format.</param>
        /// <param name="formatString">The format string to use.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A formatted string object.</returns>
        string FormatObject(object input, string formatString, IFormatProvider formatProvider);
        /// <summary>
        /// Formats an object to a astring.
        /// </summary>
        /// <param name="input">The input object to format.</param>
        /// <param name="formatString">The format string to use.</param>
        /// <returns>A formatted string object.</returns>
        string FormatObject(object input, string formatString);

        /// <summary>
        /// Formats an object to a string.
        /// </summary>
        /// <param name="input">The input object to format.</param>
        /// <param name="formatProvider">The format privoder to use.</param>
        /// <returns>A formatted string object.</returns>
        string FormatObject(object input, IFormatProvider formatProvider);

        /// <summary>
        /// Formats an object to a string.
        /// </summary>
        /// <param name="input">The input object to format.</param>
        /// <returns>A formatted string object.</returns>
        string FormatObject(object input);

        /// <summary>
        /// Parses a string into an object.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A parsed object.</returns>
        object ParseObject(string input, IFormatProvider formatProvider);

        /// <summary>
        /// Parses an input string to an object.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        /// <returns>A parsed object.</returns>
        object ParseObject(string input);

        /// <summary>
        /// Marshalls an input object to a sql statement.
        /// </summary>
        /// <param name="input">The input object to marshall.</param>
        /// <param name="nullable">Indicates that the input is nullable.</param>
        /// <returns>The formatted sql string.</returns>
        string SqlMarshallObject(object input, bool nullable);
        /// <summary>
        /// Appends a marshalled sql string to a string builder.
        /// </summary>
        /// <param name="builder">The builder to append to.</param>
        /// <param name="input">The object to marshall.</param>
        /// <param name="nullable">Indicates that the input is nullable.</param>
        void SqlMarshallObject(StringBuilder builder, object input, bool nullable);

        /// <summary>
        /// Binary reads an object from a binary reader.
        /// </summary>
        /// <param name="reader">The binary reader to read from.</param>
        /// <returns>The object read.</returns>
        object BinaryReadObject(BinaryReader reader);

        /// <summary>
        /// Binary writes an object to a binary writer.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to writer.</param>
        void BinaryWriteObject(BinaryWriter writer, object value);
    }

    /// <summary>
    /// Represents an abstract typed simple adapter.
    /// </summary>
    /// <typeparam name="T">The type of object to marshall.</typeparam>
    public interface SimpleAdapter<T>
        : SimpleAdapter
    {

        /// <summary>
        /// Checks if a given typed object is empty.
        /// </summary>
        /// <param name="input">The object to check.</param>
        /// <returns>True if empty.</returns>
        bool IsEmpty(T input);

        /// <summary>
        /// Formats a typed value to a string.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatString">The format string to use.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A formatted string value.</returns>
        string FormatValue(T input, string formatString, IFormatProvider formatProvider);
        /// <summary>
        /// Formats a typed value to a string.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatString">The format string to use.</param>
        /// <returns>The formatted string.</returns>
        string FormatValue(T input, string formatString);
        /// <summary>
        /// Formats a typed value to a string.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A formatted string value.</returns>
        string FormatValue(T input, IFormatProvider formatProvider);
        /// <summary>
        /// Formats a typed value to a string.
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <returns>A formatted string value.</returns>
        string FormatValue(T input);

        /// <summary>
        /// Parses a string into a typed value.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A parsed value.</returns>
        T ParseValue(string input, IFormatProvider formatProvider);

        /// <summary>
        /// Parses a stringt into a typed value.
        /// </summary>
        /// <param name="input">The input to parse.</param>
        /// <returns>The parse value.</returns>
        T ParseValue(string input);

        /// <summary>
        /// Marshallizes a value to a sql string.
        /// </summary>
        /// <param name="input">The value to marshalize.</param>
        /// <param name="nullable">Indicates that the input is nullable.</param>
        /// <returns>The marshalled sql string.</returns>
        string SqlMarshallValue(T input, bool nullable);

        /// <summary>
        /// Appends a marshalled sql string to a string builder.
        /// </summary>
        /// <param name="builder">The builder to append to.</param>
        /// <param name="input">The object to marshall.</param>
        /// <param name="nullable">Indicates that the input is nullable.</param>
        void SqlMarshallValue(StringBuilder builder, T input, bool nullable);

        /// <summary>
        /// Binary reads an object from a binary reader.
        /// </summary>
        /// <param name="reader">The binary reader to read from.</param>
        /// <returns>The object read.</returns>
        T BinaryReadValue(BinaryReader reader);

        /// <summary>
        /// Binary writes an object to a binary writer.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to writer.</param>
        void BinaryWriteValue(BinaryWriter writer, T value);
    }
}
