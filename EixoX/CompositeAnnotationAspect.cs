using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public class CompositeAnnotationAspect<TMemberAspect>
        : AbstractAspect<CompositeAnnotationMember<TMemberAspect>>
    {
        public CompositeAnnotationAspect(Type dataType) : base(dataType) { }

        protected override bool CreateAspectFor(ClassAcessor acessor, out CompositeAnnotationMember<TMemberAspect> member)
        {
            member = new CompositeAnnotationMember<TMemberAspect>(acessor, acessor.GetAttributes<TMemberAspect>(true));
            return member.Annotations.Count > 0;
        }
    }

    public class CompositeDecorationAspect<TEntity, TMemberAspect>
        : CompositeAnnotationAspect<TMemberAspect>
    {
        private static CompositeDecorationAspect<TEntity, TMemberAspect> _Instance;
        private CompositeDecorationAspect() : base(typeof(TEntity)) { }
        public CompositeDecorationAspect<TEntity, TMemberAspect> Instance
        {
            get { return _Instance ?? (_Instance = new CompositeDecorationAspect<TEntity, TMemberAspect>()); }
        }
    }
}
