using System;
using System.Collections.Generic;
using System.Text;

/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Data
{
    /// <summary>
    /// Performs a selection on an enumerable.
    /// </summary>
    /// <typeparam name="T">The type of entity to enumerate.</typeparam>
    public class EnumerableSelect<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _Source;
        private readonly ClassSchema<T> _Aspect;

        /// <summary>
        /// Constructs a new selection based on a source and default Class Schema.
        /// </summary>
        /// <param name="source">The source to map.</param>
        public EnumerableSelect(IEnumerable<T> source)
        {
            this._Source = source;
            this._Aspect = ClassSchema<T>.Instance;
        }

        #region Class Filter
        private ClassFilterNode _WhereFirst;
        private ClassFilterNode _WhereLast;

        /// <summary>
        /// Sets the given fiter as the only filter node.
        /// </summary>
        /// <param name="filter">The filter to set to the first filter node.</param>
        /// <returns>The ClassSelect of TEntity</returns>
        public EnumerableSelect<T> Where(ClassFilter filter)
        {
            this._WhereFirst = new ClassFilterNode(filter);
            this._WhereLast = this._WhereFirst;
            return this;
        }

        /// <summary>
        /// Set a filter term as the only filter node.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        /// <returns>The ClassSelect of TEntity</returns>
        public EnumerableSelect<T> Where(int ordinal, FilterComparison comparison, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Sets a filter term as the only filter node.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> Where(int ordinal, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, ordinal, value));
        }

        /// <summary>
        /// Sets a filter term as the only filter node.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> Where(string name, FilterComparison comparison, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Sets a filter term as the only filter node.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> Where(string name, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, name, value));
        }

        // <summary>
        /// Appends a filter with an AND operation.
        /// </summary>
        /// <param name="filter">The filter to append.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> And(ClassFilter filter)
        {
            this._WhereLast = this._WhereLast.SetNext(FilterOperation.And, filter);
            return this;
        }

        /// <summary>
        /// Appens a filter term with an AND operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> And(int ordinal, FilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an AND operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> And(int ordinal, object value)
        {
            return And(new ClassFilterTerm(_Aspect, ordinal, value));
        }

        /// <summary>
        /// Appends a filter term term with an AND operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> And(string name, FilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an AND operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> And(string name, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, value));
        }

        /// <summary>
        /// Appends a filter with an OR operation.
        /// </summary>
        /// <param name="filter">The filter to append.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> Or(ClassFilter filter)
        {
            this._WhereLast = this._WhereLast.SetNext(FilterOperation.Or, filter);
            return this;
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> Or(int ordinal, FilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> Or(int ordinal, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, ordinal, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> Or(string name, FilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> Or(string name, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, name, value));
        }

        #endregion

        #region Class Sort
        private ClassSortNode _OrderFirst;
        private ClassSortNode _OrderLast;

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="name">The name of the member to use for ordering.</param>
        /// <param name="direction">The order direction.</param>
        /// <returns>The EnumerableSelect<T>.</returns>
        public EnumerableSelect<T> OrderBy(string name, SortDirection direction)
        {
            return OrderBy(direction, name);
        }

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="direction">The order direction.</param>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> OrderBy(SortDirection direction, params string[] names)
        {
            this._OrderFirst = new ClassSortNode(_Aspect, names[0], direction);
            this._OrderLast = this._OrderFirst;
            for (int i = 1; i < names.Length; i++)
                this._OrderLast = this._OrderLast.SetNext(names[i], direction);
            return this;
        }

        /// <summary>
        /// Sets the first ordering ascending of the selection.
        /// </summary>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> OrderBy(params string[] names)
        {
            return OrderBy(SortDirection.Ascending, names);
        }

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the members used to filter.</param>
        /// <param name="direction">The sort diretion.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> OrderBy(int ordinal, SortDirection direction)
        {
            return OrderBy(direction, ordinal);
        }

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="direction">The sort diretion.</param>
        /// <param name="ordinals">The ordinals of the members to order by.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> OrderBy(SortDirection direction, params int[] ordinals)
        {
            this._OrderFirst = new ClassSortNode(new ClassSortTerm(_Aspect, ordinals[0], direction));
            this._OrderLast = this._OrderFirst;
            for (int i = 1; i < ordinals.Length; i++)
                this._OrderLast = this._OrderLast.SetNext(ordinals[i], direction);
            return this;
        }

        /// <summary>
        /// Sets the first ordering (ascending) of the selection.
        /// </summary>
        /// <param name="ordinals">The ordinal positions of the members.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> OrderBy(params int[] ordinals)
        {
            return OrderBy(SortDirection.Ascending, ordinals);
        }

        /// <summary>
        /// Appends an ordering to the selection.
        /// </summary>
        /// <param name="name">The name of the member to use for ordering.</param>
        /// <param name="direction">The order direction.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> ThenBy(string name, SortDirection direction)
        {
            return ThenBy(direction, name);
        }

        /// <summary>
        /// Appends an ordering to the selection.
        /// </summary>
        /// <param name="direction">The order direction.</param>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> ThenBy(SortDirection direction, params string[] names)
        {
            for (int i = 0; i < names.Length; i++)
                this._OrderLast = this._OrderLast.SetNext(names[i], direction);
            return this;
        }

        /// <summary>
        /// Appends ordering to the selection.
        /// </summary>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> ThenBy(params string[] names)
        {
            return ThenBy(SortDirection.Ascending, names);
        }

        /// <summary>
        /// Appends ordering to the selection.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the members used to filter.</param>
        /// <param name="direction">The sort diretion.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> ThenBy(int ordinal, SortDirection direction)
        {
            return ThenBy(direction, ordinal);
        }

        /// <summary>
        /// Appends more ordering of the selection.
        /// </summary>
        /// <param name="direction">The sort diretion.</param>
        /// <param name="ordinals">The ordinals of the members to order by.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> ThenBy(SortDirection direction, params int[] ordinals)
        {
            for (int i = 0; i < ordinals.Length; i++)
                this._OrderLast = this._OrderLast.SetNext(ordinals[i], direction);
            return this;
        }

        /// <summary>
        /// Appends more ordering of the selection.
        /// </summary>
        /// <param name="ordinals">The ordinal positions of the members.</param>
        /// <returns>The T.</returns>
        public EnumerableSelect<T> ThenBy(params int[] ordinals)
        {
            return ThenBy(SortDirection.Ascending, ordinals);
        }

        #endregion

        #region ClassPage
        private int _PageSize;
        private int _PageOrdinal;
        /// <summary>
        /// Gets or sets the size of the page for paged results.
        /// </summary>
        public int PageSize
        {
            get { return this._PageSize; }
            set { this._PageSize = value; }
        }
        /// <summary>
        /// Gets or sets the ordinal of the page for paged results.
        /// </summary>
        public int PageOrdinal
        {
            get { return this._PageOrdinal; }
            set { this._PageOrdinal = value; }
        }
        /// <summary>
        /// Sets the paging of results.
        /// </summary>
        /// <param name="pageSize">The size of the page for paged results.</param>
        /// <param name="pageOrdinal">The ordinal of the page for paged results.</param>
        /// <returns>The calling class.</returns>
        public EnumerableSelect<T> Page(int pageSize, int pageOrdinal)
        {
            this._PageSize = pageSize;
            this._PageOrdinal = pageOrdinal;
            return this;
        }
        #endregion

        /// <summary>
        /// Executes the actual select.
        /// </summary>
        /// <returns>An enumeration of the actual select.</returns>
        private IEnumerable<T> Execute()
        {
            IEnumerable<T> source = this._Source;
            if (_WhereFirst != null)
                source = this._WhereFirst.FilterPass<T>(source);
            if (_OrderFirst != null)
                source = this._OrderFirst.Sort<T>(source);
            if (_PageSize > 0 && _PageOrdinal >= 0)
                source = new Collections.EnumerablePager<T>(source, _PageSize, _PageOrdinal);
            return source;

        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Execute().GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Execute().GetEnumerator();
        }
    }
}
