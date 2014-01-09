using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace EixoX.Web
{
    public class JavascriptBuilder
    {
        private readonly StringBuilder _Builder;

        public override string ToString()
        {
            return _Builder.ToString();
        }

        public override bool Equals(object obj)
        {
            return _Builder.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _Builder.GetHashCode();
        }

        public JavascriptBuilder()
        {
            this._Builder = new StringBuilder();
        }
        public JavascriptBuilder(int capacity)
        {
            this._Builder = new StringBuilder();
        }


        public void AppendRaw(string text)
        {
            _Builder.Append(text);
        }

        public void AppendLine()
        {
            _Builder.AppendLine();
        }

        public void AppendLine(string value)
        {
            _Builder.AppendLine(value);
        }

        public void Append(bool value)
        {
            _Builder.Append(value ? "true" : "false");
        }

        public void Append(byte value)
        {
            _Builder.Append(value);
        }

        public void Append(char value)
        {
            if (value == '"')
                _Builder.Append("\"\\\"\"");
            else
            {
                _Builder.Append('"');
                _Builder.Append(value);
                _Builder.Append('"');
            }
        }

        public void Append(short value)
        {
            _Builder.Append(value);
        }

        public void Append(int value)
        {
            _Builder.Append(value);
        }

        public void Append(long value)
        {
            _Builder.Append(value);
        }

        public void Append(float value)
        {
            _Builder.Append(value.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        public void Append(double value)
        {
            _Builder.Append(value.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        public void Append(decimal value)
        {
            _Builder.Append(value.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        public void Append(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                _Builder.Append("\"\"");
            }
            else
            {
                _Builder.Append('"');
                _Builder.Append(value.Replace("\"", "\\\""));
                _Builder.Append('"');
            }
        }

        public void Append(object value)
        {
            if (value == null)
                _Builder.Append("null");
            else
            {
                Type tp = value.GetType();
                if (tp == PrimitiveTypes.Boolean)
                {
                    Append((bool)value);
                }
                else if (tp == PrimitiveTypes.Byte)
                {
                    Append((byte)value);
                }
                else if (tp == PrimitiveTypes.Char)
                {
                    Append((char)value);
                }
                else if (tp == PrimitiveTypes.Decimal)
                {
                    Append((decimal)value);
                }
                else if (tp == PrimitiveTypes.Double)
                {
                    Append((double)value);
                }
                else if (tp == PrimitiveTypes.Float)
                {
                    Append((float)value);
                }
                else if (tp == PrimitiveTypes.Int)
                {
                    Append((int)value);
                }
                else if (tp == PrimitiveTypes.Long)
                {
                    Append((long)value);
                }
                else if (tp == PrimitiveTypes.Short)
                {
                    Append((short)value);
                }
                else if (tp == PrimitiveTypes.String)
                {
                    Append((string)value);
                }
                else if (tp == PrimitiveTypes.UInt)
                {
                    Append((uint)value);
                }
                else if (tp == PrimitiveTypes.ULong)
                {
                    Append((ulong)value);
                }
                else if (tp.IsArray)
                {
                    Append((Array)value);
                }
                else if (PrimitiveTypes.IEnumerable.IsAssignableFrom(tp))
                {
                    Append((System.Collections.IEnumerable)value);
                }
                else
                {
                    AppendObject(value);
                }
            }

        }

        public void Append(System.Collections.IEnumerable collection)
        {
            _Builder.Append('[');
            bool prependComma = false;

            foreach (object o in collection)
            {
                if (prependComma)
                    _Builder.Append(", ");
                else
                    prependComma = true;

                Append(o);
            }

            _Builder.Append(']');
        }

        public void Append(Array array)
        {
            _Builder.Append('[');
            Append(array.GetValue(0));

            int count = array.GetLength(0);

            for (int i = 1; i < count; i++)
            {
                _Builder.Append(", ");
                Append(array.GetValue(i));
            }

            _Builder.Append(']');
        }

        public void AppendPair(string name, object value)
        {
            _Builder.Append(name);
            _Builder.Append(": ");
            Append(value);
        }

        public void Append<T>(T entity, params string[] names)
        {
            AspectMember[] members =
                names != null && names.Length > 0 ?
                ClassSchema<T>.Instance.GetMemberArray(names) :
                ClassSchema<T>.Instance.GetMemberArray();


            _Builder.AppendLine("{");
            _Builder.Append(members[0].Name);
            _Builder.Append(": ");
            Append(members[0].GetValue(entity));

            for (int i = 1; i < members.Length; i++)
            {
                _Builder.AppendLine(",");
                _Builder.Append(members[i].Name);
                _Builder.Append(": ");
                Append(members[i].GetValue(entity));
            }

            _Builder.AppendLine();
            _Builder.Append('}');

        }

        private void AppendObject(object value)
        {
            _Builder.AppendLine("{");
            

            Type type = value.GetType();
            FieldInfo[] fields = type.GetFields();

            if (fields != null && fields.Length > 0)
            {
                _Builder.Append(fields[0].Name);
                _Builder.Append(": ");
                Append(fields[0].GetValue(value));

                for (int i = 1; i < fields.Length; i++)
                {
                    _Builder.AppendLine(",");
                    _Builder.Append(fields[i].Name);
                    _Builder.Append(": ");
                    Append(fields[i].GetValue(value));
                }
            }

            PropertyInfo[] properties = type.GetProperties();

            if (properties != null && properties.Length > 0)
            {
                if (fields != null && fields.Length > 0)
                    _Builder.AppendLine(", ");

                _Builder.Append(properties[0].Name);
                _Builder.Append(": ");
                Append(properties[0].GetValue(value, null));

                for (int i = 1; i < properties.Length; i++)
                {
                    _Builder.AppendLine(",");
                    _Builder.Append(properties[i].Name);
                    _Builder.Append(": ");
                    Append(properties[i].GetValue(value, null));
                }
            }

            _Builder.AppendLine();
            _Builder.Append('}');
        }


    }
}
