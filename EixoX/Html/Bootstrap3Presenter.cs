using EixoX.Html.Controls;
using EixoX.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace EixoX.Html
{
    public class Bootstrap3Presenter<T> : UI.UIPresenter<T, Bootstrap3PresenterControl>
    {
        private static Dictionary<int, Bootstrap3Presenter<T>> _Instances;
        private Bootstrap3Presenter(int lcid) : base(lcid) { }

        public static Bootstrap3Presenter<T> GetInstance(int lcid)
        {
            Bootstrap3Presenter<T> instance = null;
            if (_Instances == null)
            {
                _Instances = new Dictionary<int, Bootstrap3Presenter<T>>();
                instance = new Bootstrap3Presenter<T>(lcid);
                _Instances.Add(lcid, instance);
            }
            else if (!_Instances.TryGetValue(lcid, out instance))
            {
                instance = new Bootstrap3Presenter<T>(lcid);
                _Instances.Add(lcid, instance);
            }
            return instance;
        }

        public static Bootstrap3Presenter<T> GetInstance(CultureInfo culture)
        {
            return GetInstance(culture.LCID);
        }

        public static Bootstrap3Presenter<T> GetInstance(string culture)
        {
            return GetInstance(CultureInfo.GetCultureInfo(culture));
        }

        protected override Bootstrap3PresenterControl CreateControl(SingleAnnotationAspectMember<UIControlAttribute> member, string label, string hint, int localeCultureId, Restrictions.RestrictionList restrictions, Interceptors.InterceptorList interceptors, Globalization.GlobalizationList globalization)
        {
            return new Bootstrap3PresenterControl(
                member,
                label,
                hint,
                localeCultureId,
                restrictions,
                interceptors,
                globalization,
                (Bootstrap3Control)Bootstrap3ControlFactory.Instance.CreateControlFor(member.Annotation));
        }

        public void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions, params HtmlAttribute[] attributes)
        {
            foreach (Bootstrap3PresenterControl control in this)
                control.Render(writer, entity, validateRestrictions, attributes);
        }

        public void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions)
        {
            foreach (Bootstrap3PresenterControl item in this)
                item.Render(writer, entity, validateRestrictions);
        }

        public void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions, params string[] names)
        {
            foreach (Bootstrap3PresenterControl item in GetMembers(names))
                item.Render(writer, entity, validateRestrictions);
        }

        public void RenderGroup(System.IO.TextWriter writer, object entity, string groupName, bool validateRestrictions, params HtmlAttribute[] attributes)
        {
            foreach (Bootstrap3PresenterControl item in GetGroupMembers(groupName))
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
