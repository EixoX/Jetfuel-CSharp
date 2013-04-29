using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public class GuidAdapter : TextAdapterBase<Guid>
    {

        public override bool IsEmpty(Guid value)
        {
            return value == Guid.Empty;
        }

        public override Guid ParseValue(string input)
        {
            return string.IsNullOrEmpty(input) ?
                Guid.Empty :
                new Guid(input);
        }

        public override string FormatValue(Guid input)
        {
            return input.ToString();
        }

        public override Guid ParseValue(string input, IFormatProvider formatProvider)
        {
            return ParseValue(input);
        }

        public override string FormatValue(Guid input, IFormatProvider formatProvider)
        {
            return FormatValue(input);
        }
    }
}
