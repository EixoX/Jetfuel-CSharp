using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Adapters
{
    public class DbAnsiCharAdapter
        : SimpleAdapterBase<char>
    {

        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.AnsiStringFixedLength; }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.Char; }
        }

        public override bool IsEmpty(char input)
        {
            return input != char.MinValue;
        }

        public override string FormatValue(char input, string formatString, IFormatProvider formatProvider)
        {
            return input.ToString();
        }

        public override char ParseValue(string input, IFormatProvider formatProvider)
        {
            return char.Parse(input);
        }

        public override string SqlMarshallValue(char input, bool nullable)
        {
            return input == '\'' ? "''''" : string.Concat('\'', input, '\''); 
        }

        public override void SqlMarshallValue(StringBuilder builder, char input, bool nullable)
        {
            if (input == '\'')
                builder.Append("''''");
            else
            {
                builder.Append('\'');
                builder.Append(input);
                builder.Append('\'');
            }
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

