using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public class SingleAnnotationAspectMember<TMemberAspect>
        : AspectMember
    {
        private readonly TMemberAspect _Annotation;

        public SingleAnnotationAspectMember(ClassAcessor acessor, TMemberAspect value)
            : base(acessor)
        {
            this._Annotation = value;
        }

        public TMemberAspect Annotation
        {
            get { return this._Annotation; }
        }

    }
}
