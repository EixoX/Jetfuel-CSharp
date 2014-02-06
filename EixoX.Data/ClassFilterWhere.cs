using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public interface ClassFilterWhere<T>
    {
        T Where(ClassFilter filter);
        T Where(string name, ClassFilterComparison comparison, object value);
        T Where(string name, object value);
        T Where(int ordinal, ClassFilterComparison comparison, object value);
        T Where(int ordinal, object value);

        T And(ClassFilter filter);
        T And(string name, ClassFilterComparison comparison, object value);
        T And(string name, object value);
        T And(int ordinal, ClassFilterComparison comparison, object value);
        T And(int ordinal, object value);

        T Or(ClassFilter filter);
        T Or(string name, ClassFilterComparison comparison, object value);
        T Or(string name, object value);
        T Or(int ordinal, ClassFilterComparison comparison, object value);
        T Or(int ordinal, object value);
    }
}
