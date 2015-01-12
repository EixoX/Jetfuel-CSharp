using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Text
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class FixedLengthColumnAttribute : Attribute
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        public string FormatString { get; set; }
        public string CultureInfoOverride { get; set; }
        public Type ParserType { get; set; }

        public FixedLengthColumnAttribute(int offset, int length)
        {
            this.Offset = offset;
            this.Length = length;
        }

        public FixedLengthColumnAttribute(int offset, int length, Type parserType)
            : this(offset, length)
        {
            this.ParserType = parserType;
        }

        public FixedLengthColumnAttribute(int offset, int length, string formatString)
            : this(offset, length)
        {
            this.FormatString = formatString;
        }
    }
}
