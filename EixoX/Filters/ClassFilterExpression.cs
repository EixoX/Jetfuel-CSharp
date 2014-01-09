using System;
using System.Collections.Generic;
using System.Text;


/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Data
{
    /// <summary>
    /// Represents a class filter expression.
    /// </summary>
    public class ClassFilterExpression : ClassFilter
    {
        private readonly ClassFilterNode _first;
        private readonly Aspect _Aspect;
        private ClassFilterNode _last;

        /// <summary>
        /// Creates a new filter expression with a single filter node.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public ClassFilterExpression(ClassFilter filter)
        {
            this._Aspect = filter.Aspect;
            this._first = new ClassFilterNode(filter);
            this._last = this._first;
        }

        /// <summary>
        /// Creates a filter expression with a single filter term.
        /// </summary>
        /// <param name="aspect">The aspect to use.</param>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        public ClassFilterExpression(Aspect aspect, int ordinal, FilterComparison comparison, object value)
            : this(new ClassFilterTerm(aspect, ordinal, comparison, value)) { }

        /// <summary>
        /// Creates a filter expression with a single filter term.
        /// </summary>
        /// <param name="aspect">The class aspect to filter.</param>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        public ClassFilterExpression(Aspect aspect, int ordinal, object value)
            : this(new ClassFilterTerm(aspect, ordinal, value)) { }

        /// <summary>
        /// Creates a filter expression with a single filter term.
        /// </summary>
        /// <param name="aspect">The class aspect to filter.</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        public ClassFilterExpression(Aspect aspect, string name, FilterComparison comparison, object value)
            : this(new ClassFilterTerm(aspect, name, comparison, value)) { }

        /// <summary>
        /// Creates a filter expression with a single filter term.
        /// </summary>
        /// <param name="aspect">The class aspect to filter.</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        public ClassFilterExpression(Aspect aspect, string name, object value)
            : this(new ClassFilterTerm(aspect, name, value)) { }

        /// <summary>
        /// Gets the class aspect to filter.
        /// </summary>
        public Aspect Aspect { get { return this._first.Aspect; } }

        /// <summary>
        /// Gets the first filter node.
        /// </summary>
        public ClassFilterNode First { get { return this._first; } }

        /// <summary>
        /// Gets the last filter node.
        /// </summary>
        public ClassFilterNode Last { get { return this._last; } }

        /// <summary>
        /// Tests if an entity passes this filter.
        /// </summary>
        /// <param name="entity">The entity to test.</param>
        /// <returns>True if the entity passes the filter.</returns>
        public bool FilterPass(object entity)
        {
            return _first.FilterPass(entity);
        }

        /// <summary>
        /// Filters entities on an enumeration.
        /// </summary>
        /// <typeparam name="T">The type of entity to enumerate.</typeparam>
        /// <param name="entities">The source enumeration of entities.</param>
        /// <returns>An enumeration of filtered entities.</returns>
        public IEnumerable<T> FilterPass<T>(IEnumerable<T> entities)
        {
            return _first.FilterPass<T>(entities);
        }

        /// <summary>
        /// Appends a filter with an AND operation.
        /// </summary>
        /// <param name="filter">The filter to append.</param>
        /// <returns>The ClassFilterExpression.</returns>
        public ClassFilterExpression And(ClassFilter filter)
        {
            this._last = this._last.SetNext(FilterOperation.And, filter);
            return this;
        }

        /// <summary>
        /// Appens a filter term with an AND operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        /// <returns>The ClassFilterExpression.</returns>
        public ClassFilterExpression And(int ordinal, FilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an AND operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The ClassFilterExpression.</returns>
        public ClassFilterExpression And(int ordinal, object value)
        {
            return And(new ClassFilterTerm(_Aspect, ordinal, value));
        }

        /// <summary>
        /// Appends a filter term term with an AND operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        /// <returns>The ClassFilterExpression.</returns>
        public ClassFilterExpression And(string name, FilterComparison comparison, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an AND operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The ClassFilterExpression.</returns>
        public ClassFilterExpression And(string name, object value)
        {
            return And(new ClassFilterTerm(_Aspect, name, value));
        }

        /// <summary>
        /// Appends a filter with an OR operation.
        /// </summary>
        /// <param name="filter">The filter to append.</param>
        /// <returns>The ClassFilterExpression.</returns>
        public ClassFilterExpression Or(ClassFilter filter)
        {
            this._last = this._last.SetNext(FilterOperation.Or, filter);
            return this;
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        /// <returns>The ClassFilterExpression.</returns>
        public ClassFilterExpression Or(int ordinal, FilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, ordinal, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The ClassFilterExpression.</returns>
        public ClassFilterExpression Or(int ordinal, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, ordinal, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        /// <returns>The ClassFilterExpression.</returns>
        public ClassFilterExpression Or(string name, FilterComparison comparison, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, name, comparison, value));
        }

        /// <summary>
        /// Appends a filter term with an OR operation.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        /// <returns>The ClassFilterExpression.</returns>
        public ClassFilterExpression Or(string name, object value)
        {
            return Or(new ClassFilterTerm(_Aspect, name, value));
        }
        

        
    }
}
