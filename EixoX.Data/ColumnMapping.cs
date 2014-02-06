using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public interface ColumnMapping : Column
    {

        /// <summary>
        /// Gets the value from an object.
        /// </summary>
        /// <param name="entity">The object to read from.</param>
        /// <returns>The value of the object.</returns>
        object GetValue(object entity);

        /// <summary>
        /// Set the value to an object.
        /// </summary>
        /// <param name="entity">The entity to write to.</param>
        /// <param name="value">The value to write.</param>
        void SetValue(object entity, object value);


    }
}
