using System;
using System.Collections.Generic;
using System.Text;
/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Collections
{
    /// <summary>
    /// Wraps an ienumerable and limits it's results.
    /// </summary>
    /// <typeparam name="T">The type of entities to enumerate.</typeparam>
    public class EnumerableLimit<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _Source;
        private readonly int _limit;

        /// <summary>
        /// Constructs a new enumerable limit.
        /// </summary>
        /// <param name="source">The enumerable source.</param>
        /// <param name="limit">The number of records.</param>
        public EnumerableLimit(IEnumerable<T> source, int limit)
        {
            this._Source = source;
            this._limit = limit;
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            using (IEnumerator<T> e = _Source.GetEnumerator())
            {
                for (int i = 0; i < _limit && e.MoveNext(); i++)
                    yield return e.Current;
            }
        }
        
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            using (IEnumerator<T> e = _Source.GetEnumerator())
            {
                for (int i = 0; i < _limit && e.MoveNext(); i++)
                    yield return e.Current;
            }
        }
    }
}
