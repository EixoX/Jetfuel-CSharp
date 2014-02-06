using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data.ClassFilters
{
    public class ClassFilterEqualTo
        : ClassFilterComparer
    {
        public override ClassFilterComparison Comparison
        {
            get { return ClassFilterComparison.EqualTo; }
        }

        public override bool FilterPass(object entity, object value)
        {
            return entity == null ? value == null : entity.Equals(value);
        }
    }
}
