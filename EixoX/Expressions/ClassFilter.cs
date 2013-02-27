using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Expressions
{
    public interface ClassFilter
    {
        Aspect GetAspect();
        bool FilterPass(object entity);
        IEnumerable<T> FilterPass<T>(IEnumerable<T> entities);
    }
}
