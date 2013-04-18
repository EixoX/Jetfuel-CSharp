using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public struct LazyConstructor<T>
        where T : new()
    {
        private T _Value;


        public T Value
        {
            get
            {
                if (_Value == null)
                    _Value = new T();
                return _Value;
            }
        }


        public static implicit operator T(LazyConstructor<T> item)
        {
            return item.Value;
        }



    }
}
