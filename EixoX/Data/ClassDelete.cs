using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Expressions;

namespace EixoX.Data
{
    public class ClassDelete : ClassFilterBased<ClassDelete>
    {
        private Aspect _aspect;
        private ClassFilterNode _WhereFirst;
        private ClassFilterNode _WhereLast;

        public ClassDelete Where(int ordinal, ClassFilterComparison comparison, object value)
        {
            this._WhereFirst = new ClassFilterNode(_aspect, ordinal, comparison, value));
            this._WhereLast = this._WhereFirst;
            return this;
        }

        public ClassDelete Where(int ordinal, object value)
        {
            this._WhereFirst = new ClassFilterNode(_aspect, ordinal, value));
            this._WhereLast = this._WhereFirst;
            return this;
        }

        public ClassDelete Where(string name, ClassFilterComparison comparison, object value)
        {
            this._WhereFirst = new ClassFilterNode(_aspect, name, comparison, value));
            this._WhereLast = this._WhereFirst;
            return this;
        }

        public ClassDelete Where(string name, object value)
        {
            this._WhereFirst = new ClassFilterNode(_aspect, name, value));
            this._WhereLast = this._WhereFirst;
            return this;
        }

        public ClassDelete And(int ordinal, ClassFilterComparison comparison, object value)
        {
            this._WhereLast = this._WhereLast.SetNext(ClassFilterOperation.And, new ClassFilterTerm(_aspect, ordinal, comparison, value));
            return this;
        }

        public ClassDelete And(int ordinal, object value)
        {
            this._WhereLast = this._WhereLast.SetNext(ClassFilterOperation.And, new ClassFilterTerm(_aspect, ordinal, comparison, value));
            return this;
        }

        public ClassDelete And(string name, ClassFilterComparison comparison, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete And(string name, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete Or(int ordinal, ClassFilterComparison comparison, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete Or(int ordinal, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete Or(string name, ClassFilterComparison comparison, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete Or(string name, object value)
        {
            throw new NotImplementedException();
        }
    }
}
