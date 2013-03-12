using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public static class ValidationHelper
    {
        public static bool IsNullOrEmpty(object value)
        {
            if (value == null)
                return true;
            if (value is string && string.IsNullOrEmpty((string)value))
                return true;
            if (value is int && ((int)value) == 0)
                return true;
            if (value is long && ((long)value) == 0L)
                return true;
            if (value is short && ((short)value) == 0)
                return true;
            if (value is uint && ((uint)value) == 0)
                return true;
            if (value is ulong && ((ulong)value) == 0L)
                return true;
            if (value is ushort && ((ushort)value) == 0)
                return true;
            if (value is byte && ((byte)value) == 0)
                return true;
            if (value is DateTime && ((DateTime)value) == DateTime.MinValue)
                return true;
            if (value is float && ((float)value) == 0f)
                return true;
            if (value is double && ((double)value) == 0.0)
                return true;
            if (value is decimal && ((decimal)value) == 0M)
                return true;
            if (value is TimeSpan && ((TimeSpan)value) == TimeSpan.Zero)
                return true;
            if (value is Guid && ((Guid)value) == Guid.Empty)
                return true;
            if (value is Array && ((Array)value).Length == 0)
                return true;

            return false;
        }

        public static bool IsNumericDiscrete(object value)
        {
            return
                value is int || value is long || value is short || value is byte ||
                value is uint || value is ulong || value is ushort || value is sbyte;
        }

        public static bool IsNumericFloatingPoint(object value)
        {
            return value is float || value is double || value is decimal;
        }

        public static bool IsNumeric(object value)
        {
            return IsNumericDiscrete(value) || IsNumericFloatingPoint(value);
        }

        public static bool IsEmail(string value)
        {
            return EixoX.Restrictions.Email.IsEmail(value);
        }
        
        
    }
}
