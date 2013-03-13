using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Interceptors;
using EixoX.Restrictions;
using EixoX.Globalization;
namespace EixoX.Html
{
    public class HtmlInputAspectMember : AspectMember
    {
        private readonly string _Fieldset;
        private readonly HtmlInputType _InputType;
        private readonly HtmlInputOptionSource _InputSource;
        private readonly InterceptorAspectMember _Interceptor;
        private readonly RestrictionAspectMember _Restriction;
        private readonly GlobalizationAspectMember _Globalization;

        public HtmlInputAspectMember(
            ClassAcessor acessor,
            string fieldset,
            HtmlInputType inputType,
            HtmlInputOptionSource inputSource,
            InterceptorAspectMember interceptor,
            RestrictionAspectMember restriction,
            GlobalizationAspectMember globalization)
            : base(acessor)
        {
            this._Fieldset = fieldset;
            this._InputType = inputType;
            this._Interceptor = interceptor;
            this._InputSource = inputSource;
            this._Restriction = restriction;
            this._Globalization = globalization;
        }

        public static HtmlInputType InferInputType(Type dataType)
        {
            if (dataType == typeof(DateTime))
                return HtmlInputType.Date;
            else if (dataType == typeof(bool))
                return HtmlInputType.Checkbox;
            

            return HtmlInputType.Text;
        }

        public string Fieldset { get { return this._Fieldset; } }
        public HtmlInputType InputType { get { return this._InputType; } }

        public string GetLabel(int lcid)
        {
            string label = _Globalization == null ? null : _Globalization.GetItem("label", lcid);
            return string.IsNullOrEmpty(label) ? Name : label;
        }

        public string GetPlaceholder(int lcid)
        {
            return _Globalization == null ? "" : _Globalization.GetItem("placeholder", lcid);
        }

        public string GetHint(int lcid)
        {
            return _Globalization == null ? "" : _Globalization.GetItem("hint", lcid);
        }

        public override object GetValue(object entity)
        {
            return _Interceptor == null ? base.GetValue(entity) : _Interceptor.GetValue(entity);
        }

        public override void SetValue(object entity, object value)
        {
            if (_Interceptor != null)
                _Interceptor.SetValue(entity, value);
            else
                base.SetValue(entity, value);
        }

        public HtmlInputTerm CreateTerm(object entity, int lcid, bool validateRestrictions)
        {
            return new HtmlInputTerm()
            {
                InputType = this._InputType,
                Name = this.Name,
                Label = GetLabel(lcid),
                Placeholder = GetPlaceholder(lcid),
                Hint = GetHint(lcid),
                Value = GetValue(entity),
                ErrorMessage = validateRestrictions && _Restriction != null ?
                    _Restriction.GetRestrictionMessage(entity, lcid) : null,
                InputOptions = _InputSource
            };
        }
    }
}
