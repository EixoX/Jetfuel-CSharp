using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Interceptors
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class Whitespace
        : Attribute, Interceptor
    {
        public static string Collapse(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            input = Replace(input);
            while (input.Contains("  "))
                input = input.Replace("  ", " ");
            
            return input.Trim();
        }

        public static string Replace(string input)
        {
            return input == null ? null : input.Replace('\r', ' ').Replace('\n', ' ').Replace('\t', ' ');
        }

        public static string Intercept(string input, WhitespaceStrategy strategy)
        {
            switch (strategy)
            {
                case WhitespaceStrategy.Collapse:
                    return Collapse(input);
                case WhitespaceStrategy.Replace:
                    return Replace(input);
                default:
                    throw new ArgumentOutOfRangeException("strategy", "Unknown whitespace strategy: " + strategy);
            }
        }

        private readonly WhitespaceStrategy _Strategy;
        public WhitespaceStrategy Strategy { get { return this._Strategy; } }

        public Whitespace(WhitespaceStrategy strategy)
        {
            this._Strategy = strategy;
        }

        public object Intercept(object input)
        {
            return input == null ? null : Intercept(input.ToString(), _Strategy);
        }
    }
}
