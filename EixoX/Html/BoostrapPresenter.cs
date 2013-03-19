using EixoX.Html.Controls;
using EixoX.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EixoX.Html
{
    public class BoostrapPresenter<T> : UI.UIPresenter<T, BoostrapPresenterControl>
    {
        private static Dictionary<int, BoostrapPresenter<T>> _Instances;
        private BoostrapPresenter(int lcid) : base(lcid) { }

        public static BoostrapPresenter<T> GetInstance(int lcid)
        {
            BoostrapPresenter<T> instance = null;
            if (_Instances == null)
            {
                _Instances = new Dictionary<int, BoostrapPresenter<T>>();
                instance = new BoostrapPresenter<T>(lcid);
                _Instances.Add(lcid, instance);
            }
            else if (!_Instances.TryGetValue(lcid, out instance))
            {
                instance = new BoostrapPresenter<T>(lcid);
                _Instances.Add(lcid, instance);
            }
            return instance;
        }

        public static BoostrapPresenter<T> GetInstance(CultureInfo culture)
        {
            return GetInstance(culture.LCID);
        }

        public static BoostrapPresenter<T> GetInstance(string culture)
        {
            return GetInstance(CultureInfo.GetCultureInfo(culture));
        }

        protected override BoostrapPresenterControl CreateControl(SingleAnnotationAspectMember<UIControlAttribute> member, string label, string hint, int localeCultureId, Restrictions.RestrictionList restrictions, Interceptors.InterceptorList interceptors, Globalization.GlobalizationList globalization)
        {
            return new BoostrapPresenterControl(
                member,
                label,
                hint,
                localeCultureId,
                restrictions,
                interceptors,
                globalization,
                (BoostrapControl)BoostrapControlFactory.Instance.CreateControlFor(member.Annotation));
        }

        public void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions)
        {
            foreach (BoostrapPresenterControl item in this)
                item.Render(writer, entity, validateRestrictions);
        }

        public void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions, params string[] names)
        {
            foreach (BoostrapPresenterControl item in GetMembers(names))
                item.Render(writer, entity, validateRestrictions);
        }

        public void RenderGroup(System.IO.TextWriter writer, object entity, string groupName, bool validateRestrictions)
        {
            foreach (BoostrapPresenterControl item in GetGroupMembers(groupName))
                item.Render(writer, entity, validateRestrictions);
        }

        public void RenderGroupFieldset(System.IO.TextWriter writer, object entity, string groupName, bool validateRestrictions)
        {
            writer.WriteLine("<fieldset>");
            writer.Write("<legend>");
            writer.Write(HtmlHelper.HtmlFormat(groupName));
            writer.WriteLine("</legend>");
            RenderGroup(writer, entity, groupName, validateRestrictions);
            writer.WriteLine("</fieldset>");
        }


    }
}
