using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Formatters
{
    public interface Formatter
    {
        string Format(object input, IFormatProvider formatProvider);
    }
}
