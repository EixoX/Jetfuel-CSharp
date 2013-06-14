using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System
{
    public static class TextWriterExtensions
    {
        public static void WriteHtmlAttribute(this TextWriter writer, string name, object value)
        {
            writer.Write(name);
            writer.Write("=\"");
            writer.Write(value == null ? "" : value.ToString());
            writer.Write("\"");
        }
    }
}
