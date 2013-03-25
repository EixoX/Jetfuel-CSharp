using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class R1Polynomial
        : R1
    {
        private double[] _Coefficients;

        public R1Polynomial(int rank)
        {
            this._Coefficients = new double[rank];
        }

        public R1Polynomial(params double[] coefficients)
        {
            this._Coefficients = coefficients;
        }

        public int Rank { get { return _Coefficients.Length; } }

        public double this[int rank]
        {
            get { return _Coefficients[rank]; }
            set { _Coefficients[rank] = value; }
        }

        public double Calc(double x)
        {
            double d = 0.0;
            int rank = _Coefficients.Rank;
            for (int i = 0; i < rank; i++)
                d += _Coefficients[i] * Math.Pow(x, i);
            return d;
        }
    }
}
