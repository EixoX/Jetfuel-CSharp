using EixoX.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIDropdown : UIControlAttribute
    {
        public UIDropdown(int minInclusive, int maxExclusive)
            : base(minInclusive, maxExclusive) { }
        public UIDropdown(int[] array) : base(array) { }
        public UIDropdown(string defaultLabel, int[] array)
            : base(defaultLabel, array) { }
        public UIDropdown(string defaultLabel, string defaultHint, Type choiceSource)
            : base(defaultLabel, defaultHint, choiceSource) { }
        public UIDropdown(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UIDropdown(string defaultLabel, Type choiceSource)
            : base(defaultLabel, null, choiceSource) { }
        public UIDropdown(Type choiceSource)
            : base(null, null, choiceSource) { }
        public UIDropdown(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UIDropdown() { }


    }
}
