using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public static class EnumerationHelper
    {
        public static IEnumerable<List<T>> Segmentate<T>(IEnumerable<T> source, int size)
        {
            int count = 0;
            List<T> list = new List<T>(size);

            foreach (T item in source)
            {
                list.Add(item);
                count++;

                if (count > 0 && count % size == 0)
                {
                    yield return list;
                    list = new List<T>(size);
                }
            }

            if (list.Count > 0)
                yield return list;
        }

        public static IEnumerable<T[]> Pack<T>(IEnumerable<T> source, int size)
        {
            T[] arr = new T[size];
            using (IEnumerator<T> enu = source.GetEnumerator())
            {
                int i;
                while (enu.MoveNext())
                {
                    for (i = 0; i < size && enu.MoveNext(); i++)
                        arr[i] = enu.Current;
                    for (; i < size; i++)
                        arr[i] = default(T);
                    yield return arr;
                }
            }
        }
    }
}
