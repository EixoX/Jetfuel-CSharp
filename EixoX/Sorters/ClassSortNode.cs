using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public sealed class ClassSortNode : ClassSort
    {
        private readonly ClassSort _Term;
        private readonly Aspect _Aspect;
        private ClassSortNode _next;

        public ClassSortNode(ClassSort sort)
        {
            this._Term = sort;
            this._Aspect = sort.Aspect;
        }

        public ClassSortNode(Aspect aspect, int ordinal, SortDirection direction)
            : this(new ClassSortTerm(aspect, ordinal, direction)) { }

        public ClassSortNode(Aspect aspect, string name, SortDirection direction)
            : this(aspect, aspect.GetOrdinalOrException(name), direction) { }


        public Aspect Aspect { get { return _Term.Aspect; } }

        public ClassSort Term { get { return this._Term; } }

        public ClassSortNode Next
        {
            get { return this._next; }
        }

        public ClassSortNode SetNext(int ordinal, SortDirection direction)
        {
            this._next = new ClassSortNode(_Aspect, ordinal, direction);
            return this._next;
        }

        public ClassSortNode SetNext(string name, SortDirection direction)
        {
            this._next = new ClassSortNode(_Aspect, name, direction);
            return this._next;
        }

        public IEnumerable<T> Sort<T>(IEnumerable<T> entities)
        {
            return this._next == null ?
                _Term.Sort<T>(entities) :
                _next.Sort<T>(_Term.Sort<T>(entities));
        }

    }
}
