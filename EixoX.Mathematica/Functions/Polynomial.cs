using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica.Functions
{
    public class Polynomial: Function<double, double>
    {
        private double[] _Coefs;

        public Polynomial(int rank)
        {
            this._Coefs = new double[rank];
        }

        public Polynomial(params double[] coefs)
        {
            this._Coefs = coefs;
        }

        public double this[int index]
        {
            get { return _Coefs[index]; }
            set { _Coefs[index] = value; }
        }

        public int Rank { get { return _Coefs.Length; } }



        public double Apply(double value)
        {
            double s = _Coefs[0];
            double d = 1.0;
            for (int i = 1; i < _Coefs.Length; i++)
            {
                d *= value;
                s += _Coefs[i] * d;
            }
            return s;
        }
    }
}
