using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public class ClassSortExpression: ClassSort
    {
        private readonly ClassSortNode _First;
        private ClassSortNode _Last;

        public ClassSortExpression(ClassSort sort)
        {
            this._First = new ClassSortNode(sort);
            this._Last = this._First;
        }

        public ClassSortExpression(Aspect aspect, int ordinal, SortDirection direction)
            : this(new ClassSortTerm(aspect, ordinal, direction)) { }

        public ClassSortExpression(Aspect aspect, string name, SortDirection direction)
            : this(aspect, aspect.GetOrdinalOrException(name), direction) { }

        /// <summary>
        /// Appends an ordering to the selection.
        /// </summary>
        /// <param name="name">The name of the member to use for ordering.</param>
        /// <param name="direction">The order direction.</param>
        /// <returns>The T.</returns>
        public ClassSortExpression ThenBy(string name, SortDirection direction)
        {
            return ThenBy(direction, name);
        }

        /// <summary>
        /// Appends and ordering to the selection.
        /// </summary>
        /// <param name="direction">The order direction.</param>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        public ClassSortExpression ThenBy(SortDirection direction, params string[] names)
        {
            for (int i = 0; i < names.Length; i++)
                this._Last = this._Last.SetNext(names[i], direction);

            return this;
        }

        /// <summary>
        /// Appends ordering to the selection.
        /// </summary>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        public ClassSortExpression ThenBy(params string[] names)
        {
            return ThenBy(SortDirection.Ascending, names);
        }

        /// <summary>
        /// Appends ordering to the selection.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the members used to filter.</param>
        /// <param name="direction">The sort diretion.</param>
        /// <returns>The T.</returns>
        public ClassSortExpression ThenBy(int ordinal, SortDirection direction)
        {
            return ThenBy(direction, ordinal);
        }

        /// <summary>
        /// Appends more ordering of the selection.
        /// </summary>
        /// <param name="direction">The sort diretion.</param>
        /// <param name="ordinals">The ordinals of the members to order by.</param>
        /// <returns>The T.</returns>
        public ClassSortExpression ThenBy(SortDirection direction, params int[] ordinals)
        {
            for (int i = 0; i < ordinals.Length; i++)
                this._Last = this._Last.SetNext(ordinals[i], direction);

            return this;
        }

        /// <summary>
        /// Appends more ordering of the selection.
        /// </summary>
        /// <param name="ordinals">The ordinal positions of the members.</param>
        /// <returns>The T.</returns>
        public ClassSortExpression ThenBy(params int[] ordinals)
        {
            return ThenBy(SortDirection.Ascending, ordinals);
        }


        public ClassSortNode First
        {
            get { return this._First; }
        }

        public Aspect Aspect
        {
            get { return _First.Aspect; }
        }

        public IEnumerable<T> Sort<T>(IEnumerable<T> entities)
        {
            return _First.Sort<T>(entities);
        }
    }
}
