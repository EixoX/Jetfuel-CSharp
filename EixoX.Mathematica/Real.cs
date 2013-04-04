using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public struct Real : Field<Real>
    {
        public double x;

        public Real(double x)
        {
            this.x = x;
        }


        public Real Add(Real other) { return new Real(this.x + other.x); }
        public Real Subtract(Real other) { return new Real(this.x - other.x); }
        public Real Multiply(Real other) { return new Real(this.x * other.x); }
        public Real Divide(Real other) { return new Real(this.x / other.x); }
        public Real Negate() { return new Real(-this.x); }
        public Real Inverse() { return new Real(1.0 / this.x); }
        public bool EqualTo(Real other) { return this.x == other.x; }
        public bool GreaterThan(Real other) { return this.x > other.x; }
        public bool GreaterOrEqual(Real other) { return this.x >= other.x; }
        public bool LowerThan(Real other) { return this.x < other.x; }
        public bool LowerOrEqual(Real other) { return this.x <= other.x; }
        public string ToString(string format, IFormatProvider formatProvider) { return this.x.ToString(format, formatProvider); }
        public int CompareTo(Real other) { return this.x.CompareTo(other.x); }
        public bool Equals(Real other) { return this.x.Equals(other.x); }
    }
}
