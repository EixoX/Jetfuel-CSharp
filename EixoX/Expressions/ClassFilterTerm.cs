using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Expressions
{
    public struct ClassFilterTerm : ClassFilter
    {
        private readonly Aspect _aspect;
        private readonly int _ordinal;
        private readonly AspectMember _member;
        private readonly ClassFilterComparison _comparison;
        private readonly object _value;

        public ClassFilterTerm(Aspect aspect, int ordinal, ClassFilterComparison comparison, object value)
        {
            this._member = aspect.GetMember(ordinal);
            this._aspect = aspect;
            this._ordinal = ordinal;
            this._comparison = comparison;
            this._value = value;
        }

        public ClassFilterTerm(Aspect aspect, int ordinal, object value)
            : this(aspect, ordinal, ClassFilterComparison.EqualTo, value) { }

        public ClassFilterTerm(Aspect aspect, string name, ClassFilterComparison comparison, object value)
            : this(aspect, aspect.GetOrdinalOrException(name), comparison, value) { }

        public ClassFilterTerm(Aspect aspect, string name, object value)
            : this(aspect, aspect.GetOrdinalOrException(name), value) { }

        public int Ordinal
        {
            get { return this._ordinal; }
        }

        public ClassFilterComparison Comparison
        {
            get { return this._comparison; }
        }

        public object Value
        {
            get { return this._value; }
        }

        public Aspect GetAspect()
        {
            return this._aspect;
        }

        public AspectMember Member
        {
            get { return this._member; }
        }

        public bool FilterPass(object entity)
        {
            object value = _member.GetValue(entity);
            switch (this._comparison)
            {
                case ClassFilterComparison.EqualTo:
                    return value == null ? _value == null : value.Equals(_value);
                case ClassFilterComparison.NotEqualTo:
                    return value == null ? _value != null : !value.Equals(_value);
                case ClassFilterComparison.GreaterOrEqual:
                    return value == null || _value == null ? false : ((IComparable)value).CompareTo(_value) >= 0;
                case ClassFilterComparison.GreaterThan:
                    return value == null || _value == null ? false : ((IComparable)value).CompareTo(_value) > 0;
                case ClassFilterComparison.LowerOrEqual:
                    return value == null || _value == null ? false : ((IComparable)value).CompareTo(_value) <= 0;
                case ClassFilterComparison.LowerThan:
                    return value == null || _value == null ? false : ((IComparable)value).CompareTo(_value) < 0;
                case ClassFilterComparison.Like:
                    return value == null || _value == null ? false : value.ToString().IndexOf(_value.ToString(), StringComparison.OrdinalIgnoreCase) >= 0;
                case ClassFilterComparison.NotLike:
                    return value == null || _value == null ? true : value.ToString().IndexOf(_value.ToString(), StringComparison.OrdinalIgnoreCase) < 0;
                case ClassFilterComparison.InCollection:
                    if (value == null || _value == null)
                        return false;
                    else
                    {
                        foreach (object o in ((System.Collections.IEnumerable)_value))
                            if (_value.Equals(o))
                                return true;

                        return false;
                    }
                case ClassFilterComparison.NotInCollection:
                    if (value == null || _value == null)
                        return true;
                    else
                    {
                        foreach (object o in ((System.Collections.IEnumerable)_value))
                            if (_value.Equals(o))
                                return false;

                        return true;
                    }
                default:
                    throw new NotImplementedException("Unknown comparison " + _comparison);

            }
        }

        public IEnumerable<T> FilterPass<T>(IEnumerable<T> entities)
        {
            foreach (T item in entities)
                if (FilterPass(item))
                    yield return item;
        }
    }
}
