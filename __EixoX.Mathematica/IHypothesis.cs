using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public interface IHypothesis<T>
    {
        string Name { get; }
        bool Test(T value);
    }
}
