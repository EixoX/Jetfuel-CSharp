using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class Histogram<T> : IEnumerable<KeyValuePair<T, int>>
    {
        private readonly IDictionary<T, int> _AbsFrequencies;
        private int _TotalCount;
        private int _ModeCount;
        private int _MinFreq;
        private T _MinFreqValue;
        private int _MaxFreq;
        private T _MaxFreqValue;
        private T _ModeValue;

        public Histogram()
        {
            this._AbsFrequencies = new SortedDictionary<T, int>();
            this._TotalCount = 0;
            this._ModeCount = 0;
        }

        public Histogram(IEnumerable<T> items)
            : this()
        {
            foreach (T item in items)
                Acknowledge(item);
        }

        public void Clear()
        {
            this._AbsFrequencies.Clear();
            this._TotalCount = 0;
        }

        public void Acknowledge(T item)
        {
            int count;
            if (_AbsFrequencies.TryGetValue(item, out count))
            {
                count++;
                _AbsFrequencies[item] = count;
                if (count > _ModeCount)
                {
                    _ModeCount = count;
                    _ModeValue = item;
                }
                if (count > _MaxFreq)
                {
                    _MaxFreq = count;
                    _MaxFreqValue = item;
                }
                if (item.Equals(_MinFreqValue))
                {
                    _MinFreqValue = item;
                    _MinFreq = count;
                }
            }
            else
            {
                _AbsFrequencies.Add(item, 1);
                _MinFreq = 1;
                _MinFreqValue = item;
            }

            _TotalCount++;
        }

        public int TotalCount { get { return _TotalCount; } }
        public int ValueCount { get { return _AbsFrequencies.Count; } }
        public int MinFreq { get { return this._MinFreq; } }
        public int MaxFreq { get { return this._MaxFreq; } }

        public int this[T value]
        {
            get
            {
                int count;
                return _AbsFrequencies.TryGetValue(value, out count) ? count : 0;
            }
        }

        public double Frequency(T value)
        {
            return ((double)this[value]) / _TotalCount;
        }

        public T Mode
        {
            get { return _ModeValue; }
        }

        public int ModeCount
        {
            get { return this._ModeCount; }
        }

        public double ModeFrequency
        {
            get { return ((double)_ModeCount) / _TotalCount; }
        }


        public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
        {
            return _AbsFrequencies.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _AbsFrequencies.GetEnumerator();
        }
    }
}
