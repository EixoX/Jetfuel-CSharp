using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public class CharAdapter : TextAdapterBase<char>
    {

        public override bool IsEmpty(char value)
        {
            return value == char.MinValue;
        }

        public override char ParseValue(string input)
        {
            return char.Parse(input);
        }

        public override string FormatValue(char input)
        {
            return input.ToString();
        }

        public override char ParseValue(string input, IFormatProvider formatProvider)
        {
            return char.Parse(input);
        }

        public override string FormatValue(char input, IFormatProvider formatProvider)
        {
            return input.ToString();
        }
    }
}
