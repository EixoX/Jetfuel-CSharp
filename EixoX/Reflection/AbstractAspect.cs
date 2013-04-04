using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace EixoX
{
    /// <summary>
    /// Represents the base class for an aspect.
    /// </summary>
    /// <typeparam name="TMember">The type of members that the aspect holds.</typeparam>
    public abstract class AbstractAspect<TMember> : Aspect, IEnumerable<TMember>
        where TMember : AspectMember
    {
        private readonly List<TMember> _Members;
        private readonly Type _DataType;

        /// <summary>
        /// Creates a new aspec for a member accessor.
        /// </summary>
        /// <param name="acessor">The member accessor.</param>
        /// <param name="member">The member instance to create.</param>
        /// <returns>True if it's validated and created.</returns>
        protected abstract bool CreateAspectFor(ClassAcessor acessor, out TMember member);
        
        public AbstractAspect(Type dataType)
        {
            this._DataType = dataType;

            FieldInfo[] fields = dataType.GetFields();
            PropertyInfo[] properties = dataType.GetProperties();
            TMember member;

            this._Members = new List<TMember>(fields.Length + properties.Length);
            for (int i = 0; i < fields.Length; i++)
            {

                if (CreateAspectFor(new ClassField(fields[i]), out member))
                    _Members.Add(member);
            }
            for (int i = 0; i < properties.Length; i++)
            {
                if (CreateAspectFor(new ClassProperty(properties[i]), out member))
                    _Members.Add(member);
            }
        }

        public Type DataType { get { return this._DataType; } }
        public string Name { get { return this._DataType.Name; } }
        public string FullName { get { return this._DataType.FullName; } }

        public TMember this[int ordinal]
        {
            get { return _Members[ordinal]; }
        }

        public TMember GetMemberOrDefault(string name)
        {
            int ordinal = GetOrdinal(name);
            return ordinal >= 0 ? _Members[ordinal] : default(TMember);
        }

        public int Count { get { return this._Members.Count; } }

        public int GetOrdinal(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                int count = _Members.Count;
                for (int i = 0; i < count; i++)
                    if (name.Equals(_Members[i].Name, StringComparison.OrdinalIgnoreCase))
                        return i;
            }
            return -1;
        }
        
        public int GetOrdinalOrException(string name)
        {
            int ordinal = GetOrdinal(name);
            if (ordinal < 0)
                throw new ArgumentOutOfRangeException("name", name + " is not on aspect " + _DataType.FullName);
            else
                return ordinal;
        }

        public int[] GetOrdinalsOrException(string[] names)
        {
            int[] ordinals = new int[names.Length];
            for (int i = 0; i < names.Length; i++)
                ordinals[i] = GetOrdinalOrException(names[i]);
            return ordinals;
        }

        public bool HasMember(string name)
        {
            return GetOrdinal(name) >= 0;
        }

        public TMember this[string name]
        {
            get { return _Members[GetOrdinalOrException(name)]; }
        }

        public override string ToString()
        {
            return _DataType.FullName;
        }

        public AspectMember GetMember(int ordinal)
        {
            return _Members[ordinal];
        }

        public AspectMember GetMember(string name)
        {
            return _Members[GetOrdinalOrException(name)];
        }

        public IEnumerable<AspectMember> GetMembers()
        {
            int count = _Members.Count;
            for (int i = 0; i < count; i++)
                yield return _Members[i];
        }

        public IEnumerator<TMember> GetEnumerator()
        {
            return _Members.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Members.GetEnumerator();
        }

        /// Gets custom attributes for the field.
        /// </summary>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public object[] GetCustomAttributes(bool inherit)
        {
            return _DataType.GetCustomAttributes(inherit);
        }

        /// <summary>
        /// Gets custom attributes for the field.
        /// </summary>
        /// <param name="attributeType">The type of attribute to get.</param>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>An array of attributes.</returns>
        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return _DataType.GetCustomAttributes(attributeType, inherit);
        }

        /// <summary>
        /// Checks if an attribute is defined.
        /// </summary>
        /// <param name="attributeType">The type of attribute to check.</param>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>True if defined.</returns>
        public bool IsDefined(Type attributeType, bool inherit)
        {
            return _DataType.IsDefined(attributeType, inherit);
        }

        /// <summary>
        /// Gets an attribute.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to get.</typeparam>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>The attribute or default.</returns>
        public TAttribute GetAttribute<TAttribute>(bool inherit)
        {
            object[] attributes = _DataType.GetCustomAttributes(typeof(TAttribute), inherit);

            return attributes != null && attributes.Length > 0 ? (TAttribute) attributes[0] : default(TAttribute);
        }
        /// <summary>
        /// Gets typed attributes.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to get.</typeparam>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>An array of typed attributes.</returns>
        public TAttribute[] GetAttributes<TAttribute>(bool inherit)
        {
            object[] attributes = _DataType.GetCustomAttributes(typeof(TAttribute), inherit);
            TAttribute[] newAttributes = new TAttribute[attributes.Length];

            for (int i = 0; i < attributes.Length; i++)
                newAttributes[i] = (TAttribute) attributes[i];
            
            return newAttributes;
        }
        /// <summary>
        /// Indicates that an attribute exists.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attributes.</typeparam>
        /// <param name="inherit">The </param>
        /// <returns></returns>
        public bool HasAttribute<TAttribute>(bool inherit)
        {
            return _DataType.IsDefined(typeof(TAttribute), inherit);
        }




        public AspectMember[] GetMemberArray()
        {
            return this._Members.ToArray();
        }


        public AspectMember[] GetMemberArray(params string[] names)
        {
            AspectMember[] children = new AspectMember[names.Length];
            for (int i = 0; i < names.Length; i++)
                children[i] = _Members[GetOrdinalOrException(names[i])];
            return children;
        }
    }
}
