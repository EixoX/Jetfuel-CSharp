using System;
using System.Collections.Generic;
using System.Text;
/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX
{
    /// <summary>
    /// Rerepsents an aspect member.
    /// </summary>
    public class AspectMember : ClassAcessor
    {
        private readonly ClassAcessor _Acessor;

        /// <summary>
        /// Constructs the aspect member.
        /// </summary>
        /// <param name="acessor">The class acessor to use.</param>
        public AspectMember(ClassAcessor acessor)
        {
            this._Acessor = acessor;
        }

        /// <summary>
        /// Gets the data type of the member.
        /// </summary>
        public Type DataType
        {
            get { return _Acessor.DataType; }
        }

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        public string Name
        {
            get { return _Acessor.Name; }
        }

        /// <summary>
        /// Gets the member info of the member.
        /// </summary>
        public System.Reflection.MemberInfo MemberInfo
        {
            get { return _Acessor.MemberInfo; }
        }

        /// <summary>
        /// Gets a value from an entity.
        /// </summary>
        /// <param name="entity">The entity to read the value from.</param>
        /// <returns>The value read.</returns>
        public virtual object GetValue(object entity)
        {
            return _Acessor.GetValue(entity);
        }

        /// <summary>
        /// Sets a value to a member.
        /// </summary>
        /// <param name="entity">The entity to set value on.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        public virtual void SetValue(object entity, object value, IFormatProvider formatProvider)
        {

            if (value != null)
            {
                if (!_Acessor.DataType.IsEnum)
                {
                    if (value.GetType() != _Acessor.DataType)
                    {
                        if (PrimitiveTypes.String == _Acessor.DataType)
                        {
                            value = string.Format(formatProvider, "{0}", value);
                        }
                        else if (value is string && ((string)value).Length == 0)
                        {
                            value = null;
                        }
                        else
                        {
                            try
                            {
                                value = Convert.ChangeType(value, _Acessor.DataType, formatProvider);
                            }
                            catch (Exception e)
                            {
                                throw new FormatException("\"" + value + "\":" + e.Message, e);
                            }
                        }
                    }
                }

            }

            _Acessor.SetValue(entity, value);
        }


        /// <summary>
        /// Sets a value to a member.
        /// </summary>
        /// <param name="entity">The entity to set value on.</param>
        /// <param name="value">The value to set.</param>
        public virtual void SetValue(object entity, object value)
        {
            SetValue(entity, value, System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Parses and sets a text value to a member
        /// </summary>
        /// <param name="entity">The entity to set the value on.</param>
        /// <param name="textValue">The value to set.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        public virtual void SetParsedValue(object entity, string textValue, IFormatProvider formatProvider)
        {
            object memberValue;

            if (_Acessor.DataType == PrimitiveTypes.String)
            {
                memberValue = textValue;
            }
            else if (_Acessor.DataType.IsEnum)
            {
                memberValue = string.IsNullOrEmpty(textValue) ? null :
                    Enum.Parse(_Acessor.DataType, textValue);
            }
            else if (_Acessor.DataType == PrimitiveTypes.DateTime)
            {
                memberValue =
                    string.IsNullOrEmpty(textValue) ? DateTime.MinValue :
                    DateTime.Parse(textValue, formatProvider);
            }
            else if (_Acessor.DataType == PrimitiveTypes.TimeSpan)
            {

                memberValue = string.IsNullOrEmpty(textValue) ? TimeSpan.Zero :
                    TimeSpan.Parse(textValue);
            }
            else if (_Acessor.DataType == PrimitiveTypes.Guid)
            {
                memberValue = string.IsNullOrEmpty(textValue) ? Guid.Empty :
                    new Guid(textValue);
            }
            else
            {
                memberValue = string.IsNullOrEmpty(textValue) ? null :
                    Convert.ChangeType(textValue, _Acessor.DataType, formatProvider);
            }

            _Acessor.SetValue(entity, memberValue);
        }

        public string GetTextValue(object entity, IFormatProvider formatProvider, string formatString)
        {
            object memberValue = _Acessor.GetValue(entity);

            if (memberValue == null)
                return null;

            else if (_Acessor.DataType == PrimitiveTypes.String)
            {
                return (string)memberValue;
            }
            else if (_Acessor.DataType.IsEnum)
            {
                return string.Format(formatString, (int)memberValue);
            }
            else if (_Acessor.DataType == PrimitiveTypes.DateTime)
            {
                return ((DateTime)memberValue).ToString(formatString, formatProvider);
            }
            else if (_Acessor.DataType == PrimitiveTypes.TimeSpan)
            {
                return memberValue.ToString();
            }
            else if (_Acessor.DataType == PrimitiveTypes.Guid)
            {
                return ((Guid)memberValue) == Guid.Empty ? null : memberValue.ToString();
            }
            else
            {
                return string.Format(
                    formatProvider,
                    formatString,
                    memberValue);
            }
        }

        public virtual string GetTextValue(object entity, IFormatProvider formatProvider)
        {
            return GetTextValue(entity, formatProvider, "{0}");
        }

        /// <summary>
        /// Gets a string representation of the member.
        /// </summary>
        /// <returns>Usually the name of the member.</returns>
        public override string ToString()
        {
            return _Acessor.Name;
        }/// <summary>
        /// Gets custom attributes for the field.
        /// </summary>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public object[] GetCustomAttributes(bool inherit)
        {
            return _Acessor.GetCustomAttributes(inherit);
        }

        /// <summary>
        /// Gets custom attributes for the field.
        /// </summary>
        /// <param name="attributeType">The type of attribute to get.</param>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>An array of attributes.</returns>
        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return _Acessor.GetCustomAttributes(attributeType, inherit);
        }

        /// <summary>
        /// Checks if an attribute is defined.
        /// </summary>
        /// <param name="attributeType">The type of attribute to check.</param>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>True if defined.</returns>
        public bool IsDefined(Type attributeType, bool inherit)
        {
            return _Acessor.IsDefined(attributeType, inherit);
        }

        /// <summary>
        /// Gets an attribute.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to get.</typeparam>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>The attribute or default.</returns>
        public TAttribute GetAttribute<TAttribute>(bool inherit)
        {
            return _Acessor.GetAttribute<TAttribute>(inherit);
        }
        /// <summary>
        /// Gets typed attributes.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to get.</typeparam>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>An array of typed attributes.</returns>
        public TAttribute[] GetAttributes<TAttribute>(bool inherit)
        {
            return _Acessor.GetAttributes<TAttribute>(inherit);
        }
        /// <summary>
        /// Indicates that an attribute exists.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attributes.</typeparam>
        /// <param name="inherit">The </param>
        /// <returns></returns>
        public bool HasAttribute<TAttribute>(bool inherit)
        {
            return _Acessor.HasAttribute<TAttribute>(inherit);
        }

        public int GetInt32(object entity)
        {
            return Convert.ToInt32(_Acessor.GetValue(entity));
        }

        public double GetDouble(object entity)
        {
            return Convert.ToDouble(_Acessor.GetValue(entity));
        }

        public decimal GetDecimal(object entity)
        {
            return Convert.ToDecimal(_Acessor.GetValue(entity));
        }

        public long GetInt64(object entity)
        {
            return Convert.ToInt64(_Acessor.GetValue(entity));
        }

        public short GetInt16(object entity)
        {
            return Convert.ToInt16(_Acessor.GetValue(entity));
        }

        public byte GetByte(object entity)
        {
            return Convert.ToByte(_Acessor.GetValue(entity));
        }

        public char GetChar(object entity)
        {
            return Convert.ToChar(_Acessor.GetValue(entity));
        }

        public string GetString(object entity)
        {
            return Convert.ToString(_Acessor.GetValue(entity));
        }

        public ushort GetUInt16(object entity)
        {
            return Convert.ToUInt16(_Acessor.GetValue(entity));
        }

        public uint GetUInt32(object entity)
        {
            return Convert.ToUInt32(_Acessor.GetValue(entity));
        }

        public ulong GetUInt64(object entity)
        {
            return Convert.ToUInt64(_Acessor.GetValue(entity));
        }


        public bool CanRead
        {
            get { return this._Acessor.CanRead; }
        }

        public bool CanWrite
        {
            get { return this._Acessor.CanWrite; }
        }
    }
}
