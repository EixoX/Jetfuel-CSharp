using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Adapters
{
    public class CurrencyAdapter
        : SimpleAdapterBase<decimal>
    {
        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.Currency; }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.Money; }
        }

        public override bool IsEmpty(decimal input)
        {
            return input == 0;
        }

        public override string FormatValue(decimal input, string formatString, IFormatProvider formatProvider)
        {
            return input.ToString(formatString, formatProvider);
        }

        public override decimal ParseValue(string input, IFormatProvider formatProvider)
        {
            return string.IsNullOrEmpty(input) ? 0M : decimal.Parse(input, formatProvider);
        }

        public override string SqlMarshallValue(decimal input, bool nullable)
        {
            return nullable && input == 0M ? "NULL" : input.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        public override void SqlMarshallValue(StringBuilder builder, decimal input, bool nullable)
        {
            if (nullable && input == 0M)
                builder.Append("NULL");
            else
                builder.Append(input.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        public override decimal BinaryReadValue(System.IO.BinaryReader reader)
        {
            return reader.ReadDecimal();
        }

        public override void BinaryWriteValue(System.IO.BinaryWriter writer, decimal value)
        {
            writer.Write(value);
        }
    }
}
