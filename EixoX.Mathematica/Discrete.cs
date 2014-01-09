using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public struct Discrete :
        Field<Discrete>
    {
        public long x;

        public Discrete(long value)
        {
            this.x = value;
        }

        public static readonly Discrete Zero = new Discrete(0);

        public Discrete Add(Discrete other) { return new Discrete(this.x + other.x); }
        public Discrete Subtract(Discrete other) { return new Discrete(this.x - other.x); }
        public Discrete Multiply(Discrete other) { return new Discrete(this.x * other.x); }
        public Discrete Divide(Discrete other) { return new Discrete(this.x / other.x); }
        public Discrete Negate() { return new Discrete(-this.x); }
        public Discrete Inverse() { return Zero; }
        public bool EqualTo(Discrete other) { return this.x == other.x; }
        public bool GreaterThan(Discrete other) { return this.x > other.x; }
        public bool GreaterOrEqual(Discrete other) { return this.x >= other.x; }
        public bool LowerThan(Discrete other) { return this.x < other.x; }
        public bool LowerOrEqual(Discrete other) { return this.x <= other.x; }
        public string ToString(string format, IFormatProvider formatProvider) { return this.x.ToString(format, formatProvider); }
        public int CompareTo(Discrete other) { return this.x.CompareTo(other.x); }
        public bool Equals(Discrete other) { return this.x.Equals(other.x); }
    }
}
