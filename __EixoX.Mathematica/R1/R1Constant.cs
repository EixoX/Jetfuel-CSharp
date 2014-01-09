using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public struct R1Constant : R1
    {
        public double Value;

        public R1Constant(double value)
        {
            this.Value = value;
        }

        public static implicit operator R1Constant(double value)
        {
            return new R1Constant(value);
        }

        public double Calc(double x)
        {
            return Value;
        }
    }
}
