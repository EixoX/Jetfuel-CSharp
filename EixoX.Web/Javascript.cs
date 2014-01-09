using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace EixoX.Web
{
    /// <summary>
    /// Represents a javascript formatter helper.
    /// </summary>
    public static class Javascript
    {

        public static string Format(bool value){
            return value ? "true": "false";
        }

        public static string Format(byte value)
        {
            return value.ToString();
        }

        public static string Format(char value)
        {
            return value == '"' ?
                "\"\\\"\"" :
                string.Concat("\"", value, "\"");
        }

        public static string Format(short value)
        {
            return value.ToString();
        }

        public static string Format(int value)
        {
            return value.ToString();
        }

        public static string Format(long value)
        {
            return value.ToString();
        }

        public static string Format(float value)
        {
            return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        public static string Format(double value)
        {
            return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        public static string Format(decimal value)
        {
            return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        public static string Format(string value)
        {
            return string.IsNullOrEmpty(value) ? "\"\"" :
                string.Concat("\"", value.Replace("\"", "\\\""), "\"");
        }

        public static string Format(System.Collections.IEnumerable collection)
        {
            StringBuilder builder = new StringBuilder(255);
            builder.Append('[');
            bool prependComma = false;
            foreach (object o in collection)
            {
                if (prependComma)
                    builder.Append(", ");
                else
                    prependComma = true;

                builder.Append(Format(o));
            }
            builder.Append(']');
            return builder.ToString();

        }


        public static string Format(object value)
        {
            if (value == null)
                return "null";
            else
            {
                Type tp = value.GetType();
                if (tp == PrimitiveTypes.Boolean)
                {
                    return Format((bool)value);
                }
                else if (tp == PrimitiveTypes.Byte)
                {
                    return Format((byte)value);
                }
                else if (tp == PrimitiveTypes.Char)
                {
                    return Format((char)value);
                }
                else if (tp == PrimitiveTypes.Decimal)
                {
                    return Format((decimal)value);
                }
                else if (tp == PrimitiveTypes.Double)
                {
                    return Format((double)value);
                }
                else if (tp == PrimitiveTypes.Float)
                {
                    return Format((float)value);
                }
                else if (tp == PrimitiveTypes.Int)
                {
                    return Format((int)value);
                }
                else if (tp == PrimitiveTypes.Long)
                {
                    return Format((long)value);
                }
                else if (tp == PrimitiveTypes.Short)
                {
                    return Format((short)value);
                }
                else if (tp == PrimitiveTypes.String)
                {
                    return Format((string)value);
                }
                else if (tp == PrimitiveTypes.UInt)
                {
                    return Format((int)value);
                }
                else if (tp == PrimitiveTypes.ULong)
                {
                    return Format((long)value);
                }
                else if (tp.IsArray)
                {
                    return Format((Array)value);
                }
                else if (PrimitiveTypes.IEnumerable.IsAssignableFrom(tp))
                {
                    return Format((System.Collections.IEnumerable)value);
                }
                else
                {
                    JavascriptBuilder builder = new JavascriptBuilder();
                    builder.Append(value);
                    return builder.ToString();
                }
            }
        }

        public static string Format<T>(T entity, params string[] names)
        {
            JavascriptBuilder jbuilder = new JavascriptBuilder(255);
            jbuilder.Append<T>(entity, names);
            return jbuilder.ToString();
        }
    }
}
