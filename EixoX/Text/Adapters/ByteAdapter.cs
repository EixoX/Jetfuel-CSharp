using System;

namespace EixoX.Text
{
    public class ByteAdapter
        : TextAdapterBase<Byte>
    {
        protected override byte Parse(string text, IFormatProvider formatProvider)
        {
            return byte.Parse(text, formatProvider);
        }

        protected override string Format(byte value, IFormatProvider formatProvider)
        {
            return value.ToString(formatProvider);
        }
    }
}
