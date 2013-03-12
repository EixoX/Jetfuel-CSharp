using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public class FloatAdapter
        : AbstractTextAdapter<float>
    {
        protected override float Parse(string text, IFormatProvider formatProvider)
        {
            return float.Parse(text, formatProvider);
        }

        protected override string Format(float value, IFormatProvider formatProvider)
        {
            return value.ToString(formatProvider);
        }
    }
}
