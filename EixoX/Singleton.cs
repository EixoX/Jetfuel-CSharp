using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public class Singleton<T> where T : new()
    {
        private readonly T child;
        private Singleton() { this.child = new T(); }

        private static Singleton<T> _Instance;

        public static T Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new Singleton<T>();
                return _Instance.child;
            }
        }
    }
}
