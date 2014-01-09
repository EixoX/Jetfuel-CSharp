using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class DoubleRange
    {
        private double _Min;
        private double _Max;

        public DoubleRange(double min, double max)
        {
            this._Min = min;
            this._Max = max;
        }

        public bool Contains(double x)
        {
            return x >= _Min && x <= _Max;
        }
    }
}
