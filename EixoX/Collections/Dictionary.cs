using System;
using System.Text;

namespace EixoX.Collections
{
    public class Dictionary<Tkey, Tvalue>
        : System.Collections.Generic.Dictionary<Tkey, Tvalue>
    {
        public Dictionary() { }
        public Dictionary(int capacity) : base(capacity) { }


        public Tvalue Get(Tkey key)
        {
            Tvalue v;
            return base.TryGetValue(key, out v) ? v : default(Tvalue);
        }

        public void Put(Tkey key, Tvalue value)
        {
            if (base.ContainsKey(key))
                base[key] = value;
            else
                base.Add(key, value);
        }
    }
}
