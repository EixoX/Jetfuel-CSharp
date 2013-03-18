using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Data
{
    public class GeneratorAspect
        : AbstractAspect<GeneratorAspectMember>
    {
        public GeneratorAspect(Type dataType) : base(dataType) { }

        protected override bool CreateAspectFor(ClassAcessor acessor, out GeneratorAspectMember member)
        {
            member = new GeneratorAspectMember(acessor);
            return member.Generator != null;
        }

        public void ApplyGenerators(object entity, DataScope scope)
        {
            foreach (GeneratorAspectMember member in this)
                member.ApplyTo(entity, scope);
        }
    }

    public class GeneratorAspect<T>
        : GeneratorAspect
    {
        private static GeneratorAspect<T> _Instance;
        private GeneratorAspect() : base(typeof(T)) { }
        public static GeneratorAspect<T> Instance
        {
            get { return _Instance ?? (_Instance = new GeneratorAspect<T>()); }
        }
    }
}
