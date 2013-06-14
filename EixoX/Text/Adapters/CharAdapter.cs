using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Adapters
{
    public class CharAdapter
        : SimpleAdapterBase<char>
    {
        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.StringFixedLength; }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.NChar; }
        }

        public override bool IsEmpty(char input)
        {
            return input == char.MinValue;
        }

        public override string FormatValue(char input, string formatString, IFormatProvider formatProvider)
        {
            return input == char.MinValue ? null : input.ToString();
        }

        public override char ParseValue(string input, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(input))
                return char.MinValue;
            else
                return input[0];
        }

        public override string SqlMarshallValue(char input, bool nullable)
        {
            if (nullable && input == char.MinValue)
                return "NULL";
            else if (input == '\'')
                return "''''";
            else
                return string.Concat('\'', input, '\'');
        }

        public override void SqlMarshallValue(StringBuilder builder, char input, bool nullable)
        {
            builder.Append(SqlMarshallValue(input, nullable));
        }

        public override char BinaryReadValue(System.IO.BinaryReader reader)
        {
            return reader.ReadChar();
        }

        public override void BinaryWriteValue(System.IO.BinaryWriter writer, char value)
        {
            writer.Write(value);
        }
    }
}
