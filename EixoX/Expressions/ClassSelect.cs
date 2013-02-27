using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Expressions
{
    public interface ClassSelect<TEntity, TClass> : 
        ClassFilterBased<TClass>, 
        ClassSortBased<TClass>, 
        ClassPagerBased<TClass>, 
        IEnumerable<TEntity>
    {
    }
}
