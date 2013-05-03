using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public class UInt32Adapter
        : AbstractTextAdapter<UInt32>
    {

        protected override uint Parse(string text, IFormatProvider formatProvider)
        {
            return uint.Parse(text, formatProvider);
        }

        protected override string Format(uint value, IFormatProvider formatProvider)
        {
            return value.ToString(formatProvider);
        }
    }
}
