using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data.ClassFilters
{
    public class ClassFilterLowerOrEqual
    {
        public override ClassFilterComparison Comparison
        {
            get { return ClassFilterComparison.LowerOrEqual; }
        }

        public override bool FilterPass(object entity, object value)
        {
            return entity == null ? value == null : ((IComparable)entity).CompareTo(value) <= 0;
        }
    }
}
