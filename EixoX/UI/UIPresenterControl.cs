using EixoX.Globalization;
using EixoX.Interceptors;
using EixoX.Restrictions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    /// <summary>
    /// Represents an abstract control presenter.
    /// </summary>
    public class UIPresenterControl
    {
        private List<UIControlGroup> _Groups;
        private readonly SingleAnnotationAspectMember<UIControlAttribute> _Member;
        private readonly RestrictionList _Restrictions;
        private readonly InterceptorList _Interceptors;
        private readonly GlobalizationList _Globalization;
        private string _Label;
        private string _Hint;
        private readonly int _LCID;
        private readonly System.Globalization.CultureInfo _FormatProvider;

        public UIPresenterControl(
            SingleAnnotationAspectMember<UIControlAttribute> member,
            string label,
            string hint,
            int lcid,
            RestrictionList restrictions,
            InterceptorList interceptors,
            GlobalizationList globalization)
        {
            this._Member = member;
            this._Groups = new List<UIControlGroup>(member.GetAttributes<UIControlGroup>(true));
            this._Label = string.IsNullOrEmpty(label) ? member.Name : label;
            this._Hint = hint;
            this._LCID = lcid;
            this._FormatProvider = System.Globalization.CultureInfo.GetCultureInfo(lcid);
            this._Restrictions = restrictions;
            this._Interceptors = interceptors;
            this._Globalization = globalization;

        }

        public string Name { get { return _Member.Name; } }

        public GlobalizationList Globalization { get { return this._Globalization; } }

        public RestrictionList Restrictions { get { return this._Restrictions; } }

        public InterceptorList Interceptors { get { return this._Interceptors; } }

        public string Label
        {
            get { return this._Label; }
            set { this._Label = value; }
        }

        public string Hint
        {
            get { return this._Hint; }
            set { this._Hint = value; }
        }

        public string GetGlobalizationTerm(string termName)
        {
            if (_Globalization == null)
                return null;
            else
                return _Globalization.GetTerm(termName, _LCID);
        }

        public bool InGroup(string name)
        {
            int count = _Groups.Count;
            if (count > 0 && !string.IsNullOrEmpty(name))
            {
                for (int i = 0; i < count; i++)
                    if (name.Equals(_Groups[i].Name, StringComparison.OrdinalIgnoreCase))
                        return true;
            }
            return false;
        }

        public UIControlState GetState(object entity, bool validateRestrictions)
        {
            object value = _Member.DataType.IsEnum ? (int)_Member.GetValue(entity) : _Member.GetValue(entity);

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
                Options = _Member.Annotation.GetChoices(),
                Value = value,
                Validated = validateRestrictions,
                FormatProvider = _FormatProvider
            };
        }

    }
}
