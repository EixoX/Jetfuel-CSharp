using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Expressions
{
    public class ClassSortNode: ClassSort
    {
        private readonly Aspect _Aspect;
        private readonly int _Ordinal;
        private readonly ClassSortDiretion _Direction;
        private readonly AspectMember _Member;
        private ClassSortNode _next;

        public ClassSortNode(Aspect aspect, int ordinal, ClassSortDiretion direction)
        {
            this._Member = aspect.GetMember(ordinal);
            this._Aspect = aspect;
            this._Ordinal = ordinal;
            this._Direction = direction;
        }
        public ClassSortNode(Aspect aspect, string name, ClassSortDiretion direction)
            : this(aspect, aspect.GetOrdinalOrException(name), direction) { }


        public Aspect GetAspect()
        {
            return this._Aspect;
        }

        public int Ordinal
        {
            get { return this._Ordinal; }
        }

        public ClassSortDiretion Direction
        {
            get { return this._Direction; }
        }

        public AspectMember Member
        {
            get { return this._Member; }
        }

        public ClassSortNode Next
        {
            get { return this._next; }
        }

        public ClassSortNode SetNext(int ordinal, ClassSortDiretion direction)
        {
            this._next = new ClassSortNode(GetAspect(), ordinal, direction);
            return this._next;
        }

        public ClassSortNode SetNext(string name, ClassSortDiretion direction)
        {
            this._next = new ClassSortNode(GetAspect(), name, direction);
            return this._next;
        }

        public IEnumerable<T> Sort<T>(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
