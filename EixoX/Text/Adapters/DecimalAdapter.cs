using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public class DecimalAdapter
        : AbstractTextAdapter<Decimal>
    {
        protected override decimal Parse(string text, IFormatProvider formatProvider)
        {
            return decimal.Parse(text, formatProvider);
        }

        protected override string Format(decimal value, IFormatProvider formatProvider)
        {
            return value.ToString(formatProvider);
        }
    }
}
