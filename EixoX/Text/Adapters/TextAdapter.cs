using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public interface TextAdapter
    {
        bool IsEmpty(object input);
        object ParseObject(string input);
        object ParseObject(string input, IFormatProvider formatProvider);
        string FormatObject(object input);
        string FormatObject(object input, IFormatProvider formatProvider);
    }

    public interface TextAdapter<T>
        : TextAdapter
    {
        bool IsEmpty(T value);
        T ParseValue(string input);
        T ParseValue(string input, IFormatProvider formatProvider);
        string FormatValue(T input);
        string FormatValue(T input, IFormatProvider formatProvider);
    }
}
