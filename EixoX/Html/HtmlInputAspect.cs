using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Globalization;
using EixoX.Interceptors;
using EixoX.Restrictions;

namespace EixoX.Html
{
    public class HtmlInputAspect<T> : AbstractAspect<HtmlInputAspectMember>
    {
        private static HtmlInputAspect<T> _HtmlDefault;
        public HtmlInputAspect() : base(typeof(T)) { }
        public static HtmlInputAspect<T> HtmlDefault
        {
            get { return _HtmlDefault ?? (_HtmlDefault = new HtmlInputAspect<T>()); }
        }

        protected override bool CreateAspectFor(ClassAcessor acessor, out HtmlInputAspectMember member)
        {
            HtmlInputAttribute hia = acessor.GetAttribute<HtmlInputAttribute>(true);
            if (hia == null)
            {
                member = null;
                return false;
            }
            else
            {
                member = new HtmlInputAspectMember(
                                acessor,
                                hia.Fieldset,
                                hia.InferType ? HtmlInputAspectMember.InferInputType(acessor.DataType) : hia.InputType,
                                hia.OptionSource == null && acessor.DataType.IsEnum ? new HtmlInputOptionEnum(acessor.DataType) : null,
                                InterceptorAspect<T>.Instance.GetMemberOrDefault(acessor.Name),
                                RestrictionAspect<T>.Instance.GetMemberOrDefault(acessor.Name),
                                GlobalizationAspect<T>.Instance.GetMemberOrDefault(acessor.Name));

                return true;
            }
        }

        protected virtual void BeginWrapper(HtmlWriter writer, HtmlInputTerm term)
        {
            writer.WriteLine();
            writer.WriteBeginTag("div", false);
        }

        protected virtual void EndWrapper(HtmlWriter writer, HtmlInputTerm term)
        {
            writer.WriteLine();
            writer.WriteCloseTag("div");
        }

        protected virtual void WriteLabel(HtmlWriter writer, HtmlInputTerm term)
        {
            writer.WriteLine();
            writer.WriteSimpleTag("label", term.Label, new HtmlAttribute("for", term.Name));
            writer.WriteLine();

        }

        protected virtual void WriteInput(HtmlWriter writer, HtmlInputTerm term)
        {
            switch (term.InputType)
            {
                case HtmlInputType.Checkbox:
                    WriteInputCheckbox(writer, term); break;
                case HtmlInputType.CheckboxGroup:
                    WriteInputCheckboxGroup(writer, term); break;
                case HtmlInputType.Dropdown:
                    WriteInputDrowpdown(writer, term); break;
                case HtmlInputType.Hidden:
                    WriteInputHidden(writer, term); break;
                case HtmlInputType.HtmlEditor:
                    WriteInputHtmlEditor(writer, term); break;
                case HtmlInputType.Password:
                    WriteInputPassword(writer, term); break;
                case HtmlInputType.RadioGroup:
                    WriteInputRadioGroup(writer, term); break;
                case HtmlInputType.Text:
                    WriteInputText(writer, term); break;
                case HtmlInputType.Date:
                    WriteInputDate(writer, term); break;
                case HtmlInputType.File:
                    WriteInputFile(writer, term); break;
                case HtmlInputType.Textarea:
                    WriteInputTextarea(writer, term); break;
            }
        }

        protected virtual void WriteInputFile(HtmlWriter writer, HtmlInputTerm term)
        {
            BeginWrapper(writer, term);
            WriteLabel(writer, term);

            writer.WriteBeginTag("input", true,
                new HtmlAttribute("type", "file"),
                new HtmlAttribute("name", term.Name),
                new HtmlAttribute("id", term.Name),
                new HtmlAttribute("placeholder", term.Placeholder));

            WriteHint(writer, term);
            WriteError(writer, term);
            EndWrapper(writer, term);
        }

        protected virtual void WriteInputDate(HtmlWriter writer, HtmlInputTerm term)
        {
            BeginWrapper(writer, term);
            WriteLabel(writer, term);

            writer.WriteBeginTag("input", true,
                new HtmlAttribute("type", "text"),
                new HtmlAttribute("name", term.Name),
                new HtmlAttribute("id", term.Name),
                new HtmlAttribute("class", "date"),
                new HtmlAttribute("placeholder", term.Placeholder));

            WriteHint(writer, term);
            WriteError(writer, term);
            EndWrapper(writer, term);
        }

        protected virtual void WriteInputCheckbox(HtmlWriter writer, HtmlInputTerm term)
        {
            BeginWrapper(writer, term);
            WriteLabel(writer, term);
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
            WriteHint(writer, term);
            WriteError(writer, term);
            EndWrapper(writer, term);
        }

        protected virtual void WriteInputCheckboxGroup(HtmlWriter writer, HtmlInputTerm term)
        {
            BeginWrapper(writer, term);
            WriteLabel(writer, term);
            if (term.InputOptions != null)
                foreach (HtmlInputOption option in term.InputOptions.GetInputOptions())
                {
                    if (term.Value == option.Key)
                    {
                        writer.WriteBeginTag(
                        "input",
                        true,
                        new HtmlAttribute("id", term.Name + "_" + option.Key),
                        new HtmlAttribute("name", term.Name),
                        new HtmlAttribute("type", "checkbox"),
                        new HtmlAttribute("value", term.Value),
                        new HtmlAttribute("checked", "checked"));
                    }
                    else
                    {
                        writer.WriteBeginTag(
                        "input",
                        true,
                        new HtmlAttribute("id", term.Name + "_" + option.Key),
                        new HtmlAttribute("name", term.Name),
                        new HtmlAttribute("type", "checkbox"),
                        new HtmlAttribute("value", term.Value));
                    }
                }
            WriteHint(writer, term);
            WriteError(writer, term);
            EndWrapper(writer, term);

        }

        protected virtual void WriteInputDrowpdown(HtmlWriter writer, HtmlInputTerm term)
        {
            BeginWrapper(writer, term);
            WriteLabel(writer, term);
            writer.WriteBeginTag("select", false,
                new HtmlAttribute("id", term.Name),
                new HtmlAttribute("name", term.Name));

            if (term.InputOptions != null)
                foreach (HtmlInputOption option in term.InputOptions.GetInputOptions())
                {
                    if (option.Value.Equals(term.Value))
                    {
                        writer.WriteSimpleTag("option", option.Value == null ? "" : option.Value.ToString(),
                            new HtmlAttribute("value", option.Key),
                            new HtmlAttribute("selected", "selected"));
                    }
                    else
                    {
                        writer.WriteSimpleTag("option", option.Value == null ? "" : option.Value.ToString(),
                            new HtmlAttribute("value", option.Key));
                    }
                }
            writer.WriteCloseTag("select");
            WriteHint(writer, term);
            WriteError(writer, term);
            EndWrapper(writer, term);
        }

        protected virtual void WriteInputHidden(HtmlWriter writer, HtmlInputTerm term)
        {
            writer.WriteBeginTag("input", true,
                new HtmlAttribute("type", "hidden"),
                new HtmlAttribute("name", term.Name),
                new HtmlAttribute("id", term.Name),
                new HtmlAttribute("value", term.Value));
        }

        protected virtual void WriteInputHtmlEditor(HtmlWriter writer, HtmlInputTerm term)
        {
            BeginWrapper(writer, term);
            WriteLabel(writer, term);
            writer.WriteSimpleTag("textarea", term.Value == null ? "" : term.Value.ToString(),
                new HtmlAttribute("name", term.Name),
                new HtmlAttribute("id", term.Name),
                new HtmlAttribute("rows", 5),
                new HtmlAttribute("cols", 20),
                new HtmlAttribute("class", "htmlEditor"),
                new HtmlAttribute("placeholder", term.Placeholder));
            WriteHint(writer, term);
            WriteError(writer, term);
            EndWrapper(writer, term);
        }

        protected virtual void WriteInputPassword(HtmlWriter writer, HtmlInputTerm term)
        {
            BeginWrapper(writer, term);
            WriteLabel(writer, term);
            writer.WriteBeginTag("input", true,
                new HtmlAttribute("type", "password"),
                new HtmlAttribute("name", term.Name),
                new HtmlAttribute("id", term.Name),
                new HtmlAttribute("value", term.Value),
                new HtmlAttribute("placeholder", term.Placeholder));
            WriteHint(writer, term);
            WriteError(writer, term);
            EndWrapper(writer, term);
        }

        protected virtual void WriteInputRadioGroup(HtmlWriter writer, HtmlInputTerm term)
        {
            BeginWrapper(writer, term);
            WriteLabel(writer, term);
            if (term.InputOptions != null)
                foreach (HtmlInputOption option in term.InputOptions.GetInputOptions())
                {
                    if (term.Value == option.Key)
                    {
                        writer.WriteBeginTag(
                        "input",
                        true,
                        new HtmlAttribute("id", term.Name + "_" + option.Key),
                        new HtmlAttribute("name", term.Name),
                        new HtmlAttribute("type", "radio"),
                        new HtmlAttribute("value", term.Value),
                        new HtmlAttribute("checked", "checked"));
                    }
                    else
                    {
                        writer.WriteBeginTag(
                        "input",
                        true,
                        new HtmlAttribute("id", term.Name + "_" + option.Key),
                        new HtmlAttribute("name", term.Name),
                        new HtmlAttribute("type", "radio"),
                        new HtmlAttribute("value", term.Value));
                    }
                }
            WriteHint(writer, term);
            WriteError(writer, term);
            EndWrapper(writer, term);
        }

        protected virtual void WriteInputText(HtmlWriter writer, HtmlInputTerm term)
        {
            BeginWrapper(writer, term);
            WriteLabel(writer, term);

            writer.WriteBeginTag("input", true,
                new HtmlAttribute("type", "text"),
                new HtmlAttribute("name", term.Name),
                new HtmlAttribute("id", term.Name),
                new HtmlAttribute("value", term.Value),
                new HtmlAttribute("placeholder", term.Placeholder));

            WriteHint(writer, term);
            WriteError(writer, term);
            EndWrapper(writer, term);
        }

        protected virtual void WriteInputTextarea(HtmlWriter writer, HtmlInputTerm term)
        {
            BeginWrapper(writer, term);
            WriteLabel(writer, term);
            writer.WriteSimpleTag("textarea", term.Value == null ? "" : term.Value.ToString(),
                new HtmlAttribute("name", term.Name),
                new HtmlAttribute("id", term.Name),
                new HtmlAttribute("rows", 5),
                new HtmlAttribute("cols", 20),
                new HtmlAttribute("placeholder", term.Placeholder));
            WriteHint(writer, term);
            WriteError(writer, term);
            EndWrapper(writer, term);
        }

        protected virtual void WriteHint(HtmlWriter writer, HtmlInputTerm term)
        {
            writer.WriteLine();
            writer.WriteSimpleTag("div", term.Hint, new HtmlAttribute("class", "hint"));
        }

        public virtual void WriteError(HtmlWriter writer, HtmlInputTerm term)
        {
            writer.WriteLine();
            writer.WriteSimpleTag("div", term.ErrorMessage, new HtmlAttribute("class", "error"));
        }

        public IEnumerable<HtmlInputTerm> GetInputs(T entity, int lcid, bool validateRestrictions)
        {
            foreach (HtmlInputAspectMember member in this)
                yield return member.CreateTerm(entity, lcid, validateRestrictions);
        }

        public IEnumerable<HtmlInputTerm> GetInputs(T entity, string fieldset, int lcid, bool validateRestrictions)
        {
            foreach (HtmlInputAspectMember member in this)
                if (fieldset.Equals(member.Fieldset, StringComparison.OrdinalIgnoreCase))
                    yield return member.CreateTerm(entity, lcid, validateRestrictions);
        }

        public void WriteInputs(HtmlWriter writer, T entity, int lcid, bool validateRestrictions)
        {
            foreach (HtmlInputTerm term in GetInputs(entity, lcid, validateRestrictions))
                WriteInput(writer, term);
        }

        public void WriteInputs(HtmlWriter writer, T entity, string fieldset, int lcid, bool validateRestrictions)
        {
            foreach (HtmlInputTerm term in GetInputs(entity, fieldset, lcid, validateRestrictions))
                WriteInput(writer, term);
        }

        public void WriteInputs(HtmlWriter writer, T entity, bool validateRestrictions)
        {
            WriteInputs(writer, entity, System.Globalization.CultureInfo.CurrentUICulture.LCID, validateRestrictions);
        }

        public void WriteInputs(HtmlWriter writer, T entity, string fieldset, bool validateRestrictions)
        {
            WriteInputs(writer, entity, System.Globalization.CultureInfo.CurrentUICulture.LCID, validateRestrictions);
        }

        public void WriteInputs(System.IO.TextWriter writer, T entity, int lcid, bool validateRestrictions)
        {
            WriteInputs(new HtmlWriter(writer), entity, lcid, validateRestrictions);
        }

        public void WriteInputs(System.IO.TextWriter writer, T entity, string fieldset, int lcid, bool validateRestrictions)
        {
            WriteInputs(new HtmlWriter(writer), entity, fieldset, lcid, validateRestrictions);
        }

        public void WriteInputs(System.IO.TextWriter writer, T entity, bool validateRestrictions)
        {
            WriteInputs(new HtmlWriter(writer), entity, validateRestrictions);
        }

        public void WriteInputs(System.IO.TextWriter writer, T entity, string fieldset, bool validateRestrictions)
        {
            WriteInputs(new HtmlWriter(writer), entity, fieldset, validateRestrictions);
        }

        public void WriteInput(HtmlWriter writer, string memberName, T entity, int lcid, bool validateRestrictions)
        {
            WriteInput(writer, base[memberName].CreateTerm(entity, lcid, validateRestrictions));
        }

        public void WriteInput(System.IO.TextWriter writer, string memberName, T entity, int lcid, bool validateRestrictions)
        {
            WriteInput(new HtmlWriter(writer), base[memberName].CreateTerm(entity, lcid, validateRestrictions));
        }
    }
}
