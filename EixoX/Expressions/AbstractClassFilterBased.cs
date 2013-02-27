using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Expressions
{
    public abstract class AbstractClassFilterBased<TClass> : ClassFilterBased<TClass>
    {
        private readonly Aspect _Aspect;
        private ClassFilterNode _FilterFirst;
        private ClassFilterNode _FilterLast;

        protected AbstractClassFilterBased(Aspect aspect)
        {
            this._Aspect = aspect;
        }

        public Aspect GetAspect()
        {
            return this._Aspect;
        }

        public ClassFilterNode Filter { get { return this._FilterFirst; } }

        protected abstract TClass This { get; }

        public TClass Where(ClassFilter filter)
        {
            this._FilterFirst = new ClassFilterNode(filter);
            this._FilterLast = this._FilterFirst;
            return This;
        }

        public TClass Where(int ordinal, ClassFilterComparison comparison, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        public TClass Where(int ordinal, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, ordinal, value));
        }

        public TClass Where(string name, ClassFilterComparison comparison, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        public TClass Where(string name, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, name, value));
        }

        public TClass And(ClassFilter filter)
        {
            this._FilterLast = this._FilterLast.SetNext(ClassFilterOperation.And, filter);
            return This;
        }

        public TClass And(int ordinal, ClassFilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        public TClass And(int ordinal, object value)
        {
            return And(new ClassFilterTerm(_Aspect, ordinal, value));
        }

        public TClass And(string name, ClassFilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        public TClass And(string name, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, value));
        }

        public TClass Or(ClassFilter filter)
        {
            this._FilterLast = this._FilterLast.SetNext(ClassFilterOperation.Or, filter);
            return This;
        }

        public TClass Or(int ordinal, ClassFilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        public TClass Or(int ordinal, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, ordinal, value));
        }

        public TClass Or(string name, ClassFilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        public TClass Or(string name, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, name, value));
        }
    }
}
