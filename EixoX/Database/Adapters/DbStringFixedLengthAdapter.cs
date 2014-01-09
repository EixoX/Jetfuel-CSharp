using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Adapters
{
    public class DbStringFixedLengthAdapter
        : SimpleAdapterBase<string>
    {
        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.String; }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.NText; }
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
