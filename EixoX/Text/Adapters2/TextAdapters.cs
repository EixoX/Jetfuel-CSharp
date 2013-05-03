using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public static class TextAdapters
    {
        public static readonly ByteAdapter Byte = new ByteAdapter();
        public static readonly ByteArrayAdapter ByteArray = new ByteArrayAdapter();
        public static readonly DateTimeAdapter DateTime = new DateTimeAdapter();
        public static readonly DateYmdAdapter DateYmd = new DateYmdAdapter();
        public static readonly DecimalAdapter Decimal = new DecimalAdapter();
        public static readonly DoubleAdapter Double = new DoubleAdapter();
        public static readonly FloatAdapter Float = new FloatAdapter();
        public static readonly GuidAdapter Guid = new GuidAdapter();
        public static readonly ImageAdapter ImageAdapter = new ImageAdapter();
        public static readonly Int32Adapter Int32 = new Int32Adapter();
        public static readonly Int64Adapter Int64 = new Int64Adapter();
        public static readonly SByteAdapter SByte = new SByteAdapter();
        public static readonly TimespanAdapter Timespan = new TimespanAdapter();
        public static readonly UInt32Adapter UInt32 = new UInt32Adapter();
        public static readonly UInt64Adapter UInt64 = new UInt64Adapter();
    }
}
