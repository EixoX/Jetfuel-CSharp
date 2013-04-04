using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica.CellularAutomata
{
    public struct ElementaryIntLattice : Lattice<bool>
    {
        private int _Value;
        private bool _Closed;

        public ElementaryIntLattice(int value, bool closed)
        {
            this._Value = value;
            this._Closed = closed;
        }
        public ElementaryIntLattice(int value) : this(value, false) { }


        public bool IsClosed
        {
            get { return _Closed; }
            set { this._Closed = value; }
        }

        public bool this[int index]
        {
            get
            {
                if (index >= 0 && index < 32)
                    return (_Value & (1 << index)) > 0;
                else if (_Closed)
                    return false;
                else if (index < 0)
                    return this[32 + index];
                else
                    return this[index % 32];
            }
            set
            {
                if (index >= 0 && index < 32)
                    this._Value |= (1 << index);
                else if (_Closed)
                    return;
                else if (index < 0)
                    this[32 + index] = value;
                else
                    this[index % 32] = value;
            }
        }


        public Lattice<bool> Clone()
        {
            return this;
        }


        public int Size
        {
            get { return 32; }
        }
    }
}
