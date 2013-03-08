using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Reflection;

namespace EixoX.Interceptors
{
    public class InterceptorAspect
        : AbstractAspect<InterceptorAspectMember>
    {
        public InterceptorAspect(Type dataType) : base(dataType) { }

        protected override bool CreateAspectFor(ClassAcessor acessor, out InterceptorAspectMember member)
        {
            member = new InterceptorAspectMember(acessor);
            return member.HasInterceptors;
        }

        public void Intercept(object entity)
        {
            foreach (InterceptorAspectMember iam in this)
                iam.Intercept(entity);
        }
    }

    public class InterceptorAspect<T>
        :InterceptorAspect
    {
        private static InterceptorAspect<T> _Instance;
        private InterceptorAspect() : base(typeof(T)) { }
        public static InterceptorAspect<T> Instance
        {
            get
            {
                return _Instance ?? (_Instance = new InterceptorAspect<T>());
            }
        }
    }
}
