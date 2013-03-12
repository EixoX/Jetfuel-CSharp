using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Restrictions
{
    public class RestrictionAspectMember
        : AspectMember
    {
        private readonly RestrictionList _restrictions;

        public RestrictionAspectMember(ClassAcessor acessor)
            : base(acessor)
        {
            this._restrictions = new RestrictionList(this);
        }

        public RestrictionList Restrictions
        {
            get { return this._restrictions; }
        }

        public bool HasRestrictions
        {
            get { return this._restrictions.Count > 0; }
        }

        public bool Validate(object entity)
        {
            return _restrictions.Validate(GetValue(entity));
        }

        public void AssertValid(object entity)
        {
            _restrictions.AssertValid(GetValue(entity));
        }

        public void AssertValid(object entity, int lcid)
        {
            _restrictions.AssertValid(GetValue(entity), lcid);
        }

        public string GetRestrictionMessage(object entity)
        {
            return _restrictions.GetRestrictionMessage(GetValue(entity));
        }

        public string GetRestrictionMessage(object entity, int lcid)
        {
            return _restrictions.GetRestrictionMessage(GetValue(entity), lcid);
        }
    }
}
