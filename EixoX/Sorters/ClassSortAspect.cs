using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public interface ClassSortAspect<TClass>
    {
        TClass OrderByRandom();

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="name">The name of the member to use for ordering.</param>
        /// <param name="direction">The order direction.</param>
        /// <returns>The T.</returns>
        TClass OrderBy(string name, SortDirection direction);

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="direction">The order direction.</param>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        TClass OrderBy(SortDirection direction, params string[] names);

        /// <summary>
        /// Sets the first ordering ascending of the selection.
        /// </summary>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        TClass OrderBy(params string[] names);

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the members used to filter.</param>
        /// <param name="direction">The sort diretion.</param>
        /// <returns>The T.</returns>
        TClass OrderBy(int ordinal, SortDirection direction);

        /// <summary>
        /// Sets the first ordering of the selection.
        /// </summary>
        /// <param name="direction">The sort diretion.</param>
        /// <param name="ordinals">The ordinals of the members to order by.</param>
        /// <returns>The T.</returns>
        TClass OrderBy(SortDirection direction, params int[] ordinals);

        /// <summary>
        /// Sets the first ordering (ascending) of the selection.
        /// </summary>
        /// <param name="ordinals">The ordinal positions of the members.</param>
        /// <returns>The T.</returns>
        TClass OrderBy(params int[] ordinals);

        /// <summary>
        /// Appends an ordering to the selection.
        /// </summary>
        /// <param name="name">The name of the member to use for ordering.</param>
        /// <param name="direction">The order direction.</param>
        /// <returns>The T.</returns>
        TClass ThenBy(string name, SortDirection direction);

        /// <summary>
        /// Appends an ordering to the selection.
        /// </summary>
        /// <param name="direction">The order direction.</param>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        TClass ThenBy(SortDirection direction, params string[] names);

        /// <summary>
        /// Appends ordering to the selection.
        /// </summary>
        /// <param name="names">The member names to use for ordering.</param>
        /// <returns>The T.</returns>
        TClass ThenBy(params string[] names);

        /// <summary>
        /// Appends ordering to the selection.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the members used to filter.</param>
        /// <param name="direction">The sort diretion.</param>
        /// <returns>The T.</returns>
        TClass ThenBy(int ordinal, SortDirection direction);

        /// <summary>
        /// Appends more ordering of the selection.
        /// </summary>
        /// <param name="direction">The sort diretion.</param>
        /// <param name="ordinals">The ordinals of the members to order by.</param>
        /// <returns>The T.</returns>
        TClass ThenBy(SortDirection direction, params int[] ordinals);

        /// <summary>
        /// Appends more ordering of the selection.
        /// </summary>
        /// <param name="ordinals">The ordinal positions of the members.</param>
        /// <returns>The T.</returns>
        TClass ThenBy(params int[] ordinals);
    }
}
