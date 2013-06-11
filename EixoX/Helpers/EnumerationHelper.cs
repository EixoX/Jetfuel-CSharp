using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public static class EnumerationHelper
    {
        public static IEnumerable<T[]> Segmentate<T>(IEnumerable<T> source, int size)
        {
            int count = 0;
            T[] array = new T[size];

            foreach (T item in source)
            {
                if (count > 0 && (count % size == 0))
                {
                    yield return array;
                    array = new T[size];
                }

                array[count % size] = item;
                count++;
            }

            if (count % size != 0)
                yield return array;
        }
    }
}
