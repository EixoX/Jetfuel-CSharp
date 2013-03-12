using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public class DateTimeAdapter
        : AbstractTextAdapter<DateTime>
    {
        protected override DateTime Parse(string text, IFormatProvider formatProvider)
        {
            return DateTime.Parse(text, formatProvider);
        }

        protected override string Format(DateTime value, IFormatProvider formatProvider)
        {
            return value.ToString(formatProvider);
        }
    }
}
