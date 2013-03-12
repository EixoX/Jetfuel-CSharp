using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class HtmlInputTerm
    {
        public HtmlInputType InputType { get; set; }
        public string Label { get; set; }
        public string Placeholder { get; set; }
        public string Hint { get; set; }
        public object Value { get; set; }
        public string ErrorMessage { get; set; }
        public HtmlInputOptionSource InputOptions { get; set; }
        public string Name { get; set; }
    }
}
