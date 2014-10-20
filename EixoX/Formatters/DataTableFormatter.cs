using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace EixoX.Formatters
{
    public class DataTableFormatter
    {

        public static void ToCsv(DataTable dataTable, string separator, TextWriter writer, IFormatProvider formatProvider)
        {
            int colCount = dataTable.Columns.Count;
            writer.Write(dataTable.Columns[0].ColumnName);
            for (int i = 1; i < colCount; i++)
            {
                writer.Write(separator);
                writer.Write(dataTable.Columns[i].ColumnName);
            }
            writer.WriteLine();
            foreach (DataRow row in dataTable.Rows)
            {
                writer.Write(string.Format(formatProvider, "{0}", row[0]));
                for (int i = 1; i < colCount; i++)
                {
                    writer.Write(separator);
                    writer.Write(string.Format(formatProvider, "{0}", row[i]));
                }
                writer.WriteLine();
            }
        }
    }
}
