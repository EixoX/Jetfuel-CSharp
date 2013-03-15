using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UIControlGroup : Attribute
    {
        private readonly string _name;

        public UIControlGroup(string name)
        {
            this._name = name;
        }

        public string Name
        {
            get { return this._name; }
        }
    }
}
