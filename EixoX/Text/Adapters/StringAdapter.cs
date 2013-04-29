using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public class StringAdapter : TextAdapterBase<string>
    {

        public override bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public override string ParseValue(string input)
        {
            return input;
        }

        public override string FormatValue(string input)
        {
            return input;
        }

        public override string ParseValue(string input, IFormatProvider formatProvider)
        {
            return input;
        }

        public override string FormatValue(string input, IFormatProvider formatProvider)
        {
            return input;
        }
    }
}
