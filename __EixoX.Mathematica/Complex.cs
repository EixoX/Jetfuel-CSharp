using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    /// <summary>
    /// Represents a complex number.
    /// </summary>
    public struct Complex :
        IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>, IComparable<Complex>, IEquatable<Complex>
    {
        public double x;
        public double y;


        public Complex(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double AbsoluteValue()
        {
            return System.Math.Sqrt((this.x * this.x) + (this.y * this.y));
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() | y.GetHashCode();
        }
        public override string ToString()
        {
            return x.ToString() + "; " + y.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj is Complex)
                return this == (Complex)obj;
            else if (obj is IConvertible)
                return this == Convert.ToDouble(obj);
            else
                return base.Equals(obj);
        }

        public static bool operator ==(Complex a, Complex b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator ==(Complex a, double b)
        {
            return a.x == b && a.y == 0.0;
        }

        public static bool operator ==(double a, Complex b)
        {
            return a == b.x && b.y == 0.0;
        }

        public static bool operator !=(Complex a, Complex b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public static bool operator !=(Complex a, double b)
        {
            return a.x != b || a.y != 0.0;
        }

        public static bool operator !=(double a, Complex b)
        {
            return a != b.x || b.y != 0.0;
        }

        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.x + b.x, a.y + b.y);
        }

        public static Complex operator +(Complex a, double b)
        {
            return new Complex(a.x + b, a.y);
        }

        public static Complex operator +(double a, Complex b)
        {
            return new Complex(a + b.x, b.y);
        }

        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.x - b.x, a.y - b.y);
        }
        public static Complex operator -(Complex a, double b)
        {
            return new Complex(a.x - b, a.y);
        }
        public static Complex operator -(double a, Complex b)
        {
            return new Complex(a - b.x, -b.y);
        }

        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex((a.x * b.x) - (a.y * b.y), (a.y * b.x) + (a.x * b.y));
        }

        public static Complex operator *(Complex a, double b)
        {
            return new Complex(a.x * b, a.y * b);
        }

        public static Complex operator *(double a, Complex b)
        {
            return new Complex(a * b.x, a * b.y);
        }

        public static Complex operator /(Complex a, Complex b)
        {
            double div = (b.x * b.x) + (b.y * b.y);
            return new Complex(
                ((a.x * b.x) + (a.y * b.y)) / div,
                ((a.y * b.x) - (a.x * b.y)) / div);
        }

        public static Complex operator /(Complex a, double b)
        {
            double div = (b * b);
            return new Complex(
                (a.x * b) / div,
                (a.y * b) / div);
        }

        public static Complex operator /(double a, Complex b)
        {
            double div = (b.x * b.x) + (b.y * b.y);
            return new Complex(
                (a * b.x) / div,
                -(a * b.y) / div);
        }

        public int CompareTo(object obj)
        {
            if (obj is Complex)
                return this.AbsoluteValue().CompareTo(((Complex)obj).AbsoluteValue());
            else
                return this.AbsoluteValue().CompareTo(obj);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this.x.ToString(format, formatProvider) + "; " + this.y.ToString(format, formatProvider);
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Double;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return this.AbsoluteValue() > 0.0;
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(this.AbsoluteValue(), provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(this.AbsoluteValue(), provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(this.AbsoluteValue(), provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(this.AbsoluteValue(), provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return this.AbsoluteValue();
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(this.AbsoluteValue(), provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(this.AbsoluteValue(), provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(this.AbsoluteValue(), provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(this.AbsoluteValue(), provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(this.AbsoluteValue(), provider);
        }

        public string ToString(IFormatProvider provider)
        {
            return ToString(null, provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(this.AbsoluteValue(), conversionType, provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(this.AbsoluteValue(), provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(this.AbsoluteValue(), provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(this.AbsoluteValue(), provider);
        }

        public int CompareTo(double other)
        {
            return this.AbsoluteValue().CompareTo(other);
        }

        public bool Equals(double other)
        {
            return this.x == other && this.y == 0.0;
        }

        public int CompareTo(Complex other)
        {
            return this.AbsoluteValue().CompareTo(other.AbsoluteValue());
        }

        public bool Equals(Complex other)
        {
            return this.x == other.x && this.y == other.y;
        }

        public static Complex Polar(double r, double theta)
        {
            return new Complex(r * Math.Cos(theta), r * Math.Sin(theta));
        }

        public static Complex[] ToComplex(params double[] real)
        {
            Complex[] arr = new Complex[real.Length];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = new Complex(real[i], 0.0);
            return arr;
        }

        public Complex Conjugate()
        {
            return new Complex(this.x, -this.y);
        }


    }
}
