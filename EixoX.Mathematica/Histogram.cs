using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class Histogram<T> : IEnumerable<KeyValuePair<T, int>>
    {
        private readonly SortedList<T, int> _Counter;
        private int _CountTotal;
        private T _Mode;
        private int _ModeCount;

        public Histogram()
        {
            this._Counter = new SortedList<T, int>();
        }

        public Histogram(IEnumerable<T> collection)
        {
            this._Counter = new SortedList<T, int>();
            foreach (T item in collection)
                Add(item);
        }

        public int Add(T item)
        {
            int count;
            if (_Counter.TryGetValue(item, out count))
            {
                count++;
                _Counter[item] = count;
                if (count > _ModeCount)
                {
                    _Mode = item;
                    _ModeCount = count;
                }
            }
            else
            {
                _Counter.Add(item, 1);
                if (_ModeCount == 0)
                {
                    _Mode = item;
                    _ModeCount = 1;
                }
            }

            _CountTotal++;
            return count;
        }

        public void Clear()
        {
            this._Counter.Clear();
            _CountTotal = 0;
        }

        public int CountTotal
        {
            get { return this._CountTotal; }
        }

        public int CountDistinct
        {
            get { return this._Counter.Count; }
        }

        public int this[T item]
        {
            get
            {
                int c;
                return _Counter.TryGetValue(item, out c) ? c : 0;
            }
        }

        public T Mode
        {
            get { return this._Mode; }
        }

        public int ModeCount
        {
            get { return this._ModeCount; }
        }

        public IList<T> Items
        {
            get { return this._Counter.Keys; }
        }

        public IList<int> Frequencies
        {
            get { return this._Counter.Values; }
        }



        public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
        {
            return _Counter.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Counter.GetEnumerator();
        }
    }
}
