using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Database.Adapters
{
    public class Int64Adapter
        : DatabaseAspectAdapter
    {
        public bool IsEmpty(object value)
        {
            return value == null || ((long)value) == 0;
        }

        public void AppendFormatObject(StringBuilder builder, object value)
        {
            builder.Append((long)value);
        }
    }
}
