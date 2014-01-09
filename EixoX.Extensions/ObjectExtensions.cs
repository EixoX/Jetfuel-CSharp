using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ObjectExtensions
    {
        public static string EmptyOrValue(this object obj)
        {
            return obj == null ? String.Empty : obj.ToString();
        }

        public static bool EqualsIfNotNull(this object obj, string key, StringComparison comparison)
        {
            return obj == null ? false : obj.ToString().Equals(key, comparison);
        }
    }
}
