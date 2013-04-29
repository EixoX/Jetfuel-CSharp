using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public class TimeSpanAdapter : TextAdapterBase<TimeSpan>
    {

        public override bool IsEmpty(TimeSpan value)
        {
            return value == TimeSpan.Zero;
        }

        public override TimeSpan ParseValue(string input)
        {
            return string.IsNullOrEmpty(input) ?
                TimeSpan.Zero :
                TimeSpan.Parse(input);
        }

        public override string FormatValue(TimeSpan input)
        {
            return input.ToString();
        }

        public override TimeSpan ParseValue(string input, IFormatProvider formatProvider)
        {
            return string.IsNullOrEmpty(input) ?
                TimeSpan.Zero :
                TimeSpan.Parse(input);
        }

        public override string FormatValue(TimeSpan input, IFormatProvider formatProvider)
        {
            return input.ToString();
        }
    }
}
