using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public abstract class HtmlControl : UI.UIControl
    {
        HtmlNode _Label;
        HtmlNode _Hint;
        HtmlNode _Input;

        protected abstract HtmlNode CreateInput(UI.UIControlState state);

        public virtual void Render(System.IO.TextWriter writer, UI.UIControlState state, params HtmlAttribute[] attributes)
        {
            if (_Label == null)
                _Label = new HtmlSimple("label", state.Label, new HtmlAttribute("for", state.Name));

            _Label.Write(writer);

            if (_Input == null)
                _Input = CreateInput(state);

            for (int i = 0; i < attributes.Length; i++)
                _Input.Attributes[attributes[i].Name] = attributes[i].Value;

            this._Input.Write(writer);

            if (!string.IsNullOrEmpty(state.ErrorMessage))
                new HtmlSimple("span", state.ErrorMessage, new HtmlAttribute("class", "error")).Write(writer);
            else
            {
                if (!string.IsNullOrEmpty(state.Hint))
                {
                    if (_Hint == null)
                        _Hint = new HtmlSimple("span", state.Hint, new HtmlAttribute("class", "hint"));

                    _Hint.Write(writer);
                }
            }

            
        }
    }
}
