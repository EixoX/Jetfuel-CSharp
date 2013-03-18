using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class GetDateGenerator : Attribute, Generator
    {
        private readonly DataScope _scope;
        public GetDateGenerator(DataScope scope) { this._scope = scope; }

        public object Generate()
        {
            return DateTime.Now;
        }

        public DataScope GeneratorScope
        {
            get { return _scope; }
        }
    }
}
