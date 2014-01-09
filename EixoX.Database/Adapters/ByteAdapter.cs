using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Database.Adapters
{
    public class ByteAdapter
        : DatabaseAspectAdapter
    {
        public void AppendFormatObject(StringBuilder builder, object value)
        {
            builder.Append((byte)value);
        }

        public static readonly byte Zero = 0;
        public bool IsEmpty(object value)
        {
            return value == null || ((byte)value) == Zero;
        }
    }
}
