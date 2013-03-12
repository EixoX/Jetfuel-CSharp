using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public class Int64Adapter
        : AbstractTextAdapter<Int64>
    {

        protected override long Parse(string text, IFormatProvider formatProvider)
        {
            return long.Parse(text, formatProvider);
        }

        protected override string Format(long value, IFormatProvider formatProvider)
        {
            return value.ToString(formatProvider);
        }
    }
}
