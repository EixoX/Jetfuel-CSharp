using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public struct Base36
    {
        public long Value;
        public static string Chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        public Base36(long value)
        {
            this.Value = value;
        }

        public static Base36 operator + (Base36 left, Base36 right)
        {
            return new Base36(left.Value + right.Value);
        }

        public static bool operator ==(Base36 left, long right)
        {
            return left.Value == right;
        }

        public static bool operator ==(long left, Base36 right)
        {
            return left == right.Value;
        }

        public static bool operator ==(Base36 left, Base36 right)
        {
            return left.Value == right.Value;
        }

        public static bool operator !=(Base36 left, long right)
        {
            return left.Value != right;
        }

        public static bool operator !=(long left, Base36 right)
        {
            return left != right.Value;
        }

        public static bool operator !=(Base36 left, Base36 right)
        {
            return left.Value != right.Value;
        }

        public static Base36 Parse(string input)
        {
            return new Base36(ToDecimalRecursive(input));
        }

        private static long ParseRecursive(string input, int power, int charCounter)
        {
            char currentCharacter = input[charCounter];
            long powered = (long)Math.Pow(36, power) * Chars.IndexOf(currentCharacter);

            return charCounter == 0 ? powered : powered + ParseRecursive(input, power + 1, charCounter - 1);
        }

        public static long ToDecimalRecursive(string base36input)
        {
            return ParseRecursive(base36input, 0, base36input.Length - 1);
        }

        public static long ToDecimal(string input)
        {
            long converted = 0;
            int k = 0;

            for (int i = input.Length - 1; i >= 0; i--)
                converted += (long)Math.Pow(36, k++) * Chars.IndexOf(input[i]);
            
            return converted;
        }

        public override bool Equals(object obj)
        {
            if (obj is long)
            {
                return this.Value == (long)obj;
            }
            else if (obj is Base36)
            {
                return this.Value == ((Base36)obj).Value;
            }
            
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            Stack<long> rests = new Stack<long>();

            long result = this.Value;
            long mod = 0;

            while (result > 0)
            {
                mod = result % 36;
                rests.Push(mod);
                result /= 36;
            }

            StringBuilder sb = new StringBuilder();

            do
            {
                sb.Append(Chars[(int)rests.Pop()]);
            } while (rests.Count > 0);

            return sb.ToString();
        }
    }
}
