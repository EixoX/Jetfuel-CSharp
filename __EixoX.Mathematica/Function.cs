using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public interface Function<TDomain, TImage>
    {
        TImage Calc(TDomain value);
    }
}
