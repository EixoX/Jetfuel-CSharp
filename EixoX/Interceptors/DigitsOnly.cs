using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Interceptors
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DigitsOnly : Attribute, Interceptor
    {
        public static string Intercept(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            StringBuilder builder = new StringBuilder(input.Length);
            for (int i = 0; i < input.Length; i++)
                if (char.IsDigit(input, i))
                    builder.Append(input[i]);

            return builder.ToString();
        }

        public object Intercept(object input)
        {
            return input == null ? null : Intercept(input.ToString());
        }
    }
}
