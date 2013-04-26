using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public class CharAdapter : TextAdapter<char>
    {
        public bool IsEmpty(char value)
        {
            return value == Char.MinValue;
        }

        public char ParseValue(string input)
        {
            if (string.IsNullOrEmpty(input))
                return char.MinValue;
            else
                return char.Parse(input);
        }

        public string FormatValue(char input)
        {
            return input.ToString();
        }

        public bool IsEmpty(object input)
        {
            return input == null ? true : IsEmpty((char)input);
        }

        public object ParseObject(string input)
        {
            return string.IsNullOrEmpty(input) ? null : (object)ParseValue(input);
        }

        public string FormatObject(object input)
        {
            return input == null ? null : FormatValue((char)input);
        }
    }
}
