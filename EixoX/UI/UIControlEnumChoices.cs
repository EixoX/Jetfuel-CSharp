using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIControlEnumChoices : UIControlChoices
    {
        private readonly Type _EnumType;

        public UIControlEnumChoices(Type enumType)
        {
            this._EnumType = enumType;
        }

        public IEnumerable<KeyValuePair<object, object>> GetChoices()
        {
            foreach (var enumValue in Enum.GetValues(_EnumType))
                yield return new KeyValuePair<object, object>((int) enumValue, enumValue);
        }
    }
}
