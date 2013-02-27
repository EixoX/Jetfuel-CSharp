using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Expressions
{
    public interface ClassSort
    {
        IEnumerable<T> Sort<T>(IEnumerable<T> entities);
    }
}
