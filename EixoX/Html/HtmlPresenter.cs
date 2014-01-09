using EixoX.Html.Controls;
using EixoX.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EixoX.Html
{
    public class HtmlPresenter<T> : UI.UIPresenter<T, HtmlPresenterControl>
    {
        private static Dictionary<int, HtmlPresenter<T>> _Instances;
        private HtmlPresenter(int lcid) : base(lcid) { }

        public static HtmlPresenter<T> GetInstance(int lcid)
        {
            HtmlPresenter<T> instance = null;
            if (_Instances == null)
            {
                _Instances = new Dictionary<int, HtmlPresenter<T>>();
                instance = new HtmlPresenter<T>(lcid);
                _Instances.Add(lcid, instance);
            }
            else if (!_Instances.TryGetValue(lcid, out instance))
            {
                instance = new HtmlPresenter<T>(lcid);
                _Instances.Add(lcid, instance);
            }
            return instance;
        }

        public static HtmlPresenter<T> GetInstance(CultureInfo culture)
        {
            return GetInstance(culture.LCID);
        }

        public static HtmlPresenter<T> GetInstance(string culture)
        {
            return GetInstance(CultureInfo.GetCultureInfo(culture));
        }

        protected override HtmlPresenterControl CreateControl(SingleAnnotationAspectMember<UIControlAttribute> member, string label, string hint, int localeCultureId, Restrictions.RestrictionList restrictions, Interceptors.InterceptorList interceptors, Globalization.GlobalizationList globalization)
        {
            return new HtmlPresenterControl(
                member,
                label,
                hint,
                localeCultureId,
                restrictions,
                interceptors,
                globalization,
                (HtmlControl)HtmlControlFactory.Instance.CreateControlFor(member.Annotation));
        }

        public void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions)
        {
            foreach (HtmlPresenterControl item in this)
                item.Render(writer, entity, validateRestrictions);
        }

        public void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions, params string[] names)
        {
            foreach (HtmlPresenterControl item in GetMembers(names))
                item.Render(writer, entity, validateRestrictions);
        }

        public void RenderGroup(System.IO.TextWriter writer, object entity, string groupName, bool validateRestrictions)
        {
            foreach (HtmlPresenterControl item in GetGroupMembers(groupName))
                item.Render(writer, entity, validateRestrictions);
        }


    }
}
