using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    /// <summary>
    /// Represents an Aspect to write Bootstrap HTML forms
    /// </summary>
    /// <typeparam name="T">Typeof the class that will be used to generate inputs</typeparam>
    public class BootstrapHorizontalAspect<T> : HtmlInputAspect<T>
    {
        #region Singleton
        private static BootstrapHorizontalAspect<T> _Instance;
        public static BootstrapHorizontalAspect<T> Instance
        {
            get { return _Instance ?? (_Instance = new BootstrapHorizontalAspect<T>()); }
        }
        #endregion

        protected override void BeginWrapper(HtmlWriter writer, HtmlInputTerm term)
        {
            writer.WriteLine();
            if (string.IsNullOrEmpty(term.ErrorMessage))
            {
                writer.WriteBeginTag("div", false, new HtmlAttribute("class", "control-group"));
            }
            else
            {
                writer.WriteBeginTag("div", false, new HtmlAttribute("class", "control-group error"));
            }
        }

        protected override void WriteInputCheckbox(HtmlWriter writer, HtmlInputTerm term)
        {
            BeginWrapper(writer, term);
            WriteLabel(writer, new HtmlInputTerm() { Label = string.Empty });

            writer.WriteBeginTag("label", false, new HtmlAttribute("class", "checkbox"));

            if ((bool)term.Value)
            {
                writer.WriteBeginTag(
                    "input",
                    true,
                    new HtmlAttribute("id", term.Name),
                    new HtmlAttribute("name", term.Name),
                    new HtmlAttribute("type", "checkbox"),
                    new HtmlAttribute("value", "true"),
                    new HtmlAttribute("checked", "checked"));
            }
            else
            {
                writer.WriteBeginTag(
                    "input",
                    true,
                    new HtmlAttribute("id", term.Name),
                    new HtmlAttribute("name", term.Name),
                    new HtmlAttribute("type", "checkbox"),
                    new HtmlAttribute("value", "true"));
            }

            writer.WriteHtml(term.Label);
            writer.WriteCloseTag("label");

            WriteHint(writer, term);
            WriteError(writer, term);
            EndWrapper(writer, term);
        }

        protected override void EndWrapper(HtmlWriter writer, HtmlInputTerm term)
        {
            writer.WriteCloseTag("div");
        }

        protected override void WriteLabel(HtmlWriter writer, HtmlInputTerm term)
        {
            writer.WriteLine();
            writer.WriteSimpleTag("label", term.Label,
                new HtmlAttribute("for", term.Name),
                new HtmlAttribute("class", "control-label"));
            writer.WriteLine();
            writer.WriteBeginTag("div", false, new HtmlAttribute("class", "controls"));
        }

        protected override void WriteHint(HtmlWriter writer, HtmlInputTerm term)
        {
            writer.WriteLine();
            if (!string.IsNullOrEmpty(term.ErrorMessage))
            {
                writer.WriteSimpleTag("span", term.ErrorMessage, new HtmlAttribute("class", "help-inline"));
            }
            else if (!string.IsNullOrEmpty(term.Hint))
            {
                writer.WriteSimpleTag("span", term.Hint, new HtmlAttribute("class", "help-inline"));
            }
            writer.WriteCloseTag("div");
            
        }

        public override void WriteError(HtmlWriter writer, HtmlInputTerm term)
        {
            //written on the write hint text.
        }

    }
}
