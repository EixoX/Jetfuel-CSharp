using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public abstract class Bootstrap3Control : BootstrapControl
    {
        HtmlNode _Label;
        HtmlNode _Input;

        private object GetCssClassValue(params HtmlAttribute[] attributes)
        {
            foreach (HtmlAttribute attr in attributes)
            {
                if (attr.Name.Equals("class", StringComparison.OrdinalIgnoreCase))
                    return attr.Value;
            }

            return string.Empty;
        }

        public override void Render(System.IO.TextWriter writer, UI.UIControlState state, params HtmlAttribute[] attributes)
        {
            writer.Write("<div class=\"");
            if (state.Validated)
            {
                if (string.IsNullOrEmpty(state.ErrorMessage))
                    writer.Write("form-group has-success has-feedback");
                else
                    writer.Write("form-group has-error has-feedback");
            }
            else
                writer.Write("form-group");

            writer.WriteLine("\">");

            RenderLabel(writer, state);
            RenderInput(writer, state, attributes);

            if (state.Validated)
            {
                if (string.IsNullOrEmpty(state.ErrorMessage))
                    writer.Write("<span class=\"glyphicon glyphicon-ok form-control-feedback\"></span>");
                else
                    writer.Write("<span class=\"glyphicon glyphicon-remove form-control-feedback\"></span>");
            }

            writer.WriteLine("</div>");
        }

        protected override void RenderInput(System.IO.TextWriter writer, UI.UIControlState state, params HtmlAttribute[] attributes)
        {
            bool addedClass = false;
            _Input = CreateInput(state);

            foreach (HtmlAttribute attr in attributes)
            {
                if (attr.Name.Equals("class", StringComparison.OrdinalIgnoreCase))
                {
                    HtmlAttribute cssClass = new HtmlAttribute("class", attr.Value.ToString() + " form-control");
                    _Input.Attributes[attr.Name] = cssClass.Value;
                    addedClass = true;
                }
                else
                    _Input.Attributes[attr.Name] = attr.Value;
            }

            if (!addedClass)
                _Input.Attributes["class"] = "form-control";

            _Input.Write(writer);
        }

        protected override void RenderLabel(System.IO.TextWriter writer, UI.UIControlState state)
        {
            if (_Label == null)
                _Label = new HtmlSimple(
                    "label",
                    state.Label,
                    new HtmlAttribute("for", state.Name),
                    new HtmlAttribute("class", "control-label")
                    );

            _Label.Write(writer);
        }
    }
}
