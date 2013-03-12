using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Interceptors
{
    public class InterceptorAspectMember
        : AspectMember
    {
        private readonly InterceptorList _interceptors;
        public InterceptorAspectMember(ClassAcessor acessor)
            : base(acessor)
        {
            this._interceptors = new InterceptorList(this);
        }

        public InterceptorList Interceptors { get { return this._interceptors; } }
        public bool HasInterceptors { get { return _interceptors.Count > 0; } }
        public void Intercept(object entity)
        {
            SetValue(entity, _interceptors.Intercept(GetValue(entity)));
        }
        public override object GetValue(object entity)
        {
            return _interceptors.Intercept(base.GetValue(entity));
        }

        public override void SetValue(object entity, object value)
        {
            base.SetValue(entity, _interceptors.Intercept(value));
        }
    }
}
