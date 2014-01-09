using System;
using System.Collections.Generic;
using System.Text;


/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Data
{
    /// <summary>
    /// Represents a class filter term used to filter classes.
    /// </summary>
    public struct ClassFilterTerm : ClassFilter
    {
        private readonly Aspect _Aspect;
        private readonly int _Ordinal;
        private readonly FilterComparison _Comparison;
        private readonly object _Value;

        /// <summary>
        /// Constructs a new class filter term.
        /// </summary>
        /// <param name="aspect">The class aspect to filter.</param>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        public ClassFilterTerm(Aspect aspect, int ordinal, FilterComparison comparison, object value)
        {
            this._Aspect = aspect;
            this._Ordinal = ordinal;
            this._Comparison = comparison;
            this._Value = value;
        }

        /// <summary>
        /// Constructs a new class filter term.
        /// </summary>
        /// <param name="aspect">The class aspect to filter.</param>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        public ClassFilterTerm(Aspect aspect, int ordinal, object value)
            : this(aspect, ordinal, FilterComparison.EqualTo, value) { }

        /// <summary>
        /// Constructs a new class filter term.
        /// </summary>
        /// <param name="aspect">The class aspect to filter.</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        public ClassFilterTerm(Aspect aspect, string name, FilterComparison comparison, object value)
            : this(aspect, aspect.GetOrdinalOrException(name), comparison, value) { }

        /// <summary>
        /// Constructs a new class filter term.
        /// </summary>
        /// <param name="aspect">The class aspect to filter.</param>
        /// <param name="name">The name of the membmer.</param>
        /// <param name="value">The value to compare equal to.</param>
        public ClassFilterTerm(Aspect aspect, string name, object value)
            : this(aspect, aspect.GetOrdinalOrException(name), value) { }

        /// <summary>
        /// Gets the ordinal position of the member.
        /// </summary>
        public int Ordinal
        {
            get { return this._Ordinal; }
        }

        /// <summary>
        /// Gets the comparison to make.
        /// </summary>
        public FilterComparison Comparison
        {
            get { return this._Comparison; }
        }

        /// <summary>
        /// Gets the value to compare to.
        /// </summary>
        public object Value
        {
            get { return this._Value; }
        }

        /// <summary>
        /// Gets the class aspect.
        /// </summary>
        public Aspect Aspect
        {
            get { return this._Aspect; }
        }

        /// <summary>
        /// Gets the member filtered.
        /// </summary>
        public AspectMember Member
        {
            get { return this._Aspect.GetMember(_Ordinal); }
        }


        /// <summary>
        /// Checks if an entity passes the filter.
        /// </summary>
        /// <param name="entity">The entity to check.</param>
        /// <returns>True if the entity passes the filter.</returns>
        public bool FilterPass(object entity)
        {
            object value = _Aspect.GetMember(_Ordinal).GetValue(entity);
            switch (this._Comparison)
            {
                case FilterComparison.EqualTo:
                    return value == null ? _Value == null : value.Equals(_Value);
                case FilterComparison.NotEqualTo:
                    return value == null ? _Value != null : !value.Equals(_Value);
                case FilterComparison.GreaterOrEqual:
                    return value == null || _Value == null ? false : ((IComparable)value).CompareTo(_Value) >= 0;
                case FilterComparison.GreaterThan:
                    return value == null || _Value == null ? false : ((IComparable)value).CompareTo(_Value) > 0;
                case FilterComparison.LowerOrEqual:
                    return value == null || _Value == null ? false : ((IComparable)value).CompareTo(_Value) <= 0;
                case FilterComparison.LowerThan:
                    return value == null || _Value == null ? false : ((IComparable)value).CompareTo(_Value) < 0;
                case FilterComparison.Like:
                    return value == null || _Value == null ? false : value.ToString().IndexOf(_Value.ToString(), StringComparison.OrdinalIgnoreCase) >= 0;
                case FilterComparison.NotLike:
                    return value == null || _Value == null ? true : value.ToString().IndexOf(_Value.ToString(), StringComparison.OrdinalIgnoreCase) < 0;
                case FilterComparison.InCollection:
                    if (value == null || _Value == null)
                        return false;
                    else
                    {
                        foreach (object o in ((System.Collections.IEnumerable)_Value))
                            if (_Value.Equals(o))
                                return true;

                        return false;
                    }
                case FilterComparison.NotInCollection:
                    if (value == null || _Value == null)
                        return true;
                    else
                    {
                        foreach (object o in ((System.Collections.IEnumerable)_Value))
                            if (_Value.Equals(o))
                                return false;

                        return true;
                    }
                default:
                    throw new NotImplementedException("Unknown comparison " + _Comparison);

            }
        }

        /// <summary>
        /// Enumerates the entities that pass the filter.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entities">The source enumeration.</param>
        /// <returns>An enumeration of filtered entities.</returns>
        public IEnumerable<T> FilterPass<T>(IEnumerable<T> entities)
        {
            foreach (T item in entities)
                if (FilterPass(item))
                    yield return item;
        }
    }
}
