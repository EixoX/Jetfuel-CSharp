using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public class Cached<TKey, TValue>
    {
        public struct Entry
        {
            public TValue value;
            public DateTime tdate;
        }

        private TimeSpan maxAge;
        private readonly Dictionary<TKey, Entry> dictionary = new Dictionary<TKey, Entry>();

        public Cached(TimeSpan maxAge)
        {
            this.maxAge = maxAge;
        }

        public TValue this[TKey key]
        {
            get
            {
                Entry entry;
                if (!dictionary.TryGetValue(key, out entry))
                    return default(TValue);
                else if (DateTime.Now.Subtract(entry.tdate) > maxAge)
                    return default(TValue);
                else
                    return entry.value;
            }
            set
            {
                Entry e = new Entry();
                e.tdate = DateTime.Now;
                e.value = value;
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key] = e;
                }
                else
                {
                    dictionary.Add(key, e);
                }
            }
        }


    }
}
