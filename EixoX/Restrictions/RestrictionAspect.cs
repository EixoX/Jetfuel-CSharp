using EixoX.Components;
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

        public virtual bool Validate(object entity)
        {
            foreach (RestrictionAspectMember ram in this)
                if (!ram.Validate(entity))
                    return false;
            return true;
        }

        public virtual bool Validate(object entity, params string[] names)
        {
            for (int i = 0; i < names.Length; i++)
                if (!base[names[i]].Validate(entity))
                    return false;

            return true;
        }

        public virtual bool Validate(object entity, List<string> exceptions)
        {
            foreach (RestrictionAspectMember ram in this)
                if (!exceptions.Contains(ram.Name))
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

        public RestrictionList GetRestrictionList(string name)
        {
            int ordinal = base.GetOrdinal(name);
            return ordinal < 0 ? null : base[ordinal].Restrictions;
        }
    }

    public class RestrictionAspect<T>
        : RestrictionAspect
    {
        private static RestrictionAspect<T> _Instance;
        public RestrictionAspect() : base(typeof(T)) { }
        public static RestrictionAspect<T> Instance
        {
            get
            {
                return _Instance ?? (_Instance = new RestrictionAspect<T>());
            }
        }
    }

    public class WizardRestrictionAspect<T> : RestrictionAspect
    {
        private static WizardRestrictionAspect<T> _Instance;
        public WizardRestrictionAspect() : base(typeof(T)) { }
        public static WizardRestrictionAspect<T> Instance
        {
            get
            {
                return _Instance ?? (_Instance = new WizardRestrictionAspect<T>());
            }
        }

        public override bool Validate(object entity)
        {
            Wizard wizard = (Wizard)entity;
            bool isEverythingOk = true;

            foreach (RestrictionAspectMember ram in this)
                if (!ram.Validate(entity))
                {
                    WizardStep stepAttr = ram.GetAttribute<WizardStep>(true);
                    if (stepAttr != null)
                        wizard.Invalidate(stepAttr, ram.Name, GetRestrictionMessage(entity));

                    isEverythingOk = false;
                }

            return isEverythingOk;
        }
    }
}
