using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public class GuidAdapter
        : AbstractTextAdapter<Guid>
    {
        protected override Guid Parse(string text, IFormatProvider formatProvider)
        {
            return new Guid(text);
        }

        protected override string Format(Guid value, IFormatProvider formatProvider)
        {
            return value.ToString();
        }
    }
}
