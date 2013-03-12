using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public class UInt64Adapter
        : AbstractTextAdapter<UInt64>
    {
        protected override ulong Parse(string text, IFormatProvider formatProvider)
        {
            return ulong.Parse(text, formatProvider);
        }

        protected override string Format(ulong value, IFormatProvider formatProvider)
        {
            return value.ToString(formatProvider);
        }
    }
}
