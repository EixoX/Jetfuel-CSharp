﻿using System;
using System.Collections.Generic;
using System.Text;
using EixoX.UI;
using EixoX.Interceptors;
using EixoX.Restrictions;
using EixoX.Globalization;
using EixoX.Html.Controls;

namespace EixoX.Html
{
    public class BootstrapPresenterControl : UI.UIPresenterControl
    {
        private readonly BootstrapControl _Control;

        public BootstrapPresenterControl(
            SingleAnnotationAspectMember<UIControlAttribute> member,
            string label,
            string hint,
            int lcid,
            RestrictionList restrictions,
            InterceptorList interceptors,
            GlobalizationList globalization,
            BootstrapControl control)
            : base(member, label, hint, lcid, restrictions, interceptors, globalization)
        {
            this._Control = control;
        }


        public void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions)
        {
            this._Control.Render(writer, GetState(entity, validateRestrictions));
        }

        public void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions, params HtmlAttribute[] attributes)
        {
            this._Control.Render(writer, GetState(entity, validateRestrictions), attributes);
        }

    }
}
