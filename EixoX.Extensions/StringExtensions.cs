using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class StringExtensions
    {

        public static string Left(this string input, int length)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            else if (length > input.Length)
                return input;
            else
                return input.Substring(0, length);
        }
    }
}
