using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NewGuidGenerator : Attribute, Generator
    {
        private readonly DataScope _scope;
        public NewGuidGenerator(DataScope scope) { this._scope = scope; }

        public object Generate()
        {
            return Guid.NewGuid();
        }

        public DataScope GeneratorScope
        {
            get { return this._scope; }
        }
    }
}
