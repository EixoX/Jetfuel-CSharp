using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Data;

namespace EixoX
{
    public delegate bool EqualsToHandler<T>(T item);
    public delegate int ComparesToHandler<T>(T left, T right);
    public delegate bool FilterHandler<T>(T left, FilterComparison comparison, T rignt);
}
