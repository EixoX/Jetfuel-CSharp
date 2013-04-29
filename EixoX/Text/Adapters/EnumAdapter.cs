using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public class EnumAdapter
        : TextAdapterBase<Enum>
    {
        private readonly Type _DataType;
        private readonly string _FormatString;

        public EnumAdapter(Type dataType, string formatString)
        {
            this._DataType = dataType;
            this._FormatString = string.IsNullOrEmpty(formatString) ? "{0}" : formatString;
        }


        public override bool IsEmpty(Enum value)
        {
            return value == null || !Enum.IsDefined(_DataType, value);
        }

        public override Enum ParseValue(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            else
                return (Enum)Enum.Parse(_DataType, input);
        }

        public override string FormatValue(Enum input)
        {
            return input.ToString();
        }

        public override Enum ParseValue(string input, IFormatProvider formatProvider)
        {
            return ParseValue(input);
        }

        public override string FormatValue(Enum input, IFormatProvider formatProvider)
        {
            return FormatValue(input);
        }
    }
}
