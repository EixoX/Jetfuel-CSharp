using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public class TimeSpanAdapter : TextAdapter<TimeSpan>
    {
        public bool IsEmpty(TimeSpan value)
        {
            return value == TimeSpan.Zero;
        }

        public TimeSpan ParseValue(string input)
        {
            return string.IsNullOrEmpty(input) ? TimeSpan.Zero : TimeSpan.Parse(input);
        }

        public string FormatValue(TimeSpan input)
        {
            return input.ToString();
        }

        public bool IsEmpty(object input)
        {
            return input == null || ((TimeSpan)input) == TimeSpan.Zero;
        }

        public object ParseObject(string input)
        {
            return string.IsNullOrEmpty(input) ? null : (object)TimeSpan.Parse(input);
        }

        public string FormatObject(object input)
        {
            return input == null ? null : ((TimeSpan)input).ToString();
        }
    }
}
