using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public abstract class TextAdapterBase<T> : TextAdapter<T>
    {
        public abstract bool IsEmpty(T value);

        public abstract T ParseValue(string input);

        public abstract string FormatValue(T input);

        public bool IsEmpty(object input)
        {
            if (input == null)
                return true;
            else
                return IsEmpty((T)input);
        }

        public object ParseObject(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            else
                return ParseValue(input);
        }

        public string FormatObject(object input)
        {
            if (input == null)
                return null;
            else
                return FormatValue((T)input);
        }
    }
}
