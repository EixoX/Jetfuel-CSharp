using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIRadioGroup : UIControlAttribute
    {
        public UIRadioGroup(string defaultLabel, string defaultHint, Type choiceSource)
            : base(defaultLabel, defaultHint, choiceSource) { }
        public UIRadioGroup(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UIRadioGroup(string defaultLabel, Type choiceSource)
            : base(defaultLabel, null, choiceSource) { }
        public UIRadioGroup(Type choiceSource)
            : base(null, null, choiceSource) { }
        public UIRadioGroup(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UIRadioGroup() { }
    }
}
