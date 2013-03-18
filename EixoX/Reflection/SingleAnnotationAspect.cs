using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public class SingleAnnotationAspect<TMemberAspect>
        :AbstractAspect<SingleAnnotationAspectMember<TMemberAspect>>
    {
        public SingleAnnotationAspect(Type dataType) : base(dataType) { }
        protected override bool CreateAspectFor(ClassAcessor acessor, out SingleAnnotationAspectMember<TMemberAspect> member)
        {
            TMemberAspect tma = acessor.GetAttribute<TMemberAspect>(true);
            if (tma == null)
            {
                member = null;
                return false;
            }
            else
            {
                member = new SingleAnnotationAspectMember<TMemberAspect>(acessor, tma);
                return true;
            }
        }
    }

    public class SingleDecorationAspect<TEntity, TMemberAspect>
        : SingleAnnotationAspect<TMemberAspect>
    {
        private static SingleDecorationAspect<TEntity, TMemberAspect> _Instance;
        private SingleDecorationAspect() : base(typeof(TEntity)) { }
        public static SingleDecorationAspect<TEntity, TMemberAspect> Instance
        {
            get { return _Instance ?? (_Instance = new SingleDecorationAspect<TEntity, TMemberAspect>()); }
        }
    }
}
