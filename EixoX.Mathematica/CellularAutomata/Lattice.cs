using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica.CellularAutomata
{
    public interface Lattice<TState>
    {
        bool IsClosed { get; }
        int Size { get; }
        TState this[int index]
        {
            get;
            set;
        }
        Lattice<TState> Clone();
    }
}
