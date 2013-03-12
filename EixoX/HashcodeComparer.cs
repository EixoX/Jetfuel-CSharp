using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public class HashcodeComparer<T>
        : IEqualityComparer<T>
    {

        public bool Equals(T x, T y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
