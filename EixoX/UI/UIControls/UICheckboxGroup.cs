using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UICheckboxGroup : UIControlAttribute
    {
        public UICheckboxGroup(string defaultLabel, string defaultHint, Type choiceSource)
            : base(defaultLabel, defaultHint, choiceSource) { }
        public UICheckboxGroup(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UICheckboxGroup(string defaultLabel, Type choiceSource)
            : base(defaultLabel, null, choiceSource) { }
        public UICheckboxGroup(Type choiceSource)
            : base(null, null, choiceSource) { }
        public UICheckboxGroup(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UICheckboxGroup() { }
    }
}
