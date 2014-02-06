using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data.ClassFilters
{
    public class ClassFilterGreaterThan
        : ClassFilterComparer
    {
        public override ClassFilterComparison Comparison
        {
            get { return ClassFilterComparison.GreaterThan; }
        }

        public override bool FilterPass(object entity, object value)
        {
            return entity == null ? value == null : ((IComparable)entity).CompareTo(value) > 0;
        }
    }
}
