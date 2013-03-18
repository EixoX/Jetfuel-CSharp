using System;
using System.Collections.Generic;
using System.Text;
/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX
{
    /// <summary>
    /// Rerepsents an aspect member.
    /// </summary>
    public interface IAspectMember : System.Reflection.ICustomAttributeProvider
    {
        /// <summary>
        /// Gets the data type of the member.
        /// </summary>
        Type DataType{get;}

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        string Name{get;}

        /// <summary>
        /// Gets the member info of the member.
        /// </summary>
        System.Reflection.MemberInfo MemberInfo{get;}

        /// <summary>
        /// Gets a value from an entity.
        /// </summary>
        /// <param name="entity">The entity to read the value from.</param>
        /// <returns>The value read.</returns>
        object GetValue(object entity);

        /// <summary>
        /// Sets a value to a member.
        /// </summary>
        /// <param name="entity">The entity to set value on.</param>
        /// <param name="value">The value to set.</param>
        void SetValue(object entity, object value);

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
        
        
        
        
        
    }
}
