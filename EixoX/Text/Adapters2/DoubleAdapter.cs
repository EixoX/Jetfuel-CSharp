using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public class DoubleAdapter
        : AbstractTextAdapter<Double>
    {
        protected override double Parse(string text, IFormatProvider formatProvider)
        {
            return double.Parse(text, formatProvider);
        }

        protected override string Format(double value, IFormatProvider formatProvider)
        {
            return value.ToString(formatProvider);
        }
    }
}
