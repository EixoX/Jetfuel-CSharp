using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EixoX.Text
{
    public class DelimitedTextReader
        : IDataReader
    {
        private readonly System.IO.StreamReader reader;
        private readonly char[] splitter;
        private readonly string[] names;
        private readonly IFormatProvider formatProvider;
        public string line;
        public string[] cells;
        private bool closed;

        public DelimitedTextReader(System.IO.StreamReader reader, IFormatProvider formatProvider, params char[] splitter)
        {
            this.splitter = splitter;
            this.line = reader.ReadLine();
            this.reader = reader;
            this.names = this.line.Split(splitter);
            for (int i = 0; i < this.names.Length; i++)
                this.names[i] = this.names[i].Trim();
            this.formatProvider = formatProvider;
            this.closed = false;
        }

        public DelimitedTextReader(System.IO.Stream stream, Encoding encoding, IFormatProvider formatProvider, params char[] splitter)
            : this(new System.IO.StreamReader(stream, encoding), formatProvider, splitter) { }

        public void Close()
        {
            if (!this.closed)
            {
                this.closed = true;
                this.reader.Close();
            }
        }

        public int Depth
        {
            get { return 0; }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException("This class does not returns a schema table");
        }

        public bool IsClosed
        {
            get { return this.closed; }
        }

        public bool NextResult()
        {
            return false;
        }

        public bool Read()
        {
            if (closed)
                return false;

            this.line = reader.ReadLine();
            if (line == null)
            {
                return false;
            }
            else if (line.Trim().Length < 1)
            {
                return Read();
            }
            else
            {
                this.cells = line.Split(splitter);
                return true;
            }
        }

        public int RecordsAffected
        {
            get { return 0; }
        }

        public void Dispose()
        {
            Close();
            this.reader.Dispose();
        }

        public int FieldCount
        {
            get { return this.names.Length; }
        }

        public bool GetBoolean(int i)
        {
            return Convert.ToBoolean(GetString(i), this.formatProvider);
        }

        public byte GetByte(int i)
        {
            return Convert.ToByte(GetString(i), this.formatProvider);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return this.reader.CurrentEncoding.GetBytes(GetString(i), (int)fieldOffset, length, buffer, bufferoffset);
        }

        public char GetChar(int i)
        {
            string s = GetString(i);
            return string.IsNullOrEmpty(s) ? Char.MinValue : s[0];
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            string s = GetString(i);

            if (string.IsNullOrEmpty(s))
                return -1;

            length = Math.Min(s.Length - (int)fieldoffset, length);
            for (int j = 0; j < length; j++)
            {
                buffer[j] = s[(int)fieldoffset + j];
            }
            return length;

        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            return "String";
        }

        public DateTime GetDateTime(int i)
        {
            return Convert.ToDateTime(GetString(i), this.formatProvider);
        }

        public decimal GetDecimal(int i)
        {
            return Convert.ToDecimal(GetString(i), this.formatProvider);
        }

        public double GetDouble(int i)
        {
            return Convert.ToDouble(GetString(i), this.formatProvider);
        }

        public Type GetFieldType(int i)
        {
            return typeof(String);
        }

        public float GetFloat(int i)
        {
            return Convert.ToSingle(GetString(i), this.formatProvider);
        }

        public Guid GetGuid(int i)
        {
            string s = GetString(i);
            return string.IsNullOrEmpty(s) ? Guid.Empty : new Guid(s);
        }

        public short GetInt16(int i)
        {
            return Convert.ToInt16(GetString(i), this.formatProvider);
        }

        public int GetInt32(int i)
        {
            return Convert.ToInt32(GetString(i), this.formatProvider);
        }

        public long GetInt64(int i)
        {
            return Convert.ToInt64(GetString(i), this.formatProvider);
        }

        public string GetName(int i)
        {
            return this.names[i];
        }

        public int GetOrdinal(string name)
        {
            for (int i = 0; i < names.Length; i++)
                if (name.Equals(names[i], StringComparison.OrdinalIgnoreCase))
                    return i;

            return -1;
        }

        public string GetString(int i)
        {
            if (i < 0 || i >= cells.Length)
                return null;
            else
                return string.IsNullOrEmpty(cells[i]) ? null : cells[i];
        }

        public object GetValue(int i)
        {
            return GetString(i);
        }

        public int GetValues(object[] values)
        {
            int imax = values.Length > cells.Length ? cells.Length : values.Length;
            for (int i = 0; i < imax; i++)
                values[i] = cells[i];
            return imax;
        }

        public bool IsDBNull(int i)
        {
            return string.IsNullOrEmpty(cells[i]);
        }

        public object this[string name]
        {
            get
            {
                int o = GetOrdinal(name);
                if (o < 0)
                    throw new ArgumentOutOfRangeException("name", name + " is not a valid column header");
                else
                    return GetString(o);
            }
        }

        public object this[int i]
        {
            get { return GetString(i); }
        }
    }
}
