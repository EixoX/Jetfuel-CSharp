using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ListExtensions
    {
        public static List<T> GetRandomMembers<T>(this List<T> list, int memberCount)
            where T : class
        {
            if (list.Count <= memberCount)
                return list;
            else
            {
                List<T> newList = new List<T>();
                int i = 0;
                while (i < memberCount)
                {
                    int position = new Random().Next(list.Count);
                    if (!newList.Contains(list[position]))
                    {
                        newList.Add(list[position]);
                        i++;
                    }
                }
                return newList;
            }
        }
    }
}
