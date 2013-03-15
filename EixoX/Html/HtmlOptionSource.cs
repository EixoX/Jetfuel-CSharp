using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class HtmlOptionSource : Attribute
    {
        public abstract IEnumerable<HtmlOption> GetOptions();
    }
}
