using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica.CellularAutomata
{
    public class ElementaryAutomaton
        : Automaton<Lattice<bool>, ElementaryRule>
    {
        private Lattice<bool> _Lattice;
        private ElementaryRule _Rule;

        public ElementaryAutomaton(Lattice<bool> lattice, ElementaryRule rule)
        {
            this._Lattice = lattice;
            this._Rule = rule;
        }

        public void Evolve()
        {
            this._Lattice = _Lattice.Clone();
            int sz = _Lattice.Size;
            for (int i = 0; i < sz; i++)
                _Lattice[i] =
                    _Rule.Next(_Lattice[i - 1], _Lattice[i], _Lattice[i + 1]);
        }

        public Lattice<bool> Lattice { get { return this._Lattice; } }
        public ElementaryRule Rule { get { return this._Rule; } }

    }
}
