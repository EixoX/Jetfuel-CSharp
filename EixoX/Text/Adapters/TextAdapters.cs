using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EixoX.Text.Adapters
{
    public static class TextAdapters
    {

        public static TextAdapter Create(
            Type dataType,
            IFormatProvider formatProvider,
            string formatString,
            NumberStyles numberStyles,
            DateTimeStyles dateTimeStyles)
        {
            if (dataType == PrimitiveTypes.String)
                return new StringAdapter();
            else if (dataType == PrimitiveTypes.Char)
                return new CharAdapter();
            else if (dataType == PrimitiveTypes.DateTime)
                return new DateTimeAdapter(formatProvider, formatString, dateTimeStyles);
            else if (dataType.IsEnum)
                return new EnumAdapter(dataType);
            else if (dataType == PrimitiveTypes.Guid)
                return new GuidAdapter();

            else if (dataType == PrimitiveTypes.Byte)
                return new ByteAdapter(numberStyles, formatProvider, formatString);
            else if (dataType == PrimitiveTypes.Decimal)
                return new DecimalAdapter(numberStyles, formatProvider, formatString);
            else if (dataType == PrimitiveTypes.Double)
                return new DoubleAdapter(numberStyles, formatProvider, formatString);
            else if (dataType == PrimitiveTypes.Float)
                return new FloatAdapter(numberStyles, formatProvider, formatString);
            else if (dataType == PrimitiveTypes.Short)
                return new Int16Adapter(numberStyles, formatProvider, formatString);
            else if (dataType == PrimitiveTypes.Int)
                return new Int32Adapter(numberStyles, formatProvider, formatString);
            else if (dataType == PrimitiveTypes.Long)
                return new Int64Adapter(numberStyles, formatProvider, formatString);
            else if (dataType == PrimitiveTypes.SByte)
                return new SByteAdapter(numberStyles, formatProvider, formatString);
            else if (dataType == PrimitiveTypes.UShort)
                return new UInt16Adapter(numberStyles, formatProvider, formatString);
            else if (dataType == PrimitiveTypes.UInt)
                return new UInt32Adapter(numberStyles, formatProvider, formatString);
            else if (dataType == PrimitiveTypes.ULong)
                return new UInt64Adapter(numberStyles, formatProvider, formatString);

            else
                return null;
        }

        public static TextAdapter Create(Type dataType)
        {
            return Create(dataType, CultureInfo.CurrentUICulture, null, NumberStyles.Any, DateTimeStyles.None);
        }
    }
}
