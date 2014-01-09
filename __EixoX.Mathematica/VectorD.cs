using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class VectorD
    {
        private readonly double[] _Values;

        public VectorD(int dimension)
        {
            this._Values = new double[dimension];
        }

        public VectorD(params double[] values)
        {
            this._Values = values;
        }

        public int Dimension { get { return _Values.Length; } }

        public double this[int dimension]
        {
            get { return _Values[dimension]; }
            set { _Values[dimension] = value; }
        }

        public void Add(VectorD other)
        {
            if (this._Values.Length != other._Values.Length)
                throw new ArgumentException("Incompatible dimensions " + _Values.Length + " -> " + other._Values.Length);

            int imax = this._Values.Length;
            for (int i = 0; i < imax; i++)
                this._Values[i] += other._Values[i];
        }

        public void Subtract(VectorD other)
        {
            if (this._Values.Length != other._Values.Length)
                throw new ArgumentException("Incompatible dimensions " + _Values.Length + " -> " + other._Values.Length);

            int imax = this._Values.Length;
            for (int i = 0; i < imax; i++)
                this._Values[i] -= other._Values[i];
        }

        public static bool operator ==(VectorD a, VectorD b)
        {
            if (a._Values.Length != b._Values.Length)
                return false;
            int dim = a._Values.Length;
            for (int i = 0; i < dim; i++)
                if (a._Values[i] != b._Values[i])
                    return false;

            return true;
        }

        public static bool operator !=(VectorD a, VectorD b)
        {
            if (a._Values.Length != b._Values.Length)
                return true;
            int dim = a._Values.Length;
            for (int i = 0; i < dim; i++)
                if (a._Values[i] == b._Values[i])
                    return false;

            return false;
        }



        public override bool Equals(object obj)
        {
            if (obj is VectorD)
                return this == (VectorD)obj;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return _Values.GetHashCode();
        }
    }
}
