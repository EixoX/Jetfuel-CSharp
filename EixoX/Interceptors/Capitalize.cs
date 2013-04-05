using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Interceptors
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class Capitalize : Attribute, Interceptor
    {

        public static string Intercept(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            char prev = Char.MinValue;
            int length = input.Length;
            char[] chars = new char[input.Length];

            for (int i = 0; i < length; i++)
            {
                if (char.IsLetter(input[i]))
                {
                    chars[i] = char.IsLetter(prev) ? char.ToLower(input[i]) : char.ToUpper(input[i]);
                }
                else
                    chars[i] = input[i];

                prev = input[i];
                
            }

            return new string(chars);
        }

        public object Intercept(object input)
        {
            return input == null ? null : Intercept(input.ToString());
        }
    }
}
