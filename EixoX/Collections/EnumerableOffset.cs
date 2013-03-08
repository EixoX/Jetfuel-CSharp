using System;
using System.Collections.Generic;
using System.Text;
/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Collections
{
    /// <summary>
    /// Offsets an enumerator a specific number of records.
    /// </summary>
    /// <typeparam name="T">The type of entity to enumerate.</typeparam>
    public class EnumerableOffset<T> : IEnumerable<T>
    {
        private readonly int _Offset;
        private readonly IEnumerable<T> _Source;

        /// <summary>
        /// Construct an enumerable offset.
        /// </summary>
        /// <param name="source">The enumerable source.</param>
        /// <param name="offset">The number to offset.</param>
        public EnumerableOffset(IEnumerable<T> source, int offset)
        {
            this._Source = source;
            this._Offset = offset;
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            using (IEnumerator<T> e = _Source.GetEnumerator())
            {
                for (int i = 0; i < _Offset && e.MoveNext(); i++) ;
                while (e.MoveNext())
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
                for (int i = 0; i < _Offset && e.MoveNext(); i++) ;
                while (e.MoveNext())
                    yield return e.Current;
            }
        }
    }
}
