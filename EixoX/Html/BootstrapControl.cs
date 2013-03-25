﻿using System;
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

        protected void RenderLabel(System.IO.TextWriter writer, UI.UIControlState state)
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

        protected void RenderHintOrError(System.IO.TextWriter writer, UI.UIControlState state)
        {
            if (!string.IsNullOrEmpty(state.ErrorMessage))
                new HtmlSimple("span", state.ErrorMessage, new HtmlAttribute("class", "help-inline")).Write(writer);
            else
            {
                if (!string.IsNullOrEmpty(state.Hint))
                {
                    if (_Hint == null)
                        _Hint = new HtmlSimple("span", state.Hint, new HtmlAttribute("class", "help-inline"));

                    _Hint.Write(writer);
                }
            }
        }

        protected void RenderInput(System.IO.TextWriter writer, UI.UIControlState state, params HtmlAttribute[] attributes)
        {
            
            _Input = CreateInput(state);

            for (int i = 0; i < attributes.Length; i++)
                _Input.Attributes[attributes[i].Name] = attributes[i].Value;

            _Input.Write(writer);
        }

        public virtual void Render(System.IO.TextWriter writer, UI.UIControlState state, params HtmlAttribute[] attributes)
        {

            writer.Write("<div class=\"");
            writer.Write(string.IsNullOrEmpty(state.ErrorMessage) ? "control-group" : "control-group error");
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