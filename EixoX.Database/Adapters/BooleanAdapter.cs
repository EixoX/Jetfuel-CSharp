using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Database.Adapters
{
    public class BooleanAdapter
        : DatabaseAspectAdapter
    {

        public void AppendFormatObject(StringBuilder builder, object value)
        {
            builder.Append(((bool)value) ? "1" : "0");
        }



        public bool IsEmpty(object value)
        {
            return value == null;
        }
    }
}
