using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Adapters
{
    public class DateTimeOffsetAdapter
        : SimpleAdapterBase<TimeSpan>
    {
        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.DateTimeOffset; }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.Time; }
        }

        public override bool IsEmpty(TimeSpan input)
        {
            return input == TimeSpan.Zero;
        }

        public override string FormatValue(TimeSpan input, string formatString, IFormatProvider formatProvider)
        {
            return input.ToString();
        }

        public override TimeSpan ParseValue(string input, IFormatProvider formatProvider)
        {
            return string.IsNullOrEmpty(input) ? TimeSpan.Zero : TimeSpan.Parse(input);
        }

        public override string SqlMarshallValue(TimeSpan input, bool nullable)
        {
            return string.Concat("'", input.ToString(), "'");
        }

        public override void SqlMarshallValue(StringBuilder builder, TimeSpan input, bool nullable)
        {
            builder.Append("'");
            builder.Append(input);
            builder.Append("'");
        }

        public override TimeSpan BinaryReadValue(System.IO.BinaryReader reader)
        {
            return new TimeSpan(reader.ReadInt64());
        }

        public override void BinaryWriteValue(System.IO.BinaryWriter writer, TimeSpan value)
        {
            writer.Write(value.Ticks);
        }
    }
}
