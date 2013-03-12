using System;
using System.Collections.Generic;
using System.Text;


/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Data
{
    /// <summary>
    /// Represents a delete command.
    /// </summary>
    public class ClassDelete
        : ClassFilterAspect<ClassDelete>
    {
        private readonly ClassStorageEngine _Storage;
        private readonly DataAspect _Aspect;
        
        /// <summary>
        /// Constructs a new class delete.
        /// </summary>
        /// <param name="storage">The storage to use.</param>
        /// <param name="aspect">The data aspect.</param>
        public ClassDelete(ClassStorageEngine storage, DataAspect aspect)
        {
            this._Storage = storage;
            this._Aspect = aspect;
        }

        /// <summary>
        /// Gets the data storage that will delete the classes.
        /// </summary>
        public ClassStorageEngine Storage { get { return this._Storage; } }
        /// <summary>
        /// Gets the data aspect.
        /// </summary>
        public DataAspect Aspect { get { return this._Aspect; } }

        /// <summary>
        /// Executes the delete.
        /// </summary>
        /// <returns>The number of items affected.</returns>
        public int Delete() { return _Storage.Delete(this._Aspect, this._WhereFirst); }


        #region Class Filter
        private ClassFilterNode _WhereFirst;
        private ClassFilterNode _WhereLast;

        /// <summary>
        /// Sets the given fiter as the only filter node.
        /// </summary>
        /// <param name="filter">The filter to set to the first filter node.</param>
        /// <returns>The ClassSelect of TEntity</returns>
        public ClassDelete Where(ClassFilter filter)
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
        public ClassDelete Where(int ordinal, FilterComparison comparison, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Sets a filter term as the only filter node.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassDelete Where(int ordinal, object value)
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
        public ClassDelete Where(string name, FilterComparison comparison, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Sets a filter term as the only filter node.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassDelete Where(string name, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, name, value));
        }

        // <summary>
        /// Appends a filter with an AND operation.
        /// </summary>
        /// <param name="filter">The filter to append.</param>
        /// <returns>The T.</returns>
        public ClassDelete And(ClassFilter filter)
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
        public ClassDelete And(int ordinal, FilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an AND operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassDelete And(int ordinal, object value)
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
        public ClassDelete And(string name, FilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an AND operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassDelete And(string name, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, value));
        }

        /// <summary>
        /// Appends a filter with an OR operation.
        /// </summary>
        /// <param name="filter">The filter to append.</param>
        /// <returns>The T.</returns>
        public ClassDelete Or(ClassFilter filter)
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
        public ClassDelete Or(int ordinal, FilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassDelete Or(int ordinal, object value)
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
        public ClassDelete Or(string name, FilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassDelete Or(string name, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, name, value));
        }

        #endregion

    }
}
