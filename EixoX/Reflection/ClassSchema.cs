using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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


        public int Parse(NameValueCollection collection, object entity, IFormatProvider provider)
        {
            Interceptors.InterceptorAspect<T> interceptors = Interceptors.InterceptorAspect<T>.Instance;

            int foundMemberCounter = 0;
            foreach (AspectMember member in this)
            {
                if (member.CanWrite)
                {
                    string collectionValue = collection[member.Name];
                    if (collectionValue != null)
                    {
                        foundMemberCounter++;
                        int interceptorOrdinal = interceptors.GetOrdinal(member.Name);
                        if (interceptorOrdinal >= 0)
                            collectionValue = (string)interceptors[interceptorOrdinal].Interceptors.Intercept(collectionValue);

                        object value = null;
                        try
                        {
                            if (member.DataType.IsEnum)
                            {
                                value = Enum.Parse(member.DataType, collectionValue);
                            }
                            else if (member.DataType == PrimitiveTypes.TimeSpan)
                            {
                                value = TimeSpan.Parse(collectionValue);
                            }
                            else
                            {
                                value = Convert.ChangeType(collectionValue, member.DataType, provider);
                            }
                        }
                        catch
                        {
                            value = member.DataType.IsValueType ? Activator.CreateInstance(member.DataType) : null;
                        }
                        member.SetValue(entity, value);

                    }
                    else
                    {
                        if (member.DataType == PrimitiveTypes.Boolean)
                            member.SetValue(entity, false);
                    }
                }
            }
            
            return foundMemberCounter;
        }

        public int Parse(NameValueCollection collection, object entity)
        {
            return Parse(collection, entity, System.Globalization.CultureInfo.CurrentUICulture);
        }
    }
}
