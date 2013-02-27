using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Expressions
{
    public interface ClassSortBased<TClass>
    {
        ClassSort Sort { get; }
        TClass OrderBy(int ordinal, ClassSortDiretion direction);
        TClass OrderBy(params int[] ordinals);
        TClass OrderBy(string name, ClassSortDiretion direction);
        TClass OrderBy(params string[] names);
        TClass OrderBy(ClassSortDiretion direction, params string[] names);
        TClass OrderBy(ClassSortDiretion direction, params int[] ordinals);
    }
}
