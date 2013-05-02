using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Adapters
{
    public class BooleanAdapter
        : SimpleAdapterBase<bool>
    {
        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.Boolean; }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.Bit; }
        }

        public override bool IsEmpty(bool input)
        {
            return false;
        }

        public override string FormatValue(bool input, string formatString, IFormatProvider formatProvider)
        {
            return input ? "1" : "0";
        }

        public override bool ParseValue(string input, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            else if (input.Length == 1)
                return input[0] == '1';
            else
                return bool.Parse(input);
        }

        public override string SqlMarshallValue(bool input, bool nullable)
        {
            return input ? "1" : "0";
        }

        public override void SqlMarshallValue(StringBuilder builder, bool input, bool nullable)
        {
            builder.Append(input ? "1" : "0");
        }

        public override bool BinaryReadValue(System.IO.BinaryReader reader)
        {
            return reader.ReadBoolean();
        }

        public override void BinaryWriteValue(System.IO.BinaryWriter writer, bool value)
        {
            writer.Write(value);
        }
    }
}
