using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Database.Adapters
{
    public class NVarcharAdapter
        : DatabaseAspectAdapter
    {

        public bool IsEmpty(object value)
        {
            return value == null;
        }

        public void AppendFormatObject(StringBuilder builder, object value)
        {
            builder.Append("'");
            builder.Append(((string)value).Replace("'", "''"));
            builder.Append("'");
        }
    }
}
