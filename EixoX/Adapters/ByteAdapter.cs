using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Adapters
{
    public class ByteAdapter
        : SimpleAdapterBase<byte>
    {
        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.Byte; }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.TinyInt; }
        }

        public override bool IsEmpty(byte input)
        {
            return input == 0;
        }

        public override string FormatValue(byte input, string formatString, IFormatProvider formatProvider)
        {
            return input.ToString();
        }

        public override byte ParseValue(string input, IFormatProvider formatProvider)
        {
            return string.IsNullOrEmpty(input) ? (byte)0 : byte.Parse(input, formatProvider);
        }

        public override string SqlMarshallValue(byte input, bool nullable)
        {
            return nullable && input == 0 ? "NULL" : input.ToString();
        }

        public override void SqlMarshallValue(StringBuilder builder, byte input, bool nullable)
        {
            builder.Append(
                nullable && input == 0 ? "NULL" : input.ToString());
        }

        public override byte BinaryReadValue(System.IO.BinaryReader reader)
        {
            return reader.ReadByte();
        }

        public override void BinaryWriteValue(System.IO.BinaryWriter writer, byte value)
        {
            writer.Write(value);
        }
    }
}
