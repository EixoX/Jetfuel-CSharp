using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public class SByteAdapter
        : AbstractTextAdapter<sbyte>
    {
        protected override sbyte Parse(string text, IFormatProvider formatProvider)
        {
            return sbyte.Parse(text, formatProvider);
        }

        protected override string Format(sbyte value, IFormatProvider formatProvider)
        {
            return value.ToString(formatProvider);
        }
    }
}
