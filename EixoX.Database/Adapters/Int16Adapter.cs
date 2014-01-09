using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Database.Adapters
{
    public class Int16Adapter
        : DatabaseAspectAdapter
    {
        public static readonly Int16 Zero = 0;
        public bool IsEmpty(object value)
        {
            return value == null || ((Int16)value) == Zero;
        }

        public void AppendFormatObject(StringBuilder builder, object value)
        {
            builder.Append((short)value);
        }
    }
}
