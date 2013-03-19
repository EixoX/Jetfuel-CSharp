using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIDropdown : UIControlAttribute
    {
        public UIDropdown(string defaultLabel, string defaultHint, Type classStorageType)
            : base(defaultLabel, defaultHint, classStorageType) { }
        public UIDropdown(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UIDropdown(string defaultLabel, Type classStorageType)
            : base(defaultLabel, null, classStorageType) { }
        public UIDropdown(Type classStorageType)
            : base(null, null, classStorageType) { }
        public UIDropdown(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UIDropdown() { }


    }
}
