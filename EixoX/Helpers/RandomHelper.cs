using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Helpers
{
    public static class RandomHelper
    {
        private readonly static Random theRandom = new Random();

        public static void PutDigits(char[] array, int offset, int length)
        {
            for (int i = 0; i < length; i++)
                array[offset + i] = (char)theRandom.Next(48, 58);
        }


        public static void PutDigitsButNotLast(char[] array, int offset)
        {
            PutDigits(array, offset, array.Length - offset - 1);
        }

        public static int Next(int minInclusive, int maxExclusive)
        {
            return theRandom.Next(minInclusive, maxExclusive);
        }

        public static string RandomLetters(int size)
        {
            char[] arr = new char[size];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (char)theRandom.Next(97, 123);
            }
            return new string(arr);
        }

        public static string RandomLetterOrDigits(int size)
        {
            char[] arr = new char[size];
            for (int i = 0; i < arr.Length; i++)
            {
                int r = theRandom.Next(97, 133);
                arr[i] = r < 123 ? (char)r : (char)(r - 75);
            }
            return new string(arr);
        }
    }
}
