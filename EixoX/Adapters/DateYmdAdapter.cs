using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Adapters
{
    public class DateYmdAdapter : SimpleAdapterBase<DateTime>
    {
        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.DateTime; }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.DateTime; }
        }

        public override bool IsEmpty(DateTime input)
        {
            return input == DateTime.MinValue;
        }

        public override string FormatValue(DateTime input, string formatString, IFormatProvider formatProvider)
        {
            return input.ToString("yyyyMMdd");
        }

        public override DateTime ParseValue(string input, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(input))
                return DateTime.MinValue;
            
                switch (input.Length)
                {
                    case 8:
                        try
                        {
                            return new DateTime(
                                int.Parse(input.Substring(0, 4), formatProvider),
                                int.Parse(input.Substring(4, 2), formatProvider),
                                int.Parse(input.Substring(6, 2), formatProvider));
                        }
                        catch (Exception e)
                        {
                            throw new ArgumentException("Check format of " + input, "input", e);
                        }
                    case 14:
                        try
                        {
                            return new DateTime(
                                int.Parse(input.Substring(0, 4), formatProvider),
                                int.Parse(input.Substring(4, 2), formatProvider),
                                int.Parse(input.Substring(6, 2), formatProvider),
                                int.Parse(input.Substring(8, 2), formatProvider),
                                int.Parse(input.Substring(10, 2), formatProvider),
                                int.Parse(input.Substring(12, 2), formatProvider));
                        }
                        catch (Exception e)
                        {
                            throw new ArgumentException("Check format of " + input, "input", e);
                        }
                    default:
                        throw new NotImplementedException("Unexpected string length");
                }
            
        }

        public override string SqlMarshallValue(DateTime input, bool nullable)
        {
            return input == DateTime.MinValue && nullable ? "NULL" :
                "'" + input.ToString("yyyy-MM-dd HH:mm:ss.S") + "'";
        }

        public override void SqlMarshallValue(StringBuilder builder, DateTime input, bool nullable)
        {
            builder.Append(SqlMarshallValue(input, nullable));
        }

        public override DateTime BinaryReadValue(System.IO.BinaryReader reader)
        {
            return DateTime.FromFileTime(reader.ReadInt64());
        }

        public override void BinaryWriteValue(System.IO.BinaryWriter writer, DateTime value)
        {
            writer.Write(value.ToFileTime());
        }
    }
}
