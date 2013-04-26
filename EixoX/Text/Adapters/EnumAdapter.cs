using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public class EnumAdapter
        : TextAdapter
    {
        private readonly Type _DataType;
        private readonly string _FormatString;

        public EnumAdapter(Type dataType, string formatString)
        {
            this._DataType = dataType;
            this._FormatString = string.IsNullOrEmpty(formatString) ? "{0}" : formatString;
        }

        public bool IsEmpty(object input)
        {
            return input == null || !Enum.IsDefined(_DataType, input);
        }

        public object ParseObject(string input)
        {
            return string.IsNullOrEmpty(input) ? null : Enum.Parse(_DataType, input);
        }

        public string FormatObject(object input)
        {
            return input == null ? null : string.Format(_FormatString, (int)input);
        }
    }
}
