using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Adapters
{
    public static class SimpleAdapters
    {
        public static SimpleAdapter CreateInstance(Type type, string formatString, IFormatProvider formatProvider)
        {
            if (type == PrimitiveTypes.Boolean)
                return new BooleanAdapter();
            else if (type == PrimitiveTypes.Byte)
                return new ByteAdapter();
            else if (type == PrimitiveTypes.Char)
                return new CharAdapter();
            else if (type == PrimitiveTypes.DateTime)
                return new DateTimeAdapter(formatString, formatProvider);
            else if (type == PrimitiveTypes.Decimal)
                return new DecimalAdapter(formatString, formatProvider);
            else if (type == PrimitiveTypes.Double)
                return new DoubleAdapter(formatString, formatProvider);
            else if (type == PrimitiveTypes.Float)
                return new SingleAdapter(formatString, formatProvider);
            else if (type == PrimitiveTypes.Guid)
                return new GuidAdapter();
            else if (type == PrimitiveTypes.Int)
                return new Int32Adapter(formatString, formatProvider);
            else if (type == PrimitiveTypes.Long)
                return new Int64Adapter(formatString, formatProvider);
            else if (type == PrimitiveTypes.SByte)
                return new SByteAdapter(formatString, formatProvider);
            else if (type == PrimitiveTypes.Short)
                return new Int16Adapter(formatString, formatProvider);
            else if (type == PrimitiveTypes.String)
                return new StringAdapter();
            else if (type == PrimitiveTypes.TimeSpan)
                return new TimeSpanAdapter();
            else if (type == PrimitiveTypes.UInt)
                return new UInt32Adapter(formatString, formatProvider);
            else if (type == PrimitiveTypes.ULong)
                return new UInt64Adapter(formatString, formatProvider);
            else if (type == PrimitiveTypes.UShort)
                return new UInt16Adapter(formatString, formatProvider);
            else
                return null;
        }
    }
}
