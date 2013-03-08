using System;
using System.Collections.Generic;
using System.Text;
/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Collections
{
    /// <summary>
    /// An enumerable pager.
    /// </summary>
    /// <typeparam name="T">The type of entity to enumerate.</typeparam>
    public class EnumerablePager<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _Source;
        private int _PageSize;
        private int _PageOrdinal;

        /// <summary>
        /// Constructs an enumerable pager.
        /// </summary>
        /// <param name="source">The original enumerable.</param>
        /// <param name="pageSize">The size of the page of the enumeration.</param>
        /// <param name="pageOrdinal">The page ordinal of the enumeration.</param>
        public EnumerablePager(IEnumerable<T> source, int pageSize, int pageOrdinal)
        {
            this._Source = source;
            this._PageSize = pageSize;
            this._PageOrdinal = pageOrdinal;
        }

        /// <summary>
        /// Executes the paging of the enumeration.
        /// </summary>
        /// <returns>An enumeration of T.</returns>
        private IEnumerable<T> Execute()
        {
            using (IEnumerator<T> e = _Source.GetEnumerator())
            {
                int startRecord = _PageSize * _PageOrdinal;
                int endRecord = _PageSize * (_PageOrdinal + 1);
                int count = 0;
                for (; count < startRecord && e.MoveNext(); count++) ;
                for (; count < endRecord && e.MoveNext(); count++)
                    yield return e.Current;
            }
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
