using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public abstract class BootstrapControl : UI.UIControl
    {
        HtmlNode _Label;
        HtmlNode _Hint;
        HtmlNode _Input;

        protected abstract HtmlNode CreateInput(UI.UIControlState state);

        protected virtual void RenderLabel(System.IO.TextWriter writer, UI.UIControlState state)
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

        protected virtual void RenderHintOrError(System.IO.TextWriter writer, UI.UIControlState state)
        {

            if (!string.IsNullOrEmpty(state.ErrorMessage))
            {
                _Hint = new HtmlSimple("span", state.ErrorMessage, new HtmlAttribute("class", "help-inline"));
            }
            else
            {
                string hintText = string.IsNullOrEmpty(state.Hint) ? " " : state.Hint;
                _Hint = new HtmlSimple("span", hintText, new HtmlAttribute("class", "help-inline"));
            }

            _Hint.Write(writer);

        }

        protected virtual void RenderInput(System.IO.TextWriter writer, UI.UIControlState state, params HtmlAttribute[] attributes)
        {
            _Input = CreateInput(state);

            for (int i = 0; i < attributes.Length; i++)
                _Input.Attributes[attributes[i].Name] = attributes[i].Value;

            _Input.Write(writer);
        }

        public virtual void Render(System.IO.TextWriter writer, UI.UIControlState state, params HtmlAttribute[] attributes)
        {
            writer.Write("<div class=\"");
            if (state.Validated)
            {
                if (string.IsNullOrEmpty(state.ErrorMessage))
                {
                    writer.Write("control-group success");
                }
                else
                {
                    writer.Write("control-group error");
                }
            }
            else
            {
                writer.Write("control-group");
            }

            writer.WriteLine("\">");
            
            RenderLabel(writer, state);

            writer.WriteLine("<div class=\"controls\">");
            
            RenderInput(writer, state, attributes);
            RenderHintOrError(writer, state);
            
            writer.WriteLine("</div>");
            writer.WriteLine("</div>");
        }
    }
}
