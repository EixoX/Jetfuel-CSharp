using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EixoX.Text
{
    public class FixedLengthTextWriter
        : IDisposable
    {
        public readonly TextWriter writer;
        public readonly char[] buffer;
        public char defaultChar = ' ';

        public FixedLengthTextWriter(TextWriter writer, int lineSize)
        {
            this.writer = writer;
            this.buffer = new char[lineSize];
        }
        public FixedLengthTextWriter(Stream output, Encoding encoding, int lineSize)
            : this(new StreamWriter(output, encoding), lineSize)
        { }

        public FixedLengthTextWriter(string fileName, bool append, Encoding encoding, int lineSize)
            : this(new StreamWriter(fileName, append, encoding), lineSize)
        { }

        public void Dispose()
        {
            this.writer.Dispose();
        }

        public void Close()
        {
            this.writer.Close();
        }

        public void ClearRow()
        {
            int l = this.buffer.Length;
            for (int i = 0; i < l; i++)
                this.buffer[i] = defaultChar;
        }

        public void Write()
        {
            this.writer.WriteLine(this.buffer);
            ClearRow();
        }

        public void PutLTR(string str, int position, int length)
        {
            this.PutLTR(str, position, length, this.defaultChar);
        }

        public void PutLTR(string str, int position, int length, char padChar)
        {
            int lmax = string.IsNullOrEmpty(str) ? 0 :
                (str.Length > length ? length : str.Length);

            for (int i = 0; i < lmax; i++)
                this.buffer[position + i] = str[i];
            for (int i = lmax; i < length; i++)
                this.buffer[position + i] = padChar;
        }

        public void PutRTL(string str, int position, int length)
        {
            this.PutRTL(str, position, length, this.defaultChar);
        }

        public void PutRTL(string str, int position, int length, char padChar)
        {
            int clength = string.IsNullOrEmpty(str) ? 0 : (str.Length > length ? length : str.Length);
            int cstart = position + length - clength;
            for (int i = 0; i < clength; i++)
                this.buffer[cstart + i] = str[i];
            for (int i = cstart; i > position; i--)
                this.buffer[i] = padChar;
        }

        public void PutNumber(int value, int position, int length)
        {
            this.PutRTL(value.ToString(), position, length, '0');
        }

        public void PutNumber(long value, int position, int length)
        {
            this.PutRTL(value.ToString(), position, length, '0');
        }

        public void PutNumber(double value, int position, int length, int decimalDigits)
        {
            long integral = (long)value;
            this.PutRTL(integral.ToString(), position, length - decimalDigits, '0');
            long fraction = (long)((value - integral) * Math.Pow(10.0, decimalDigits));
            this.PutRTL(fraction.ToString(), position + (length - decimalDigits), decimalDigits);
        }

        public void PutNumber(decimal value, int position, int length, int decimalDigits)
        {
            long integral = (long)value;
            this.PutRTL(integral.ToString(), position, length - decimalDigits, '0');
            long fraction = (long)((value - integral) * new decimal(Math.Pow(10.0, decimalDigits)));
            this.PutRTL(fraction.ToString(), position + (length - decimalDigits), decimalDigits);
        }


        public void PutDate(DateTime dt, int position, int length)
        {
            switch (length)
            {
                case 6: //YYMMDD
                    PutNumber(dt.Year % 100, position, 2);
                    PutNumber(dt.Month, position + 2, 2);
                    PutNumber(dt.Day, position + 4, 2);
                    break;
                case 8: //YYYYMMDD
                    PutNumber(dt.Year, position, 4);
                    PutNumber(dt.Month, position + 4, 2);
                    PutNumber(dt.Day, position + 6, 2);
                    break;
                case 10: //YYMMDDHHMM
                    PutNumber(dt.Year % 100, position, 2);
                    PutNumber(dt.Month, position + 2, 2);
                    PutNumber(dt.Day, position + 4, 2);
                    PutNumber(dt.Hour, position + 6, 2);
                    PutNumber(dt.Minute, position + 8, 2);
                    break;
                case 12: //YYYYMMDDHHMM
                    PutNumber(dt.Year, position, 4);
                    PutNumber(dt.Month, position + 4, 2);
                    PutNumber(dt.Day, position + 6, 2);
                    PutNumber(dt.Hour, position + 8, 2);
                    PutNumber(dt.Minute, position + 10, 2);
                    break;
                case 14: //YYYYMMDDHHMMSS
                    PutNumber(dt.Year, position, 4);
                    PutNumber(dt.Month, position + 4, 2);
                    PutNumber(dt.Day, position + 6, 2);
                    PutNumber(dt.Hour, position + 8, 2);
                    PutNumber(dt.Minute, position + 10, 2);
                    PutNumber(dt.Second, position + 12, 2);
                    break;
                case 16: //YYYYMMDDHH:MM:SS
                    PutNumber(dt.Year, position, 4);
                    PutNumber(dt.Month, position + 4, 2);
                    PutNumber(dt.Day, position + 6, 2);
                    PutNumber(dt.Hour, position + 8, 2);
                    this.buffer[position + 10] = ':';
                    PutNumber(dt.Minute, position + 11, 2);
                    this.buffer[position + 13] = ':';
                    PutNumber(dt.Second, position + 14, 2);
                    break;
                default:
                    throw new NotImplementedException("Unknown string lenght to format date: " + dt + " [length=" + length + "]");
            }
        }

        public void PutDatePtBr(DateTime dt, int position, int length)
        {
            switch (length)
            {
                case 6: //DDMMAA
                    PutNumber(dt.Day, position + 4, 2);
                    PutNumber(dt.Month, position + 2, 2);
                    PutNumber(dt.Year % 100, position, 2);
                    break;
                case 8: //DDMMAAAA
                    PutNumber(dt.Day, position + 6, 2);
                    PutNumber(dt.Month, position + 4, 2);
                    PutNumber(dt.Year, position, 4);
                    break;
                case 10: //DDMMAAAAHHMM
                    PutNumber(dt.Day, position + 4, 2);
                    PutNumber(dt.Month, position + 2, 2);
                    PutNumber(dt.Year % 100, position, 2);
                    PutNumber(dt.Hour, position + 6, 2);
                    PutNumber(dt.Minute, position + 8, 2);
                    break;
                case 12: //DDMMAAAAHHMM
                    PutNumber(dt.Day, position + 6, 2);
                    PutNumber(dt.Month, position + 4, 2);
                    PutNumber(dt.Year, position, 4);
                    PutNumber(dt.Hour, position + 8, 2);
                    PutNumber(dt.Minute, position + 10, 2);
                    break;
                case 14: //DDMMAAAAHHMMSS
                    PutNumber(dt.Day, position + 6, 2);
                    PutNumber(dt.Month, position + 4, 2);
                    PutNumber(dt.Year, position, 4);
                    PutNumber(dt.Hour, position + 8, 2);
                    PutNumber(dt.Minute, position + 10, 2);
                    PutNumber(dt.Second, position + 12, 2);
                    break;
                case 16: //DDMMAAAA:MM:SS
                    PutNumber(dt.Day, position + 6, 2);
                    PutNumber(dt.Month, position + 4, 2);
                    PutNumber(dt.Year, position, 4);
                    PutNumber(dt.Hour, position + 8, 2);
                    this.buffer[position + 10] = ':';
                    PutNumber(dt.Minute, position + 11, 2);
                    this.buffer[position + 13] = ':';
                    PutNumber(dt.Second, position + 14, 2);
                    break;
                default:
                    throw new NotImplementedException("Unknown string lenght to format date: " + dt + " [length=" + length + "]");
            }
        }
    }
}
