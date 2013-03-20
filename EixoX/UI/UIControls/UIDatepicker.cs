using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIDatepicker : UIControlAttribute
    {
        public UIDatepicker(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UIDatepicker(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UIDatepicker() { }
    }
}
