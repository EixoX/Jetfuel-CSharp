using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Expressions
{
    public interface ClassFilterBased<TClass>
    {
        ClassFilter Filter { get; }

        TClass Where(ClassFilter filter);
        TClass Where(int ordinal, ClassFilterComparison comparison, object value);
        TClass Where(int ordinal, object value);
        TClass Where(string name, ClassFilterComparison comparison, object value);
        TClass Where(string name, object value);


        TClass And(ClassFilter filter);
        TClass And(int ordinal, ClassFilterComparison comparison, object value);
        TClass And(int ordinal, object value);
        TClass And(string name, ClassFilterComparison comparison, object value);
        TClass And(string name, object value);

        TClass Or(ClassFilter filter);
        TClass Or(int ordinal, ClassFilterComparison comparison, object value);
        TClass Or(int ordinal, object value);
        TClass Or(string name, ClassFilterComparison comparison, object value);
        TClass Or(string name, object value);
    }
}
