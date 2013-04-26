using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public class StringAdapter : TextAdapter<string>
    {
        public bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public string ParseValue(string input)
        {
            return input;
        }

        public string FormatValue(string input)
        {
            return input;
        }

        public bool IsEmpty(object input)
        {
            return input == null || string.IsNullOrEmpty((string)input);
        }

        public object ParseObject(string input)
        {
            return input;
        }

        public string FormatObject(object input)
        {
            return input == null ? null : (string)input;
        }
    }
}
