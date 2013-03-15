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


        protected virtual HtmlNode CreateLabel(UI.UIControlState state)
        {
            return new HtmlSimple("label", state.Label, new HtmlAttribute("for", state.Name));
        }
        
        protected abstract HtmlNode CreateInput(UI.UIControlState state);

        protected virtual HtmlNode CreateHint(UI.UIControlState state)
        {
            return new HtmlSimple("span", state.Hint, new HtmlAttribute("class", "hint"));
        }

        protected virtual HtmlNode CreateError(UI.UIControlState state)
        {
            return string.IsNullOrEmpty(state.ErrorMessage) ? null :
                new HtmlSimple("span", state.ErrorMessage, new HtmlAttribute("class", "error"));
        }

        public void Render(System.IO.TextWriter writer, UI.UIControlState state, params HtmlAttribute[] attributes)
        {
            if (_Label == null)
                _Label = CreateLabel(state);

            _Label.Write(writer);

            if (_Input == null)
                _Input = CreateInput(state);

            for (int i = 0; i < attributes.Length; i++)
                _Input.Attributes[attributes[i].Name] = attributes[i].Value;

            this._Input.Write(writer);

            HtmlNode error = CreateError(state);
            if (error != null)
                error.Write(writer);
            else
            {
                if (!string.IsNullOrEmpty(state.Hint))
                {
                    if (_Hint == null)
                        _Hint = CreateHint(state);

                    _Hint.Write(writer);
                }
            }

            
        }
    }
}
