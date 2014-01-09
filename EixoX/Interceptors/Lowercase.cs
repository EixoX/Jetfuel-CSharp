using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Interceptors
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class Lowercase : Attribute, Interceptor
    {
        public object Intercept(object input)
        {
            return (input == null || !(input is string)) ? input :
                ((string)input).ToLower();
        }
    }
}
