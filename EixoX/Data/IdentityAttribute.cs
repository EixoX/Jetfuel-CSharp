using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class IdentityAttribute: Attribute
    {
    }
}
