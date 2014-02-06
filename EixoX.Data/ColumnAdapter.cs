using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    /// <summary>
    /// Represents an adapter for columns.
    /// </summary>
    public interface ColumnAdapter
    {

        object Parse(string input, IFormatProvider formatProvider);
        object Parse(string input);

        object Parse(string[] csv, IFormatProvider formatProvider);
        object Parse(string[] csv);

        object ParseFixedLenght(string line, IFormatProvider formatProvider);
        object ParseFixedLength(string line);

        string Format(object input, IFormatProvider formatProvider);

    }
}
