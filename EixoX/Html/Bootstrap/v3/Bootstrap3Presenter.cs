using EixoX.Html.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class Bootstrap3Presenter<T> : BootstrapPresenter<T>
    {
        public Bootstrap3Presenter(int lcid) : base(lcid)
        {

        }

        public override string GetVersionName()
        {
            return "3";
        }

        public override string GetControlPrefix()
        {
            return "3";
        }

        protected override BootstrapPresenterControl CreateControl(SingleAnnotationAspectMember<UI.UIControlAttribute> member, string label, string hint, int localeCultureId, Restrictions.RestrictionList restrictions, Interceptors.InterceptorList interceptors, Globalization.GlobalizationList globalization)
        {
            return new BootstrapPresenterControl(
                member,
                label,
                hint,
                localeCultureId,
                restrictions,
                interceptors,
                globalization,
                (Bootstrap3Control)BootstrapControlFactory.GetInstance(this.GetControlPrefix()).CreateControlFor(member.Annotation));
        }
    }
}
