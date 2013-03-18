using System;
using System.Collections.Generic;
using System.Text;


/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Data
{
    /// <summary>
    /// A filter that can test entities and enumeration of entities.
    /// </summary>
    public interface ClassFilter
    {
        /// <summary>
        /// The aspect associated with the filter.
        /// </summary>
        Aspect Aspect { get; }
        
        /// <summary>
        /// Checks if an entity passes the filter.
        /// </summary>
        /// <param name="entity">The entity to check.</param>
        /// <returns>True if the entity passes the filter.</returns>
        bool FilterPass(object entity);
        
        /// <summary>
        /// Enumerates the entities that pass the filter.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entities">The source enumeration.</param>
        /// <returns>An enumeration of filtered entities.</returns>
        IEnumerable<T> FilterPass<T>(IEnumerable<T> entities);
    }
}
