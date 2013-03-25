using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica.CellularAutomata
{
    public class ElementaryLattice : Lattice<Boolean>
    {
        private bool[] _Values;
        private bool _Closed;

        public ElementaryLattice(int size, bool closed)
        {
            this._Values = new bool[size];
            this._Closed = closed;
        }

        public ElementaryLattice(bool closed, params bool[] values)
        {
            this._Closed = closed;
            this._Values = values;
        }

        public int Size
        {
            get { return this._Values.Length; }
        }

        public bool this[int index]
        {
            get
            {
                if (index >= 0 && index <= _Values.Length)
                    return _Values[index];
                else if (_Closed)
                    return false;
                else if (index < 0)
                    return this[_Values.Length + index];
                else
                    return this[index % _Values.Length];
            }
            set
            {
                if (index >= 0 && index <= _Values.Length)
                    _Values[index] = value;
                else if (_Closed)
                    return;
                else if (index < 0)
                    this[_Values.Length + index] = value;
                else
                    _Values[index % _Values.Length] = value;
            }
        }

        public bool IsClosed
        {
            get { return this._Closed; }
            set { this._Closed = value; }
        }


        public Lattice<bool> Clone()
        {
            return new ElementaryLattice(_Closed, (bool[])_Values.Clone());
        }
    }
}
