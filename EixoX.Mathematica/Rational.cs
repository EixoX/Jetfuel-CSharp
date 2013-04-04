using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    /// <summary>
    /// Represents a rational number
    /// </summary>
    public struct Rational : Field<Rational>
    {
        public long n;
        public long d;

        public Rational(long n, long d)
        {
            this.n = n;
            this.d = d;
        }

        public double ToDouble()
        {
            return ((double)this.n) / this.d;
        }

        public static Rational Simplify(long n, long d)
        {
            return new Rational(n, d);
        }

        public Rational Add(Rational other)
        {
            return Simplify(
                (this.n * other.d) + (this.d * other.n),
                this.d * other.d);
        }

        public Rational Subtract(Rational other)
        {
            return Simplify(
                (this.n * other.d) - (this.d * other.n),
                this.d * other.d);
        }

        public Rational Multiply(Rational other)
        {
            return Simplify(
                this.n * other.n,
                this.d * other.d);
        }

        public Rational Divide(Rational other)
        {
            return Simplify(
                this.n * other.d,
                this.d * other.n);
        }

        public Rational Negate()
        {
            return new Rational(-this.n, this.d);
        }

        public Rational Inverse()
        {
            return new Rational(this.d, this.n);
        }



        public bool EqualTo(Rational other)
        {
            return ToDouble() == other.ToDouble();
        }

        public bool GreaterThan(Rational other)
        {
            return this.ToDouble() > other.ToDouble();
        }

        public bool GreaterOrEqual(Rational other)
        {
            return this.ToDouble() >= other.ToDouble();
        }

        public bool LowerThan(Rational other)
        {
            return this.ToDouble() < other.ToDouble();
        }

        public bool LowerOrEqual(Rational other)
        {
            return this.ToDouble() <= other.ToDouble();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this.n.ToString(format, formatProvider) + "/" + this.d.ToString(format, formatProvider);
        }

        public int CompareTo(Rational other)
        {
            return this.ToDouble().CompareTo(other.ToDouble());
        }

        public bool Equals(Rational other)
        {
            return this.ToDouble().Equals(other.ToDouble());
        }
    }
}
