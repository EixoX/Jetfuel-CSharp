using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Database.Adapters
{
    public class GuidAdapter
        : DatabaseAspectAdapter
    {

        public bool IsEmpty(object value)
        {
            return value == null || ((Guid)value) == Guid.Empty;
        }

        public void AppendFormatObject(StringBuilder builder, object value)
        {
            builder.Append('\'');
            builder.Append(value);
            builder.Append('\'');
        }
    }
}
