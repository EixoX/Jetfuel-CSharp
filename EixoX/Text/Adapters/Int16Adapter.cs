using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text.Adapters
{
    public class Int16Adapter : AbstractTextAdapter<Int16>
    {
        protected override short Parse(string text, IFormatProvider formatProvider)
        {
            return short.Parse(text, formatProvider);
        }

        protected override string Format(short value, IFormatProvider formatProvider)
        {
            return value.ToString(formatProvider);
        }
    }
}
