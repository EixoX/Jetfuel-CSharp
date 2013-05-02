using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Adapters
{
    public class BinaryAdapter
        : SimpleAdapterBase<byte[]>
    {
        private readonly int _Maxlength;


        public BinaryAdapter(int maxLength)
        {
            this._Maxlength = maxLength;
        }

        public override System.Data.DbType DbType
        {
            get { return System.Data.DbType.Binary; }
        }

        public override System.Data.SqlDbType SqlDbType
        {
            get { return System.Data.SqlDbType.VarBinary; }
        }

        public override bool IsEmpty(byte[] input)
        {
            return input == null || input.Length == 0;
        }

        public override string FormatValue(byte[] input, string formatString, IFormatProvider formatProvider)
        {
            return ToHexString(input);
        }

        public override byte[] ParseValue(string input, IFormatProvider formatProvider)
        {
            return FromHexString(input);
        }

        public override string SqlMarshallValue(byte[] input, bool nullable)
        {
            return IsEmpty(input) ? "NULL" : ToHexString(input);
        }

        public override void SqlMarshallValue(StringBuilder builder, byte[] input, bool nullable)
        {
            builder.Append(IsEmpty(input) ? "NULL" : ToHexString(input));
        }

        public override byte[] BinaryReadValue(System.IO.BinaryReader reader)
        {
            return reader.ReadBytes(_Maxlength);
        }

        public override void BinaryWriteValue(System.IO.BinaryWriter writer, byte[] value)
        {
            writer.Write(value);
        }

        public static readonly string HexAlphabet = "0123456789ABCDEF";

        public static string ToHexString(byte[] input)
        {
            if (input == null || input.Length == 0)
                return null;

            StringBuilder builder = new StringBuilder(input.Length * 2 + 2);
            builder.Append("0x");
            for (int i = 0; i < input.Length; i++)
            {
                builder.Append(HexAlphabet[input[i] / 16]);
                builder.Append(HexAlphabet[input[i] % 16]);
            }
            return builder.ToString();
        }

        public static byte[] FromHexString(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 4)
                return null;

            if (input[0] != '0' || (input[1] != 'x' && input[1] != 'X'))
                throw new ArgumentException("Hex strings must start with 0x");

            byte[] arr = new byte[(input.Length - 2) / 2];
            for (int i = 2; i < input.Length; i+=2)
            {
                arr[i] = (byte)
                    (HexAlphabet.IndexOf(char.ToUpperInvariant(input[i])) * 16 +
                    (HexAlphabet.IndexOf(char.ToUpperInvariant(input[i + 1]))));
            }

            return arr;
        }
    }
}
