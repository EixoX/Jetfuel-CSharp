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



        public static readonly AnsiCharAdapter AnsiChar = new AnsiCharAdapter();
        public static readonly AnsiStringAdapter AnsiString = new AnsiStringAdapter();
        public static readonly BooleanAdapter Boolean = new BooleanAdapter();
        public static readonly ByteAdapter Byte = new ByteAdapter();
        public static readonly CharAdapter Char = new CharAdapter();
        public static readonly CurrencyAdapter Currency = new CurrencyAdapter();
        public static readonly DateTimeAdapter DateTime = new DateTimeAdapter();
        public static readonly DateTimeOffsetAdapter DateTimeOffset = new DateTimeOffsetAdapter();
        public static readonly DateYmdAdapter DateYmd = new DateYmdAdapter();
        public static readonly DecimalAdapter Decimal = new DecimalAdapter();
        public static readonly DoubleAdapter Double = new DoubleAdapter();
        public static readonly GuidAdapter Guid = new GuidAdapter();
        public static readonly Int16Adapter Int16 = new Int16Adapter();
        public static readonly Int32Adapter Int32 = new Int32Adapter();
        public static readonly Int64Adapter Int64 = new Int64Adapter();
        public static readonly SByteAdapter SByte = new SByteAdapter();
        public static readonly TimeSpanAdapter TimeSpan = new TimeSpanAdapter();
        public static readonly UInt16Adapter UInt16 = new UInt16Adapter();
        public static readonly UInt32Adapter UInt32 = new UInt32Adapter();
        public static readonly UInt64Adapter UInt64 = new UInt64Adapter();
    }
}
