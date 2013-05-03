using System;

namespace EixoX.Text
{
    public class Int32Adapter
        : AbstractTextAdapter<Int32>
    {


        protected override int Parse(string text, IFormatProvider formatProvider)
        {
            return int.Parse(text, formatProvider);
        }

        protected override string Format(int value, IFormatProvider formatProvider)
        {
            return value.ToString(formatProvider);
        }
    }
}
