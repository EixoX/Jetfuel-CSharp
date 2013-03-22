using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class R1Term: R1
    {
        private R1 _Coefficient;
        private R1 _Power;

        public R1Term(R1 coefficient, R1 power)
        {
            this._Coefficient = coefficient;
            this._Power = power;
        }

        public R1Term(R1 coefficient) : this(coefficient, new R1Constant(1.0)) { }

        public R1 Coefficient
        {
            get { return this._Coefficient; }
            set { this._Coefficient = value; }
        }

        public R1 Power
        {
            get { return this._Coefficient; }
            set { this._Coefficient = value; }
        }

        public double Calc(double x)
        {
            throw new NotImplementedException();
        }
    }
}
