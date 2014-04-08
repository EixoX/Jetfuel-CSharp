using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class Bootstrap2Presenter<T> : BootstrapPresenter<T>
    {
        public Bootstrap2Presenter(int lcid) : base(lcid)
        {

        }

        public override string GetVersionName()
        {
            return "2.3.2";
        }

        public override string GetControlPrefix()
        {
            return "2";
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
                (BootstrapControl)BootstrapControlFactory.GetInstance(this.GetControlPrefix()).CreateControlFor(member.Annotation));
        }
    }
}
