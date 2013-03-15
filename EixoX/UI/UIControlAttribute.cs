using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public abstract class UIControlAttribute : Attribute
    {
        public string DefaultLabel { get; set; }
        public string DefaultHint { get; set; }
    }
}
