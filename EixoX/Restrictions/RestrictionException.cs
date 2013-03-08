using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Restrictions
{
    [Serializable]
    public class RestrictionException : Exception
    {
        public RestrictionException() { }
        public RestrictionException(string msg) : base(msg) { }
    }
}
