using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data.ClassFilters
{
    public abstract class ClassFilterComparer : ClassFilter
    {

        public abstract ClassFilterComparison Comparison { get; }

        public abstract bool FilterPass(object entity, object value);

        public sealed System.Collections.IEnumerable FilterPass(System.Collections.IEnumerable entities, object value)
        {
            foreach (object o in entities)
                if (FilterPass(o, value))
                    yield return o;
        }
    }
}
