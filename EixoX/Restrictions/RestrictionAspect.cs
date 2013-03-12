using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Restrictions
{
    public class RestrictionAspect
        : AbstractAspect<RestrictionAspectMember>
    {
        public RestrictionAspect(Type dataType) : base(dataType) { }

        protected override bool CreateAspectFor(ClassAcessor acessor, out RestrictionAspectMember member)
        {
            member = new RestrictionAspectMember(acessor);
            return member.HasRestrictions;
        }

        public bool Validate(object entity)
        {
            foreach (RestrictionAspectMember ram in this)
                if (!ram.Validate(entity))
                    return false;
            return true;
        }

        public void AssertValid(object entity)
        {
            foreach (RestrictionAspectMember ram in this)
                ram.AssertValid(entity);
        }

        public void AssertValid(object entity, int lcid)
        {
            foreach (RestrictionAspectMember ram in this)
                ram.AssertValid(entity, lcid);
        }

        public string GetRestrictionMessage(object entity)
        {
            foreach (RestrictionAspectMember ram in this)
            {
                string msg = ram.GetRestrictionMessage(entity);
                if (msg != null)
                    return msg;
            }
            return null;
        }

        public string GetRestrictionMessage(object entity, int lcid)
        {
            foreach (RestrictionAspectMember ram in this)
            {
                string msg = ram.GetRestrictionMessage(entity, lcid);
                if (msg != null)
                    return msg;
            }
            return null;
        }
    }

    public class RestrictionAspect<T>
        : RestrictionAspect
    {
        private static RestrictionAspect<T> _Instance;
        private RestrictionAspect() : base(typeof(T)) { }
        public static RestrictionAspect<T> Instance
        {
            get
            {
                return _Instance ?? (_Instance = new RestrictionAspect<T>());
            }
        }
    }
}
