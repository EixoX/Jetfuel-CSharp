using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public class CompositeAnnotationMember<TMemberAspect>
        : AspectMember
    {
        private readonly LinkedList<TMemberAspect> _Annotations;
        public CompositeAnnotationMember(ClassAcessor acessor, IEnumerable<TMemberAspect> values)
            : base(acessor)
        {
            this._Annotations = new LinkedList<TMemberAspect>(values);
        }

        public LinkedList<TMemberAspect> Annotations { get { return this._Annotations; } }
    }
}
