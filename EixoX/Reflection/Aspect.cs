using System;
using System.Collections.Generic;
using System.Text;
/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Reflection
{
   
    /// <summary>
    /// Represents the aspect of a class.
    /// </summary>
    public interface Aspect
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
        /// <summary>
        /// Gets an aspect member.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <returns>The aspect member on that position.</returns>
        AspectMember GetMember(int ordinal);
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
    }
}
