using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EixoX.Adapters
{
    public class XmlElementStringAdapter
        : SimpleAdapterBase<XmlElement>
    {
        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.Xml; }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.Xml; }
        }

        public override bool IsEmpty(XmlElement input)
        {
            return input == null;
        }

        public override string FormatValue(XmlElement input, string formatString, IFormatProvider formatProvider)
        {
            return input.OuterXml;
        }

        public override XmlElement ParseValue(string input, IFormatProvider formatProvider)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(input);
            return doc.DocumentElement;
        }

        public override string SqlMarshallValue(XmlElement input, bool nullable)
        {
            if (input == null)
                return nullable ? "NULL" : "''";
            else
                return string.Concat("'", StringHelper.SqlSafeString(input.OuterXml), "'");

        }

        public override void SqlMarshallValue(StringBuilder builder, XmlElement input, bool nullable)
        {
            if (input == null)
            {
                builder.Append(nullable ? "NULL" : "''");
            }
            else
            {
                builder.Append("'");
                builder.Append(StringHelper.SqlSafeString(input.OuterXml));
                builder.Append("'");
            }
        }

        public override XmlElement BinaryReadValue(System.IO.BinaryReader reader)
        {
            return ParseValue(reader.ReadString());
        }

        public override void BinaryWriteValue(System.IO.BinaryWriter writer, XmlElement value)
        {
            writer.Write(value.OuterXml);
        }
    }
}
