using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public class ByteArrayAdapter
        : AbstractTextAdapter<byte[]>
    {
        protected override byte[] Parse(string text, IFormatProvider formatProvider)
        {
            return Convert.FromBase64String(text);
        }

        protected override string Format(byte[] value, IFormatProvider formatProvider)
        {
            return Convert.ToBase64String(value);
        }
    }
}
