using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text.Adapters
{
    public class UInt16Adapter : AbstractTextAdapter<UInt16>
    {
        protected override ushort Parse(string text, IFormatProvider formatProvider)
        {
            return ushort.Parse(text, formatProvider);
        }

        protected override string Format(ushort value, IFormatProvider formatProvider)
        {
            return value.ToString(formatProvider);
        }
    }
}
