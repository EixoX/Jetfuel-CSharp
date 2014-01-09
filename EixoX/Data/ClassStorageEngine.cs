using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    /// <summary>
    /// Represents a class storage engine.
    /// </summary>
    public interface ClassStorageEngine
    {
        /// <summary>
        /// Executes the deletion of classes based on a filter.
        /// </summary>
        /// <param name="aspect">The data aspect.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>The number of items affected.</returns>
        int Delete(DataAspect aspect, ClassFilter filter);

        /// <summary>
        /// Executes the insertion of a class based on its members.
        /// </summary>
        /// <param name="aspect">The data aspect.</param>
        /// <param name="values">The member values.</param>
        /// <param name="identityValue">The identity value (if present).</param>
        /// <returns>The number of items affected.</returns>
        int Insert(DataAspect aspect, IEnumerable<AspectMemberValue> values, out object identityValue);

        /// <summary>
        /// Executes the insertion of an enumeration of classes.
        /// </summary>
        /// <param name="aspect">The aspect of the type.</param>
        /// <param name="entities">The entity enumeration.</param>
        /// <returns>The number of records affected.</returns>
        int Insert(DataAspect aspect, System.Collections.IEnumerable entities);

        /// <summary>
        /// Executes an update of classes.
        /// </summary>
        /// <param name="aspect">The data aspect of the class.</param>
        /// <param name="values">The member update values.</param>
        /// <param name="filter">The update filter.</param>
        /// <returns>The number of items affected.</returns>
        int Update(DataAspect aspect, IEnumerable<AspectMemberValue> values, ClassFilter filter);

        /// <summary>
        /// Selects classes.
        /// </summary>
        /// <typeparam name="T">The type of class to select.</typeparam>
        /// <param name="aspect">The data aspect of the class.</param>
        /// <param name="filter">The filter to use.</param>
        /// <param name="sort">The sort to use.</param>
        /// <param name="pageSize">The size of the paged result.</param>
        /// <param name="pageOrdinal">The page ordinal.</param>
        /// <returns>An enumeration of classes.</returns>
        IEnumerable<T> Select<T>(DataAspect aspect, ClassFilter filter, ClassSort sort, int pageSize, int pageOrdinal);

        /// <summary>
        /// Selects a single member from the class storage.
        /// </summary>
        /// <typeparam name="T">The type of class to select.</typeparam>
        /// <param name="aspect">The data aspect of the class.</param>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="filter">The filter to use.</param>
        /// <param name="sort">The sort to use.</param>
        /// <param name="pageSize">The size of the paged result.</param>
        /// <param name="pageOrdinal">The page ordinal.</param>
        /// <returns>An enumeration of class member values.</returns>
        IEnumerable<object> SelectMember(DataAspect aspect, int ordinal, ClassFilter filter, ClassSort sort, int pageSize, int pageOrdinal);

        /// <summary>
        /// Gets the value of a specific member of a specific aspect.
        /// </summary>
        /// <param name="aspect">The data aspect of the class.</param>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="filter">The filter to use.</param>
        /// <returns>The filtered value.</returns>
        object GetMemberValue(DataAspect aspect, int ordinal, ClassFilter filter);

        /// <summary>
        /// Checks if a filter of a given aspect exists.
        /// </summary>
        /// <param name="aspect">The data aspect to filter.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>True if successful.</returns>
        bool Exists(DataAspect aspect, ClassFilter filter);

        /// <summary>
        /// Counts the number of items.
        /// </summary>
        /// <param name="aspect">The data aspect to filter.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>The number of items.</returns>
        long Count(DataAspect aspect, ClassFilter filter);


        /// <summary>
        /// Creates a search filter based on a string.
        /// </summary>
        /// <param name="aspect">The data aspect to filter.</param>
        /// <param name="filter">The content of the filter</param>
        /// <returns>The filter term or expression, or null if nothing is filtered.</returns>
        ClassFilter CreateSearchFilter(DataAspect aspect, string filter);
    }
}
