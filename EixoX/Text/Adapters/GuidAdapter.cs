using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text.Adapters
{
    public class GuidAdapter : TextAdapter<Guid>
    {
        public bool IsEmpty(Guid value)
        {
            return value != Guid.Empty;
        }

        public Guid ParseValue(string input)
        {
            return string.IsNullOrEmpty(input) ?
                Guid.Empty :
                new Guid(input);
        }

        public string FormatValue(Guid input)
        {
            return input.ToString();
        }

        public bool IsEmpty(object input)
        {
            return input == null || ((Guid)input) == Guid.Empty;
        }

        public object ParseObject(string input)
        {
            return string.IsNullOrEmpty(input) ? null : (object)new Guid(input);
        }

        public string FormatObject(object input)
        {
            return input == null ? null : ((Guid)input).ToString();
        }
    }
}
