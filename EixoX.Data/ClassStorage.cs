using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    /// <summary>
    /// Represents a class storage interface.
    /// </summary>
    public interface ClassStorage
    {

        /// <summary>
        /// Creates a delete class command.
        /// </summary>
        /// <returns>The delete command.</returns>
        ClassDelete Delete();

        /// <summary>
        /// Deletes an object from the storage.
        /// </summary>
        /// <param name="entity">The object to delete.</param>
        /// <returns>True if successful.</returns>
        bool Delete(object entity);

        /// <summary>
        /// Creates an update object command.
        /// </summary>
        /// <returns>The update object command.</returns>
        ClassUpdate Update();

        /// <summary>
        /// Updates a given object on the storage.
        /// </summary>
        /// <param name="entity">The object to update.</param>
        /// <returns>True if successful.</returns>
        bool Update(object entity);

        /// <summary>
        /// Creates an insert object command.
        /// </summary>
        /// <returns>The insert object command.</returns>
        ClassInsert Insert();

        /// <summary>
        /// Inserts a given object on the storage.
        /// </summary>
        /// <param name="entity">The object to insert.</param>
        /// <returns>True if successful.</returns>
        bool Insert(object entity);

        /// <summary>
        /// Saves a given object on the storage.
        /// </summary>
        /// <param name="entity">The object to save.</param>
        /// <returns>True if successful.</returns>
        bool Save(object entity);

        /// <summary>
        /// Creates a select object command.
        /// </summary>
        /// <returns>The select command.</returns>
        ClassSelect Select();

    }
}
