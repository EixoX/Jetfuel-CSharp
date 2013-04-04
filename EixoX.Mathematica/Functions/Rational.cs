using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica.Functions
{
    public class Rational : Function<double, double>
    {
        private Function<double, double> _N;
        private Function<double, double> _D;

        public Rational(Function<double, double> n, Function<double, double> d)
        {
            this._N = n;
            this._D = d;
        }

        public Function<double, double> N
        {
            get { return this._N; }
            set { this._N = value; }
        }

        public Function<double, double> D
        {
            get { return this._D; }
            set { this._D = value; }
        }




        public double Apply(double value)
        {
            return this._N.Apply(value) / this._D.Apply(value);
        }
    }
}
