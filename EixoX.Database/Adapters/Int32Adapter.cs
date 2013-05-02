using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Database.Adapters
{
    public class Int32Adapter
        : DatabaseAspectAdapter
    {

        public bool IsEmpty(object value)
        {
            return value == null || ((int)value) == 0;
        }

        public void AppendFormatObject(StringBuilder builder, object value)
        {
            builder.Append((int)value);
        }
    }
}
