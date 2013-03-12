using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    /// <summary>
    /// Represents the generic singleton class schema with all members.
    /// </summary>
    /// <typeparam name="T">The type to get the singleton for.</typeparam>
    public sealed class ClassSchema<T> : AbstractAspect<AspectMember>
    {
        private static ClassSchema<T> _instance;

        /// <summary>
        /// Constructs a class schema.
        /// </summary>
        private ClassSchema() : base(typeof(T)) { }

        /// <summary>
        /// Gets the only instance of the class schema for the type.
        /// </summary>
        public static ClassSchema<T> Instance
        {
            get { return _instance ?? (_instance = new ClassSchema<T>()); }
        }

        /// <summary>
        /// Creates a basic aspect member.
        /// </summary>
        /// <param name="acessor">The class acessor.</param>
        /// <param name="member">The built class member.</param>
        /// <returns>True</returns>
        protected override bool CreateAspectFor(ClassAcessor acessor, out AspectMember member)
        {
            member = new AspectMember(acessor);
            return true;
        }
    }
}
