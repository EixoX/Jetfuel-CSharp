using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX
{
    /// <summary>
    /// Represents the aspect of a class.
    /// </summary>
    public interface Aspect : ICustomAttributeProvider
    {
        /// <summary>
        /// Gets the data type of the class.
        /// </summary>
        Type DataType { get; }
        /// <summary>
        /// Gets the name of the data type of the class.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets the full name of the data type fo the class.
        /// </summary>
        string FullName { get; }
        /// <summary>
        /// Gets the number of members associated with the aspect.
        /// </summary>
        int Count { get; }
        /// <summary>s
        /// Gets an aspect member.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <returns>The aspect member on that position.</returns>
        AspectMember GetMember(int ordinal);
        /// <summary>
        /// Gets all aspect members.
        /// </summary>
        /// <returns>All the members as an array of members.</returns>
        AspectMember[] GetMemberArray();
        /// <summary>
        /// Gets aspect members by name.
        /// </summary>
        /// <param name="names">The names of the members.</param>
        /// <returns>The member array.</returns>
        AspectMember[] GetMemberArray(params string[] names);
        /// <summary>
        /// Gets an aspect member.
        /// </summary>
        /// <param name="name">The name of the aspect member.</param>
        /// <returns>The aspect member with that name.</returns>
        AspectMember GetMember(string name);
        /// <summary>
        /// Gets the ordinal position of an aspect member by name.
        /// </summary>
        /// <param name="name">The name of the member to look for.</param>
        /// <returns>The ordinal position of the member or -1 if not found.</returns>
        int GetOrdinal(string name);
        /// <summary>
        /// Gets the ordinal position of an aspect member by name.
        /// </summary>
        /// <param name="name">The name of the member to look for.</param>
        /// <returns>The ordinal position of the member or an argument exception if not found.</returns>
        int GetOrdinalOrException(string name);
        /// <summary>
        /// Indicates that the aspect has a member.
        /// </summary>
        /// <param name="name">The aspect member name to look for.</param>
        /// <returns>True if found.</returns>
        bool HasMember(string name);
        /// <summary>
        /// Enumerates the aspect members.
        /// </summary>
        /// <returns>An aspect member enumeration.</returns>
        IEnumerable<AspectMember> GetMembers();
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
