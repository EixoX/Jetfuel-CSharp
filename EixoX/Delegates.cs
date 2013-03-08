using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Reflection;

namespace EixoX
{
    public delegate bool EqualsToHanlder<T>(T left, T right);
    public delegate int ComparesToHandler<T>(T left, T right);
    public delegate bool FilterHandlder<T>(T left, FilterComparison comparison, T rignt);
}
