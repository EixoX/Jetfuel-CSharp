using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NullableAttribute : Attribute
    {
    }
}
