using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public interface ClassSort
    {
        Aspect Aspect { get; }
        IEnumerable<T> Sort<T>(IEnumerable<T> entities);

    }
}
