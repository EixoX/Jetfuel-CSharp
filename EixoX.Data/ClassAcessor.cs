using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EixoX.Data
{
    /// <summary>
    /// Represents a generic class member acessor and an attribute provider.
    /// </summary>
    public interface ClassAcessor : ICustomAttributeProvider
    {

        /// <summary>
        /// Gets the value from a given object.
        /// </summary>
        /// <param name="entity">The object to read from.</param>
        /// <returns>The value read.</returns>
        object GetValue(object entity);

        /// <summary>
        /// Set the value to a given object.
        /// </summary>
        /// <param name="entity">The entity to write to.</param>
        /// <param name="value">The value to set.</param>
        void SetValue(object entity, object value);


        /// <summary>
        /// Gets the name of the class accessor.
        /// </summary>
        string Name { get; }
    }
}
