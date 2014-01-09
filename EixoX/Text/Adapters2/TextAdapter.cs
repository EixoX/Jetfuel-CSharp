using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public interface TextAdapter
    {
        object ParseObject(string text);
        object ParseObject(string text, IFormatProvider formatProvider);
        Type DataType { get; }
        string FormatObject(object input);
        string FormatObject(object input, IFormatProvider formatProvider);
    }

    public interface TextAdapter<T>
        : TextAdapter
    {
        T ParseValue(string text);
        T ParseValue(string text, IFormatProvider formatProvider);
        string FormatValue(T value);
        string FormatValue(T value, IFormatProvider formatProvider);
    }
}
