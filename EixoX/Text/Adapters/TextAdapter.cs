using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public interface TextAdapter
    {
        bool IsEmpty(object input);
        object ParseObject(string input);
        string FormatObject(object input);
    }

    public interface TextAdapter<T>
        : TextAdapter
    {
        bool IsEmpty(T value);
        T ParseValue(string input);
        string FormatValue(T input);
    }
}
