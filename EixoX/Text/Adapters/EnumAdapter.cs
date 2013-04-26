using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public class EnumAdapter
        : TextAdapter
    {
        private readonly Type _DataType;

        public EnumAdapter(Type dataType)
        {
            this._DataType = dataType;
        }

        public bool IsEmpty(object input)
        {
            return input == null || Enum.IsDefined(_DataType, input);
        }

        public object ParseObject(string input)
        {
            return string.IsNullOrEmpty(input) ? null : Enum.Parse(_DataType, input);
        }

        public string FormatObject(object input)
        {
            return input == null ? null : Enum.Format(_DataType, input, "d");
        }
    }
}
