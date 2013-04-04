using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class Matrix<T> : IMatrix<T>
    {
        private readonly T[,] _values;
        private readonly int _rows;
        private readonly int _cols;

        public Matrix(int rows, int cols)
        {
            this._values = new T[rows, cols];
            this._rows = rows;
            this._cols = cols;
        }

        public int Rows
        {
            get { return this._rows; }
        }

        public int Cols
        {
            get { return this._cols; }
        }

        public T this[int i, int j]
        {
            get { return this._values[i, j]; }
            set { this._values[i, j] = value; }
        }
    }
}
