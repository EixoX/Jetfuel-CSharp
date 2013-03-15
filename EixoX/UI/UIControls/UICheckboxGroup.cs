using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UICheckboxGroup : UIControlAttribute
    {
        public UICheckboxGroup(string defaultLabel, string defaultHint, Type classStorageType)
            : base(defaultLabel, defaultHint, classStorageType) { }
        public UICheckboxGroup(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UICheckboxGroup(string defaultLabel, Type classStorageType)
            : base(defaultLabel, null, classStorageType) { }
        public UICheckboxGroup(Type classStorageType)
            : base(null, null, classStorageType) { }
        public UICheckboxGroup() { }
    }
}
