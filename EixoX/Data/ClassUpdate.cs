using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    /// <summary>
    /// Represents a class update command.
    /// </summary>
    public class ClassUpdate : 
        ClassFilterAspect<ClassUpdate>
    {
        private readonly ClassStorageEngine _Storage;
        private readonly DataAspect _Aspect;
        private readonly LinkedList<AspectMemberValue> _Values;

        /// <summary>
        /// Constructs a class update.
        /// </summary>
        /// <param name="storage">The data storage.</param>
        /// <param name="aspect">The data aspect.</param>
        public ClassUpdate(ClassStorageEngine storage, DataAspect aspect)
        {
            this._Storage = storage;
            this._Aspect = aspect;
            this._Values = new LinkedList<AspectMemberValue>();
        }

        public int Execute()
        {
            return this._Storage.Update(this._Aspect, this._Values, this._WhereFirst);
        }

        /// <summary>
        /// Gets the data sotrage.
        /// </summary>
        public ClassStorageEngine Storage { get { return this._Storage; } }
        /// <summary>
        /// Gets the data aspect.
        /// </summary>
        public DataAspect Aspect { get { return this._Aspect; } }
        /// <summary>
        /// Gets the update values.
        /// </summary>
        public LinkedList<AspectMemberValue> Values { get { return this._Values; } }

        /// <summary>
        /// Sets a value for an update.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value of the update.</param>
        /// <returns>This ClassUpdate.</returns>
        public ClassUpdate Set(int ordinal, object value)
        {
            this._Values.AddLast(new AspectMemberValue(_Aspect, ordinal, value));
            return this;
        }
        /// <summary>
        /// Sets a value for an update.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value of the update.</param>
        /// <returns>The ClassUpdate.</returns>
        public ClassUpdate Set(string name, object value)
        {
            this._Values.AddLast(new AspectMemberValue(_Aspect, name, value));
            return this;
        }

        #region Class Filter
        private ClassFilterNode _WhereFirst;
        private ClassFilterNode _WhereLast;

        /// <summary>
        /// Sets the given fiter as the only filter node.
        /// </summary>
        /// <param name="filter">The filter to set to the first filter node.</param>
        /// <returns>The ClassSelect of TEntity</returns>
        public ClassUpdate Where(ClassFilter filter)
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
        public ClassUpdate Where(int ordinal, FilterComparison comparison, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Sets a filter term as the only filter node.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassUpdate Where(int ordinal, object value)
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
        public ClassUpdate Where(string name, FilterComparison comparison, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Sets a filter term as the only filter node.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassUpdate Where(string name, object value)
        {
            return Where(new ClassFilterTerm(_Aspect, name, value));
        }

        // <summary>
        /// Appends a filter with an AND operation.
        /// </summary>
        /// <param name="filter">The filter to append.</param>
        /// <returns>The T.</returns>
        public ClassUpdate And(ClassFilter filter)
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
        public ClassUpdate And(int ordinal, FilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an AND operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassUpdate And(int ordinal, object value)
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
        public ClassUpdate And(string name, FilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an AND operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassUpdate And(string name, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, value));
        }

        /// <summary>
        /// Appends a filter with an OR operation.
        /// </summary>
        /// <param name="filter">The filter to append.</param>
        /// <returns>The T.</returns>
        public ClassUpdate Or(ClassFilter filter)
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
        public ClassUpdate Or(int ordinal, FilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassUpdate Or(int ordinal, object value)
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
        public ClassUpdate Or(string name, FilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The T.</returns>
        public ClassUpdate Or(string name, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, name, value));
        }

        #endregion
    }
}
