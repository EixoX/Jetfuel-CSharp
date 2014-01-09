using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Helpers
{
    public static class CharHelper
    {

        public static void Reverse(char[] array, int offset, int length)
        {
            char tmp;
            int current, other;
            for (int i = 0; i < length; i++)
            {
                current = offset + i;
                other = length + offset - i - 1;

                tmp = array[current];
                array[current] = array[other];
                array[other] = tmp;
            }
        }
    }
}
