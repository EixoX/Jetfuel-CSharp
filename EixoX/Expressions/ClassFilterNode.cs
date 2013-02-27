using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Expressions
{
    public class ClassFilterNode: ClassFilter
    {
        private readonly ClassFilter _Filter;
        private ClassFilterOperation _Operation;
        private ClassFilterNode _Next;


        public ClassFilterNode(ClassFilter filter)
        {
            this._Filter = filter;
        }

        public ClassFilterNode(Aspect aspect, int ordinal, ClassFilterComparison comparison, object value)
            : this(new ClassFilterTerm(aspect, ordinal, comparison, value)) { }

        public ClassFilterNode(Aspect aspect, int ordinal, object value)
            : this(aspect, ordinal, ClassFilterComparison.EqualTo, value) { }

        public ClassFilterNode(Aspect aspect, string name, ClassFilterComparison comparison, object value)
            : this(aspect, aspect.GetOrdinalOrException(name), comparison, value) { }

        public ClassFilterNode(Aspect aspect, string name, object value)
            : this(aspect, aspect.GetOrdinalOrException(name), value) { }


        public ClassFilter Filter { get { return this._Filter; } }
        public ClassFilterOperation Operation { get { return this._Operation; } }
        public ClassFilterNode Next { get { return this._Next; } }


        public ClassFilterNode SetNext(ClassFilterOperation operation, ClassFilter filter)
        {
            this._Operation = operation;
            this._Next = new ClassFilterNode(filter);
            return this._Next;
        }


        public Aspect GetAspect()
        {
            return _Filter.GetAspect();
        }

        public bool FilterPass(object entity)
        {
            if (this._Next != null)
            {
                switch (this._Operation)
                {
                    case ClassFilterOperation.And:
                        return _Filter.FilterPass(entity) && _Next.FilterPass(entity);
                    case ClassFilterOperation.Or:
                        return _Filter.FilterPass(entity) || _Next.FilterPass(entity);
                    default:
                        throw new NotImplementedException("Unknown filter operation " + _Operation);
                }
            }
            else
            {
                return _Filter.FilterPass(entity);
            }
        }

        public IEnumerable<T> FilterPass<T>(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
                if (FilterPass(entity))
                    yield return entity;
        }
    }
}
