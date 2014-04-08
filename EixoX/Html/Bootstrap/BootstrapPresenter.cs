using EixoX.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EixoX.Html
{
    public abstract class BootstrapPresenter<T>
        : UIPresenter<T, BootstrapPresenterControl>,
        UIPresenterRenderable
    {
        
        public BootstrapPresenter(int lcid) : base(lcid)
        {

        }

        public abstract string GetVersionName();
        public abstract string GetControlPrefix();

        
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
