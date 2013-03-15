using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UICheckbox : UIControlAttribute
    {
        public UICheckbox(string defaultLabel, string defaultHint, Type classStorageType)
            : base(defaultLabel, defaultHint, classStorageType) { }
        public UICheckbox(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UICheckbox(string defaultLabel, Type classStorageType)
            : base(defaultLabel, null, classStorageType) { }
        public UICheckbox(Type classStorageType)
            : base(null, null, classStorageType) { }
        public UICheckbox() { }
    }
}
