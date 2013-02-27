using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public sealed class ClassSchema<T> : AbstractAspect<AspectMember>
    {
        private static ClassSchema<T> _instance;
        private ClassSchema() : base(typeof(T)) { }
        public static ClassSchema<T> Instance
        {
            get { return _instance ?? (_instance = new ClassSchema<T>()); }
        }

        protected override bool CreateAspectFor(ClassAcessor acessor, out AspectMember member)
        {
            member = new AspectMember(acessor);
            return true;
        }
    }
}
