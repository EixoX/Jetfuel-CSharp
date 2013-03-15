using EixoX.Globalization;
using EixoX.Interceptors;
using EixoX.Restrictions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIPresenterControl
    {

        private readonly AspectMember _Member;
        private readonly RestrictionList _Restrictions;
        private readonly InterceptorList _Interceptors;
        private readonly GlobalizationList _Globalization;
        private readonly UIControl _Control;
        private readonly string _Label;
        private readonly string _Hint;
        private readonly int _LCID;

        public UIPresenterControl(
            AspectMember member,
            string label,
            string hint,
            int lcid,
            RestrictionList restrictions,
            InterceptorList interceptors,
            GlobalizationList globalization,
            UIControl control)
        {
            this._Member = member;
            this._Label = string.IsNullOrEmpty(label) ? member.Name : label;
            this._Hint = hint;
            this._Restrictions = restrictions;
            this._Interceptors = interceptors;
            this._Globalization = globalization;
            this._Control = control;
            this._LCID = lcid;
        }

        public string Name { get { return _Member.Name; } }
        public GlobalizationList Globalization { get { return this._Globalization; } }

        public string GetGlobalizationTerm(string termName)
        {
            if (_Globalization == null)
                return null;
            else
                return _Globalization.GetTerm(termName, _LCID);
        }

        public UIControlState GetState(object entity, bool validateRestrictions)
        {
            return GetState(entity, validateRestrictions, null);
        }

        public UIControlState GetState(object entity, bool validateRestrictions, object options)
        {
            object value = _Member.GetValue(entity);

            if (_Interceptors != null && _Interceptors.Count > 0)
                value = _Interceptors.Intercept(value);

            string errorMessage = validateRestrictions && _Restrictions != null && _Restrictions.Count > 0 ?
                _Restrictions.GetRestrictionMessage(value, _LCID) : null;

            return new UIControlState()
            {
                Control = this,
                ErrorMessage = errorMessage,
                Name = _Member.Name,
                Label = _Label,
                Hint = _Hint,
                Options = options,
                Value = value
            };
        }

        public void Render(object entity, bool validateRestrictions, object options, object output)
        {
            this._Control.Render(GetState(entity, validateRestrictions, options), output);
        }
    }
}
