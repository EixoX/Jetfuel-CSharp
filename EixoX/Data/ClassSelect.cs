using System;
using System.Collections.Generic;
using System.Text;

/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Data
{
    /// <summary>
    /// Represents a selection of entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities selected.</typeparam>
    public class ClassSelect<T> :
        ClassFilterAspect<ClassSelect<T>>,
        ClassSortAspect<ClassSelect<T>>,
        IEnumerable<T>
    {

        private readonly ClassStorageEngine _Storage;
        private readonly DataAspect _Aspect;

        /// <summary>
        /// Constructs a class select.
        /// </summary>
        /// <param name="storage">The storage to read from.</param>
        /// <param name="aspect">The data aspect of the class</param>
        public ClassSelect(ClassStorageEngine storage, DataAspect aspect)
        {
            this._Storage = storage;
            this._Aspect = aspect;
        }

        /// <summary>
        /// Gets the storage to read from.
        /// </summary>
        public ClassStorageEngine Storage
        {
            get { return this._Storage; }
        }

        /// <summary>
        /// Gets the data aspect of the class.
        /// </summary>
        public DataAspect Aspect
        {
            get { return this._Aspect; }
        }

        #region Class Filter
        private ClassFilterNode _WhereFirst;
        private ClassFilterNode _WhereLast;

        /// <summary>
        /// Sets the given fiter as the only filter node.
        /// </summary>
        /// <param name="filter">The filter to set to the first filter node.</param>
        /// <returns>The ClassSelect of TEntity</returns>
        public ClassSelect<T> Where(ClassFilter filter)
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
        public ClassSelect<T> Where(int ordinal, FilterComparison comparison, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Sets a filter term as the only filter node.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> Where(int ordinal, object value)
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
        public ClassSelect<T> Where(string name, FilterComparison comparison, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Sets a filter term as the only filter node.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> Where(string name, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, name, value));
        }

        // <summary>
        /// Appends a filter with an AND operation.
        /// </summary>
        /// <param name="filter">The filter to append.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> And(ClassFilter filter)
        {
            if (this._WhereFirst == null)
            {
                this._WhereFirst = new ClassFilterNode(filter);
                this._WhereLast = this._WhereFirst;
            }
            else
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
        public ClassSelect<T> And(int ordinal, FilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an AND operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> And(int ordinal, object value)
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
        public ClassSelect<T> And(string name, FilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an AND operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> And(string name, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, value));
        }

        /// <summary>
        /// Appends a filter with an OR operation.
        /// </summary>
        /// <param name="filter">The filter to append.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> Or(ClassFilter filter)
        {
            if (this._WhereFirst == null)
            {
                this._WhereFirst = new ClassFilterNode(filter);
                this._WhereLast = this._WhereFirst;
            }
            else
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
        public ClassSelect<T> Or(int ordinal, FilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> Or(int ordinal, object value)
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
        public ClassSelect<T> Or(string name, FilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> Or(string name, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, name, value));
        }

        public ClassSelect<T> AppendAnd(string name, FilterComparison comparison, object value)
        {
            return this._WhereFirst == null ?
                Where(name, comparison, value) :
                And(name, comparison, value);
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
        /// <returns>The ClassSelect<T>.</returns>
        public ClassSelect<T> OrderBy(string name, SortDirection direction)
        {
            return OrderBy(direction, name);
        }

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="direction">The order direction.</param>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> OrderBy(SortDirection direction, params string[] names)
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
        public ClassSelect<T> OrderBy(params string[] names)
        {
            return OrderBy(SortDirection.Ascending, names);
        }

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the members used to filter.</param>
        /// <param name="direction">The sort diretion.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> OrderBy(int ordinal, SortDirection direction)
        {
            return OrderBy(direction, ordinal);
        }

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="direction">The sort diretion.</param>
        /// <param name="ordinals">The ordinals of the members to order by.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> OrderBy(SortDirection direction, params int[] ordinals)
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
        public ClassSelect<T> OrderBy(params int[] ordinals)
        {
            return OrderBy(SortDirection.Ascending, ordinals);
        }

        /// <summary>
        /// Appends an ordering to the selection.
        /// </summary>
        /// <param name="name">The name of the member to use for ordering.</param>
        /// <param name="direction">The order direction.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> ThenBy(string name, SortDirection direction)
        {
            return ThenBy(direction, name);
        }

        /// <summary>
        /// Appends an ordering to the selection.
        /// </summary>
        /// <param name="direction">The order direction.</param>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> ThenBy(SortDirection direction, params string[] names)
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
        public ClassSelect<T> ThenBy(params string[] names)
        {
            return ThenBy(SortDirection.Ascending, names);
        }

        /// <summary>
        /// Appends ordering to the selection.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the members used to filter.</param>
        /// <param name="direction">The sort diretion.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> ThenBy(int ordinal, SortDirection direction)
        {
            return ThenBy(direction, ordinal);
        }

        /// <summary>
        /// Appends more ordering of the selection.
        /// </summary>
        /// <param name="direction">The sort diretion.</param>
        /// <param name="ordinals">The ordinals of the members to order by.</param>
        /// <returns>The T.</returns>
        public ClassSelect<T> ThenBy(SortDirection direction, params int[] ordinals)
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
        public ClassSelect<T> ThenBy(params int[] ordinals)
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
        public ClassSelect<T> Page(int pageSize, int pageOrdinal)
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
        public IEnumerator<T> GetEnumerator()
        {
            return _Storage.Select<T>(_Aspect, _WhereFirst, _OrderFirst, _PageSize, _PageOrdinal).GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Storage.Select<T>(_Aspect, _WhereFirst, _OrderFirst, _PageSize, _PageOrdinal).GetEnumerator();
        }


        /// <summary>
        /// Gets the single result of the select.
        /// </summary>
        /// <returns>The first result or default(T).</returns>
        public T SingleResult()
        {
            using (IEnumerator<T> ie = _Storage.Select<T>(_Aspect, _WhereFirst, _OrderFirst, _PageSize, _PageOrdinal).GetEnumerator())
            {
                return ie.MoveNext() ? ie.Current : default(T);
            }
        }

        /// <summary>
        /// Gets the total class count of the select.
        /// </summary>
        /// <returns>The number of classes available for the enumeration.</returns>
        public long Count()
        {
            return _Storage.Count(_Aspect, _WhereFirst);
        }

        /// <summary>
        /// Indicates that the selection exists.
        /// </summary>
        /// <returns>True if at least one class of the select exists.</returns>
        public bool Exists()
        {
            return _Storage.Exists(_Aspect, _WhereFirst);
        }

        /// <summary>
        /// Executes select and creates a select result based on this select.
        /// </summary>
        /// <returns>A class select result.</returns>
        public ClassSelectResult<T> ToResult()
        {
            return new ClassSelectResult<T>(this);
        }

        public ClassSelect<T> Search(string filter, params string[] fields)
        {
            if (string.IsNullOrEmpty(filter))
                return this;
            else
            {
                ClassFilterExpression expression = new ClassFilterExpression(this._Aspect, fields[0], FilterComparison.Like, filter);
                for (int i = 1; i < fields.Length; i++)
                    expression.Or(fields[i], FilterComparison.Like, filter);
                if (this._WhereFirst == null)
                    return Where(expression);
                else
                    return And(expression);
            }
        }
        /// <summary>
        /// Enumerates the selected entities as a key value pair of options.
        /// </summary>
        /// <returns>An enumeration of key and value pairs.</returns>
        public IEnumerable<KeyValuePair<object, object>> ToOptions()
        {
            int identityOrdinal = _Aspect.IdentityOrdinal;
            if (identityOrdinal >= 0)
            {
                foreach (T entity in this)
                    yield return new KeyValuePair<object, object>(_Aspect[identityOrdinal].GetValue(entity), entity);
            }
            else if (_Aspect.HasUniqueMembers)
            {
                using (IEnumerator<int> uniqueOrdinalEnum = _Aspect.UniqueMemberOrdinals.GetEnumerator())
                {
                    uniqueOrdinalEnum.MoveNext();
                    int uniqueOrdinal = uniqueOrdinalEnum.Current;
                    foreach (T entity in this)
                        yield return new KeyValuePair<object, object>(_Aspect[uniqueOrdinal].GetValue(entity), entity);
                }

            }
            else
            {
                throw new ArgumentException("To enumerate options the entity must have an identity or unique member: " + _Aspect.DataType.ToString());
            }
        }


        public Collections.Tree<T> ToTree(string primaryKeyName, string parentReferenceName)
        {
            DataAspectMember primaryKeyMember = this._Aspect[primaryKeyName];
            DataAspectMember parentReferenceMember = this._Aspect[parentReferenceName];
            Collections.Tree<T> tree = new Collections.Tree<T>();

            foreach (T item in this)
            {
                object key = parentReferenceMember.GetValue(item);
                Collections.TreeNode<T> parent = null;
                if (!ValidationHelper.IsNullOrEmpty(key))
                {
                    parent = tree.DepthSearch(primaryKeyMember, key);
                }
                if (parent != null)
                {
                    parent.AddLast(item);
                }
                else
                {
                    tree.AddLast(item);
                }
            }

            return tree;
        }


        public IEnumerable<object> GetMember(int memberOrdinal)
        {
            foreach (T item in this)
                yield return _Aspect[memberOrdinal].GetValue(item);
        }

        public IEnumerable<object> GetMember(string memberName)
        {
            return GetMember(_Aspect.GetOrdinalOrException(memberName));
        }

        public ClassSelect<T> OrderByRandom()
        {
            this._OrderFirst = new ClassSortNode(this._Aspect, -1, SortDirection.Random);
            this._OrderLast = this._OrderFirst;
            return this;
        }
    }
}
