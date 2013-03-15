using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIRadioGroup : UIControlAttribute
    {
        public UIRadioGroup(string defaultLabel, string defaultHint, Type classStorageType)
            : base(defaultLabel, defaultHint, classStorageType) { }
        public UIRadioGroup(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UIRadioGroup(string defaultLabel, Type classStorageType)
            : base(defaultLabel, null, classStorageType) { }
        public UIRadioGroup(Type classStorageType)
            : base(null, null, classStorageType) { }
        public UIRadioGroup() { }
    }
}
