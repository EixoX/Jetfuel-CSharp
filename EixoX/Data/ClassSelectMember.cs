using System;
using System.Collections.Generic;
using System.Text;

/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Data
{
    /// <summary>
    /// Represents a selection of class member values.
    /// </summary>
    public class ClassSelectMember :
        IEnumerable<object>,
        ClassFilterAspect<ClassSelectMember>,
        ClassSortAspect<ClassSelectMember>
    {
        private readonly ClassStorageEngine _Storage;
        private readonly DataAspect _Aspect;
        private readonly int _Ordinal;

        /// <summary>
        /// Construcs a class member value selection.
        /// </summary>
        /// <param name="storage">The storage to get values from.</param>
        /// <param name="aspect">The aspect of the class.</param>
        /// <param name="ordinal">The ordinal position of the member.</param>
        public ClassSelectMember(ClassStorageEngine storage, DataAspect aspect, int ordinal)
        {
            this._Storage = storage;
            this._Aspect = aspect;
            this._Ordinal = ordinal;
        }

        /// <summary>
        /// Construcs a class member value selection.
        /// </summary>
        /// <param name="storage">The storage to get values from.</param>
        /// <param name="aspect">The aspect of the class.</param>
        /// <param name="name">The name of the member.</param>
        public ClassSelectMember(ClassStorageEngine storage, DataAspect aspect, string name)
            : this(storage, aspect, aspect.GetOrdinalOrException(name)) { }

        /// <summary>
        /// Gets the class storage to use.
        /// </summary>
        public ClassStorageEngine Storage { get { return this.Storage; } }
        /// <summary>
        /// Gets the data aspect.
        /// </summary>
        public DataAspect Aspect { get { return this._Aspect; } }
        /// <summary>
        /// Gets the member ordinal.
        /// </summary>
        public int Ordinal { get { return this._Ordinal; } }
        /// <summary>
        /// Gets the actual member.
        /// </summary>
        public DataAspectMember Member { get { return _Aspect[_Ordinal]; } }


        #region Class Filter
        private ClassFilterNode _WhereFirst;
        private ClassFilterNode _WhereLast;

        /// <summary>
        /// Sets the given fiter as the only filter node.
        /// </summary>
        /// <param name="filter">The filter to set to the first filter node.</param>
        /// <returns>The ClassSelect of TEntity</returns>
        public ClassSelectMember Where(ClassFilter filter)
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
        public ClassSelectMember Where(int ordinal, FilterComparison comparison, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Sets a filter term as the only filter node.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember Where(int ordinal, object value)
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
        public ClassSelectMember Where(string name, FilterComparison comparison, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Sets a filter term as the only filter node.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember Where(string name, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, name, value));
        }

        // <summary>
        /// Appends a filter with an AND operation.
        /// </summary>
        /// <param name="filter">The filter to append.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember And(ClassFilter filter)
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
        public ClassSelectMember And(int ordinal, FilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an AND operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember And(int ordinal, object value)
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
        public ClassSelectMember And(string name, FilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an AND operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember And(string name, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, value));
        }

        /// <summary>
        /// Appends a filter with an OR operation.
        /// </summary>
        /// <param name="filter">The filter to append.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember Or(ClassFilter filter)
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
        public ClassSelectMember Or(int ordinal, FilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember Or(int ordinal, object value)
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
        public ClassSelectMember Or(string name, FilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember Or(string name, object value)
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
        /// <returns>The ClassSelectMember.</returns>
        public ClassSelectMember OrderBy(string name, SortDirection direction)
        {
            return OrderBy(direction, name);
        }

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="direction">The order direction.</param>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember OrderBy(SortDirection direction, params string[] names)
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
        public ClassSelectMember OrderBy(params string[] names)
        {
            return OrderBy(SortDirection.Ascending, names);
        }

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the members used to filter.</param>
        /// <param name="direction">The sort diretion.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember OrderBy(int ordinal, SortDirection direction)
        {
            return OrderBy(direction, ordinal);
        }

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="direction">The sort diretion.</param>
        /// <param name="ordinals">The ordinals of the members to order by.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember OrderBy(SortDirection direction, params int[] ordinals)
        {
            this._OrderFirst = new ClassSortNode(_Aspect, ordinals[0], direction);
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
        public ClassSelectMember OrderBy(params int[] ordinals)
        {
            return OrderBy(SortDirection.Ascending, ordinals);
        }

        /// <summary>
        /// Appends an ordering to the selection.
        /// </summary>
        /// <param name="name">The name of the member to use for ordering.</param>
        /// <param name="direction">The order direction.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember ThenBy(string name, SortDirection direction)
        {
            return ThenBy(direction, name);
        }

        /// <summary>
        /// Appends an ordering to the selection.
        /// </summary>
        /// <param name="direction">The order direction.</param>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember ThenBy(SortDirection direction, params string[] names)
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
        public ClassSelectMember ThenBy(params string[] names)
        {
            return ThenBy(SortDirection.Ascending, names);
        }

        /// <summary>
        /// Appends ordering to the selection.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the members used to filter.</param>
        /// <param name="direction">The sort diretion.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember ThenBy(int ordinal, SortDirection direction)
        {
            return ThenBy(direction, ordinal);
        }

        /// <summary>
        /// Appends more ordering of the selection.
        /// </summary>
        /// <param name="direction">The sort diretion.</param>
        /// <param name="ordinals">The ordinals of the members to order by.</param>
        /// <returns>The T.</returns>
        public ClassSelectMember ThenBy(SortDirection direction, params int[] ordinals)
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
        public ClassSelectMember ThenBy(params int[] ordinals)
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
        public ClassSelectMember Page(int pageSize, int pageOrdinal)
        {
            this._PageSize = pageSize;
            this._PageOrdinal = pageOrdinal;
            return this;
        }
        #endregion


        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<object> GetEnumerator()
        {
            return _Storage.SelectMember(_Aspect, _Ordinal, _WhereFirst, _OrderFirst, _PageSize, _PageOrdinal).GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Storage.SelectMember(_Aspect, _Ordinal, _WhereFirst, _OrderFirst, _PageSize, _PageOrdinal).GetEnumerator();
        }

        /// <summary>
        /// Gets the single result of the member select.
        /// </summary>
        /// <returns>The first value or null.</returns>
        public object SingleResult()
        {
            using (IEnumerator<object> ie = _Storage.SelectMember(_Aspect, _Ordinal, _WhereFirst, _OrderFirst, _PageSize, _PageOrdinal).GetEnumerator())
            {
                return ie.MoveNext() ? ie.Current : null;
            }
        }

        /// <summary>
        /// Gets the single result of the member select.
        /// </summary>
        /// <typeparam name="T">The type to convert the result.</typeparam>
        /// <returns>A first value or default.</returns>
        public T SingleResult<T>()
        {
            object value = SingleResult();
            return value == null ? default(T) : (T)Convert.ChangeType(value, typeof(T));
        }

        public ClassSelectMember OrderByRandom()
        {
            this._OrderFirst = new ClassSortNode(this._Aspect, -1, SortDirection.Random);
            this._OrderLast = this._OrderFirst;
            return this;
        }
    }
}
