using System;
using System.Collections.Generic;
using System.Text;


/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Data
{
    /// <summary>
    /// Represents a filter node with a filter operation and a pointer no the next node.
    /// </summary>
    public class ClassFilterNode: ClassFilter
    {
        private readonly ClassFilter _Filter;
        private FilterOperation _Operation;
        private ClassFilterNode _Next;

        /// <summary>
        /// Creates a filter node wrapping a filter.
        /// </summary>
        /// <param name="filter">The filter to wrap.</param>
        public ClassFilterNode(ClassFilter filter)
        {
            this._Filter = filter;
        }

        /// <summary>
        /// Creates a filter node wrapping a filter term.
        /// </summary>
        /// <param name="aspect">The class aspect to filter.</param>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        public ClassFilterNode(Aspect aspect, int ordinal, FilterComparison comparison, object value)
            : this(new ClassFilterTerm(aspect, ordinal, comparison, value)) { }

        /// <summary>
        /// Creates a fitler node wrapping a filter term.
        /// </summary>
        /// <param name="aspect">The class aspect to filter</param>
        /// <param name="ordinal">The ordinal parameters of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        public ClassFilterNode(Aspect aspect, int ordinal, object value)
            : this(aspect, ordinal, FilterComparison.EqualTo, value) { }

        /// <summary>
        /// Creates a filter node wrapping a filter term.
        /// </summary>
        /// <param name="aspect">The class aspect to filter.</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="comparison">The comparison to make.</param>
        /// <param name="value">The value to compare to.</param>
        public ClassFilterNode(Aspect aspect, string name, FilterComparison comparison, object value)
            : this(aspect, aspect.GetOrdinalOrException(name), comparison, value) { }

        /// <summary>
        /// Creates a fitler node wrapping a filter term.
        /// </summary>
        /// <param name="aspect">The class aspect to filter</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value to compare equal to.</param>
        public ClassFilterNode(Aspect aspect, string name, object value)
            : this(aspect, aspect.GetOrdinalOrException(name), value) { }


        /// <summary>
        /// Gets the wrapped filter.
        /// </summary>
        public ClassFilter Filter { get { return this._Filter; } }

        /// <summary>
        /// Gets or sets the filter operation.
        /// </summary>
        public FilterOperation Operation { get { return this._Operation; } set { this._Operation = value; } }

        /// <summary>
        /// Gets or sets the next node.
        /// </summary>
        public ClassFilterNode Next { get { return this._Next; } set { this._Next = value; } }


        /// <summary>
        /// Sets the next node.
        /// </summary>
        /// <param name="operation">The operation to join the nodes.</param>
        /// <param name="filter">The filter of the next node.</param>
        /// <returns>The next node.</returns>
        public ClassFilterNode SetNext(FilterOperation operation, ClassFilter filter)
        {
            this._Operation = operation;
            this._Next = new ClassFilterNode(filter);
            return this._Next;
        }

        /// <summary>
        /// The aspect associated with the filter.
        /// </summary>
        public Aspect Aspect
        {
            get { return _Filter.Aspect; }
        }

        /// <summary>
        /// Checks if an entity passes the filter.
        /// </summary>
        /// <param name="entity">The entity to check.</param>
        /// <returns>True if the entity passes the filter.</returns>
        public bool FilterPass(object entity)
        {
            if (this._Next != null)
            {
                switch (this._Operation)
                {
                    case FilterOperation.And:
                        return _Filter.FilterPass(entity) && _Next.FilterPass(entity);
                    case FilterOperation.Or:
                        return _Filter.FilterPass(entity) || _Next.FilterPass(entity);
                    default:
                        throw new NotImplementedException("Unknown filter operation " + _Operation);
                }
            }
            else
            {
                return _Filter.FilterPass(entity);
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
            foreach (T entity in entities)
                if (FilterPass(entity))
                    yield return entity;
        }
    }
}
