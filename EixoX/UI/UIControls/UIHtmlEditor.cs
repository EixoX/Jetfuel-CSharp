using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIHtmlEditor : UIControlAttribute
    {
        public UIHtmlEditor(string defaultLabel, string defaultHint, Type classStorageType)
            : base(defaultLabel, defaultHint, classStorageType) { }
        public UIHtmlEditor(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UIHtmlEditor(string defaultLabel, Type classStorageType)
            : base(defaultLabel, null, classStorageType) { }
        public UIHtmlEditor(Type classStorageType)
            : base(null, null, classStorageType) { }
        public UIHtmlEditor(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UIHtmlEditor() { }
    }
}
