using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica.CellularAutomata
{
    /// <summary>
    /// Represents the elementary cellular automaton rule.
    /// </summary>
    public class ElementaryRule
    {
        private readonly byte _Value;

        public ElementaryRule(byte value)
        {
            this._Value = value;
        }

        public bool Next(bool left, bool center, bool right)
        {
            int bit =
                (left ? 4 : 0) +
                (center ? 2 : 0) +
                (right ? 1 : 0);

            return (_Value & (1 << bit)) > 0;
        }


    }
}
