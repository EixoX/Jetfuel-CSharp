using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Adapters
{
    public class GuidAdapter
        : SimpleAdapterBase<Guid>
    {
        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.Guid; }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.UniqueIdentifier; }
        }

        public override bool IsEmpty(Guid input)
        {
            return input == Guid.Empty;
        }

        public override string FormatValue(Guid input, string formatString, IFormatProvider formatProvider)
        {
            return input.ToString(formatString);
        }

        public override Guid ParseValue(string input, IFormatProvider formatProvider)
        {
            return string.IsNullOrEmpty(input) ? Guid.Empty : new Guid(input);
        }

        public override string SqlMarshallValue(Guid input, bool nullable)
        {
            return nullable && input == Guid.Empty ?
                "NULL" :
                string.Concat("'", input, "'");
        }

        public override void SqlMarshallValue(StringBuilder builder, Guid input, bool nullable)
        {
            builder.Append(SqlMarshallValue(input, nullable));
        }

        public override Guid BinaryReadValue(System.IO.BinaryReader reader)
        {
            return new Guid(reader.ReadString());
        }

        public override void BinaryWriteValue(System.IO.BinaryWriter writer, Guid value)
        {
            writer.Write(value.ToString());
        }
    }
}
