using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Adapters
{
    public class DbAnsiStringFixedLengthAdapter
        : SimpleAdapterBase<string>
    {
        private readonly int _Length;

        public DbAnsiStringFixedLengthAdapter(int length)
        {
            this._Length = length;
        }

        public int Length { get { return this._Length; } }

        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.AnsiStringFixedLength; }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.Char; }
        }

        public override bool IsEmpty(string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public override string FormatValue(string input, string formatString, IFormatProvider formatProvider)
        {
            return input;
        }

        public override string ParseValue(string input, IFormatProvider formatProvider)
        {
            return input;
        }

        public override string SqlMarshallValue(string input, bool nullable)
        {
            if (nullable && string.IsNullOrEmpty(input))
                return "NULL";
            else
                return string.Concat("'", StringHelper.SqlSafeString(input), "'");
        }

        public override void SqlMarshallValue(StringBuilder builder, string input, bool nullable)
        {
            if (nullable && string.IsNullOrEmpty(input))
            {
                builder.Append("NULL");
            }
            else
            {
                builder.Append('\'');
                builder.Append(StringHelper.SqlSafeString(input));
                builder.Append('\'');
            }
        }

        public override string BinaryReadValue(System.IO.BinaryReader reader)
        {
            return reader.ReadString();
        }

        public override void BinaryWriteValue(System.IO.BinaryWriter writer, string value)
        {
            writer.Write(value);
        }
    }
}
