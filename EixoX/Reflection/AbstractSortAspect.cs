using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Reflection
{
    public class AbstractSortAspect<TClass> : ClassSortAspect<TClass>
    {
        public TClass OrderBy(string name, SortDirection direction)
        {
            throw new NotImplementedException();
        }

        public TClass OrderBy(SortDirection direction, params string[] names)
        {
            throw new NotImplementedException();
        }

        public TClass OrderBy(params string[] names)
        {
            throw new NotImplementedException();
        }

        public TClass OrderBy(int ordinal, SortDirection direction)
        {
            throw new NotImplementedException();
        }

        public TClass OrderBy(SortDirection direction, params int[] ordinals)
        {
            throw new NotImplementedException();
        }

        public TClass OrderBy(params int[] ordinals)
        {
            throw new NotImplementedException();
        }

        public TClass ThenBy(string name, SortDirection direction)
        {
            throw new NotImplementedException();
        }

        public TClass ThenBy(SortDirection direction, params string[] names)
        {
            throw new NotImplementedException();
        }

        public TClass ThenBy(params string[] names)
        {
            throw new NotImplementedException();
        }

        public TClass ThenBy(int ordinal, SortDirection direction)
        {
            throw new NotImplementedException();
        }

        public TClass ThenBy(SortDirection direction, params int[] ordinals)
        {
            throw new NotImplementedException();
        }

        public TClass ThenBy(params int[] ordinals)
        {
            throw new NotImplementedException();
        }
    }
}
