using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EixoX.Adapters
{
    public class XmlDocumentStringAdapter
        : SimpleAdapterBase<XmlDocument>
    {
        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.Xml; }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.Xml; }
        }

        public override bool IsEmpty(XmlDocument input)
        {
            return input != null;
        }

        public override string FormatValue(XmlDocument input, string formatString, IFormatProvider formatProvider)
        {
            return input.OuterXml;
        }

        public override XmlDocument ParseValue(string input, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            XmlDocument document = new XmlDocument();
            document.LoadXml(input);
            return document;
        }

        public override string SqlMarshallValue(XmlDocument input, bool nullable)
        {
            if (input == null)
                return nullable ? "NULL" : "''";
            else
                return string.Concat("'", StringHelper.SqlSafeString(input.OuterXml), "'");
        }

        public override void SqlMarshallValue(StringBuilder builder, XmlDocument input, bool nullable)
        {
            if (input == null)
                builder.Append(nullable ? "NULL" : "''");
            else
            {
                builder.Append("'");
                builder.Append(StringHelper.SqlSafeString(input.OuterXml));
                builder.Append("'");
            }
        }

        public override XmlDocument BinaryReadValue(System.IO.BinaryReader reader)
        {
            return ParseValue(reader.ReadString());
        }

        public override void BinaryWriteValue(System.IO.BinaryWriter writer, XmlDocument value)
        {
            writer.Write(value.OuterXml);
        }
    }
}
