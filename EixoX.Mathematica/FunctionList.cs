using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class FunctionList<TDomain>
        : LinkedList<Function<TDomain, TDomain>>, Function<TDomain, TDomain>
    {
        public TDomain Apply(TDomain value)
        {
            for (LinkedListNode<Function<TDomain, TDomain>> node = this.First;
                node != null;
                node = node.Next)
                value = node.Value.Apply(value);

            return value;
        }
    }
}
