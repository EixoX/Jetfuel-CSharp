using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Database.Adapters
{
    public class Base64Adapter
        : DatabaseAspectAdapter
    {
        public void AppendFormatObject(StringBuilder builder, object value)
        {
            builder.Append((long)value);
        }

        public bool IsEmpty(object value)
        {
            return value == null || ((long)value) == 0L;
        }
    }
}
