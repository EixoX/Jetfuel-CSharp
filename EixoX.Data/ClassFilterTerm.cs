using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public struct ClassFilterTerm
        : ClassFilter
    {
        private readonly ClassSchema _Schema;
        private readonly int _Ordinal;
        private readonly ClassFilterComparison _Comparison;
        private readonly object _Value;
        private readonly ClassAcessor _Acessor;

        public ClassFilterTerm(ClassSchema schema, int ordinal, ClassFilterComparison comparison, object value)
        {
            this._Schema = schema;
            this._Ordinal = ordinal;
            this._Comparison = comparison;
            this._Value = value;
            this._Acessor = schema[ordinal];
        }

        public ClassSchema Schema { get { return this._Schema; } }
        public int Ordinal { get { return this._Ordinal; } }
        public ClassFilterComparison Comparison { get { return this._Comparison; } }
        public object Value { get { return this._Value; } }
        public ClassAcessor Acessor { get { return this._Acessor; } }


        private sealed bool In(System.Collections.IEnumerable items, object item)
        {
            if (item != null && items != null)
                foreach (object o in items)
                    if (item.Equals(o))
                        return true;

            return false;
        }

        private sealed bool Like(object input, object value)
        {
            if (value == null)
                return input == null;
            else
                return input.ToString().IndexOf(value.ToString(), StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public bool FilterPass(object entity, object value)
        {
            object input = _Acessor.GetValue(entity);
            switch (this._Comparison)
            {
                case ClassFilterComparison.EqualTo:
                    return value == null ? input == null : value.Equals(input);
                case ClassFilterComparison.GreaterOrEqual:
                    return ((IComparable)input).CompareTo(value) >= 0;
                case ClassFilterComparison.GreaterThan:
                    return ((IComparable)input).CompareTo(value) > 0;
                case ClassFilterComparison.In:
                    return In((IEnumerable)value, input);
                case ClassFilterComparison.Like:
                    return Like(input, value);
                case ClassFilterComparison.LowerOrEqual:
                    return ((IComparable)input).CompareTo(value) <= 0;
                case ClassFilterComparison.LowerThan:
                    return ((IComparable)input).CompareTo(value) < 0;
                case ClassFilterComparison.Not_EqualTo:
                    return value == null ? input != null : !value.Equals(input);
                case ClassFilterComparison.Not_In:
                    return !In((IEnumerable)value, input);
                case ClassFilterComparison.Not_Like:
                    return !Like(input, value);
                default:
                    throw new NotImplementedException(_Comparison.ToString());
            }
        }

        public System.Collections.IEnumerable FilterPass(System.Collections.IEnumerable entities, object value)
        {
            foreach (Object o in entities)
                if (FilterPass(o, value))
                    yield return o;
        }
    }
}
