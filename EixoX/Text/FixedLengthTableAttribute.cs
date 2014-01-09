using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false)]
    public class FixedLengthTableAttribute : Attribute
    {
        public string CultureInfo { get; set; }
    }
}
