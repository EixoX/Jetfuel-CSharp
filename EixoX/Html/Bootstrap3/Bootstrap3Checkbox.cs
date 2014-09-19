using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class Bootstrap3Checkbox : Bootstrap3Control
    {
        public override void Render(System.IO.TextWriter writer, UI.UIControlState state, params HtmlAttribute[] attributes)
        {
            writer.Write("<div class=\"");
            writer.Write(string.IsNullOrEmpty(state.ErrorMessage) ? "form-group" : "form-group has-error");
            writer.WriteLine("\">");

            writer.WriteLine("<div class=\"checkbox\">");
            writer.Write("<label class=\"\">");
            RenderInput(writer, state, attributes);
            writer.Write(HtmlHelper.HtmlFormat(state.Label));
            writer.WriteLine("</label>");
            RenderHintOrError(writer, state);
            writer.WriteLine("</div>");


            writer.WriteLine("</div>");
        }

        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            HtmlStandalone checkbox = new HtmlStandalone("input",
                    new HtmlAttribute("type", "checkbox"),
                    new HtmlAttribute("id", state.Name),
                    new HtmlAttribute("name", state.Name),
                    new HtmlAttribute("value", "True"));

            if (Convert.ToBoolean(state.Value))
            {
                checkbox.Attributes.AddLast("checked", "checked");
            }

            return checkbox;
        }
    }
}
