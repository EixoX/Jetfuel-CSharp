using EixoX.Html.Controls;
using EixoX.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace EixoX.Html
{
    public class BootstrapPresenter<T> : UI.UIPresenter<T, BootstrapPresenterControl>
    {
        private static Dictionary<int, BootstrapPresenter<T>> _Instances;
        private BootstrapPresenter(int lcid) : base(lcid) { }

        public static BootstrapPresenter<T> GetInstance(int lcid)
        {
            BootstrapPresenter<T> instance = null;
            if (_Instances == null)
            {
                _Instances = new Dictionary<int, BootstrapPresenter<T>>();
                instance = new BootstrapPresenter<T>(lcid);
                _Instances.Add(lcid, instance);
            }
            else if (!_Instances.TryGetValue(lcid, out instance))
            {
                instance = new BootstrapPresenter<T>(lcid);
                _Instances.Add(lcid, instance);
            }
            return instance;
        }

        public static BootstrapPresenter<T> GetInstance(CultureInfo culture)
        {
            return GetInstance(culture.LCID);
        }

        public static BootstrapPresenter<T> GetInstance(string culture)
        {
            return GetInstance(CultureInfo.GetCultureInfo(culture));
        }

        protected override BootstrapPresenterControl CreateControl(SingleAnnotationAspectMember<UIControlAttribute> member, string label, string hint, int localeCultureId, Restrictions.RestrictionList restrictions, Interceptors.InterceptorList interceptors, Globalization.GlobalizationList globalization)
        {
            return new BootstrapPresenterControl(
                member,
                label,
                hint,
                localeCultureId,
                restrictions,
                interceptors,
                globalization,
                (BootstrapControl)BootstrapControlFactory.Instance.CreateControlFor(member.Annotation));
        }

        public void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions, params HtmlAttribute[] attributes)
        {
            foreach (BootstrapPresenterControl control in this)
                control.Render(writer, entity, validateRestrictions, attributes);
        }

        public void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions)
        {
            foreach (BootstrapPresenterControl item in this)
                item.Render(writer, entity, validateRestrictions);
        }

        public void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions, params string[] names)
        {
            foreach (BootstrapPresenterControl item in GetMembers(names))
                item.Render(writer, entity, validateRestrictions);
        }

        public void RenderGroup(System.IO.TextWriter writer, object entity, string groupName, bool validateRestrictions, params HtmlAttribute[] attributes)
        {
            foreach (BootstrapPresenterControl item in GetGroupMembers(groupName))
                item.Render(writer, entity, validateRestrictions, attributes);
        }

        public void RenderGroupFieldset(TextWriter writer, object entity, string groupName, bool validateRestrictions, params HtmlAttribute[] attributes)
        {
            writer.WriteLine("<fieldset>");
            writer.Write("<legend>");
            writer.Write(HtmlHelper.HtmlFormat(groupName));
            writer.WriteLine("</legend>");
            RenderGroup(writer, entity, groupName, validateRestrictions, attributes);
            writer.WriteLine("</fieldset>");
        }

    }
}
