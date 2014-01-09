using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public struct UIControlState
    {
        public UIPresenterControl Control { get; set; }
        public object Value { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Hint { get; set; }
        public IEnumerable<KeyValuePair<object, object>> Options { get; set; }
        public string ErrorMessage { get; set; }
        public bool Validated { get; set; }
        public IFormatProvider FormatProvider { get; set; }
    }
}
