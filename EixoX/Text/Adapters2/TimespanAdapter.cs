using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public class TimespanAdapter
        : AbstractTextAdapter<TimeSpan>
    {
        protected override TimeSpan Parse(string text, IFormatProvider formatProvider)
        {
            return TimeSpan.Parse(text);
        }

        protected override string Format(TimeSpan value, IFormatProvider formatProvider)
        {
            return value.ToString();
        }
    }
}
