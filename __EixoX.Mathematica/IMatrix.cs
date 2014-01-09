using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public interface IMatrix<T>
    {
        int Rows { get; }
        int Cols { get; }

        T this[int i, int j] { get; set; }

    }
}
