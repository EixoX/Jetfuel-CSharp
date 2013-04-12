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
    }
}
