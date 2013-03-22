using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class IntSample : IEnumerable<int>
    {
        private readonly List<int> _Values;
        private readonly Histogram<int> _Histogram;

        private int _min;
        private int _max;
        private int _sum;

        private void Initialize(IEnumerable<int> collection)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            int sum = 0;
            foreach (int v in collection)
            {
                if (v < min)
                    min = v;
                if (v > max)
                    max = v;

                sum += v;
                _Histogram.Acknowledge(v);
                _Values.Add(v);
            }

            this._min = min;
            this._max = max;
            this._sum = sum;
        }

        public IntSample()
        {
            this._Values = new List<int>();
            this._Histogram = new Histogram<int>();
        }

        public IntSample(int capacity)
        {
            this._Values = new List<int>(capacity);
            this._Histogram = new Histogram<int>();
        }

        public IntSample(IEnumerable<int> collection)
            : this()
        {
            Initialize(collection);
        }

        public IntSample(int capacity, IEnumerable<int> collection)
            : this(capacity)
        {
            Initialize(collection);
        }

        public int Min { get { return this._min; } }
        public int Max { get { return this._max; } }
        public int Avg { get { return (int)System.Math.Round(((double)_sum) / _Values.Count, 0); } }
        public Histogram<int> Histogram { get { return this._Histogram; } }
        public int Count { get { return _Values.Count; } }
        public int this[int i] { get { return _Values[i]; } }

        public IEnumerator<int> GetEnumerator()
        {
            return _Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Values.GetEnumerator();
        }
    }
}
