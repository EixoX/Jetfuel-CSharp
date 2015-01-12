using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace EixoX.Text
{
    public class FixedLengthTextReader
        : IDisposable
    {
        private readonly StreamReader reader;
        private bool isClosed = false;
        private string line;
        private int lineNumber = 0;

        public CultureInfo Culture = System.Globalization.CultureInfo.InvariantCulture;
        
        public string CommentQualifier = "#";
        public bool IgnoreComments = true;
        public bool IgnoreBlankLines = true;
        

        public FixedLengthTextReader(Stream input, Encoding encoding)
        {
            this.reader = new StreamReader(input, encoding);
        }

        public FixedLengthTextReader(StreamReader reader)
        {
            this.reader = reader;
        }


        public void Close()
        {
            if (!this.isClosed)
            {
                this.isClosed = true;
                this.reader.Close();
            }
        }

        public void Dispose()
        {
            Close();
            this.reader.Dispose();
        }


        public bool Read()
        {
            if (this.isClosed)
                return false;

            this.line = reader.ReadLine();
            if (this.line == null)
                return false;

            lineNumber++;

            if (IgnoreBlankLines && line.Trim().Length < 1)
                return Read();
            else if (IgnoreComments && line.StartsWith(this.CommentQualifier))
                return Read();
            else
                return true;
        }

        public bool IsClosed { get { return this.isClosed; } }
        public string Line { get { return this.line; } }
        public int LineNumber { get { return this.lineNumber; } }

        public string GetString(int start, int length)
        {
            if ((length - start) > line.Length)
                return line.Substring(start, line.Length - start).Trim();
            else
                return line.Substring(start, length).Trim();
        }

        public char GetChar(int position)
        {
            return line[position];
        }

        public int GetInt(int start, int length)
        {
            string str = GetString(start, length);
            return string.IsNullOrEmpty(str) ? 0 : int.Parse(str, this.Culture);
        }

        public long GetLong(int start, int length)
        {
            string str = GetString(start, length);
            return string.IsNullOrEmpty(str) ? 0L : long.Parse(str, this.Culture);
        }

        public DateTime GetDateYmd(int start, int length)
        {
            string str = GetString(start, length);
            if (string.IsNullOrEmpty(str))
                return DateTime.MinValue;

            int y = int.Parse(str.Substring(0, 4));
            int m = int.Parse(str.Substring(4, 2));
            int d = int.Parse(str.Substring(6, 2));

            return y > 0 && m > 0 && d > 0 ?
                new DateTime(y, m, d) :
                DateTime.MinValue;
        }

        public decimal GetDecimal(int start, int length)
        {
            string str = GetString(start, length);
            if (string.IsNullOrEmpty(str))
                return 0M;
            else
                return decimal.Parse(str, Culture);
        }

    }
}
