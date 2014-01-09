using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX
{
    /// <summary>
    /// Represents an acessor for a class.
    /// </summary>
    public interface ClassAcessor : ICustomAttributeProvider
    {
        /// <summary>
        /// Gets the data type of the acessor.
        /// </summary>
        Type DataType { get; }
        /// <summary>
        /// Gets the name of the acessor.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets the member information of the acessor.
        /// </summary>
        MemberInfo MemberInfo { get; }
        /// <summary>
        /// Gets the value from an entity.
        /// </summary>
        /// <param name="entity">The entity to read from.</param>
        /// <returns>A member value.</returns>
        object GetValue(object entity);
        /// <summary>
        /// Sets value on an entity.
        /// </summary>
        /// <param name="entity">The entity to write to.</param>
        /// <param name="value">The value to set.</param>
        void SetValue(object entity, object value);
        /// <summary>
        /// Sets value on an entity.
        /// </summary>
        /// <param name="entity">The entity to write to.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="formatProvider">The format provider to use if changing object types.</param>
        void SetValue(object entity, object value, IFormatProvider formatProvider);
        /// <summary>
        /// Gets an attribute.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to get.</typeparam>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>The attribute or default.</returns>
        TAttribute GetAttribute<TAttribute>(bool inherit);
        /// <summary>
        /// Gets typed attributes.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to get.</typeparam>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>An array of typed attributes.</returns>
        TAttribute[] GetAttributes<TAttribute>(bool inherit);
        /// <summary>
        /// Indicates that an attribute exists.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attributes.</typeparam>
        /// <param name="inherit">The </param>
        /// <returns></returns>
        bool HasAttribute<TAttribute>(bool inherit);

        /// <summary>
        /// Indicates that the acessor can read values from instances.
        /// </summary>
        bool CanRead { get; }

        /// <summary>
        /// Indicates that the acessor can write values to instances.
        /// </summary>
        bool CanWrite { get; }


    }
}
