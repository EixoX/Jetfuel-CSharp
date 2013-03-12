using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Interceptors
{
    public class InterceptorList: LinkedList<Interceptor>, Interceptor
    {
        public InterceptorList() { }
        public InterceptorList(IEnumerable<Interceptor> interceptors) : base(interceptors) { }
        public InterceptorList(AspectMember aspectMember) : base(aspectMember.GetAttributes<Interceptor>(true)) { }

        public object Intercept(object input)
        {
            foreach (Interceptor i in this)
                input = i.Intercept(input);
            return input;
        }
    }
}
