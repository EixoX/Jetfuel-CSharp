using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Adapters
{
    public class TimeSpanAdapter
        : SimpleAdapterBase<TimeSpan>
    {
        public override System.Data.DbType DbType
        {
            get { throw new NotImplementedException(); }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsEmpty(TimeSpan input)
        {
            throw new NotImplementedException();
        }

        public override string FormatValue(TimeSpan input, string formatString, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        public override TimeSpan ParseValue(string input, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        public override string SqlMarshallValue(TimeSpan input, bool nullable)
        {
            throw new NotImplementedException();
        }

        public override void SqlMarshallValue(StringBuilder builder, TimeSpan input, bool nullable)
        {
            throw new NotImplementedException();
        }

        public override TimeSpan BinaryReadValue(System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void BinaryWriteValue(System.IO.BinaryWriter writer, TimeSpan value)
        {
            throw new NotImplementedException();
        }
    }
}
