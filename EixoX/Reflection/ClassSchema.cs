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
            foreach (string key in collection.Keys)
            {
                int ordinal = GetOrdinal(key);
                if (ordinal >= 0)
                {
                    AspectMember member = base[ordinal];

                    string collectionValue = collection[key];

                    int interceptorOrdinal = interceptors.GetOrdinal(key);
                    if (interceptorOrdinal >= 0)
                        collectionValue = (string)interceptors[interceptorOrdinal].Interceptors.Intercept(collectionValue);

                    object value = null;
                    if (!string.IsNullOrEmpty(collectionValue))
                    {
                        value = member.DataType.IsEnum ?
                                Enum.Parse(member.DataType, collection[key]) :
                                Convert.ChangeType(collectionValue, member.DataType, provider);
                    }

                    member.SetValue(entity, value);
                    foundMemberCounter++;
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
