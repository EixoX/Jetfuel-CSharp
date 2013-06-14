using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX
{
    /// <summary>
    /// The basic field acessor.
    /// </summary>
    public struct ClassField : ClassAcessor
    {
        private readonly FieldInfo _Field;

        /// <summary>
        /// Construct as class field object.
        /// </summary>
        /// <param name="field">The class field to wrap.</param>
        public ClassField(FieldInfo field)
        {
            this._Field = field;
        }

        /// <summary>
        /// Gets the datat type of the field.
        /// </summary>
        public Type DataType
        {
            get { return this._Field.FieldType; }
        }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        public string Name
        {
            get { return this._Field.Name; }
        }

        /// <summary>
        /// Gets the member info for the field.
        /// </summary>
        public MemberInfo MemberInfo
        {
            get { return this._Field; }
        }

        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <param name="entity">The entity to read from.</param>
        /// <returns>The field value.</returns>
        public object GetValue(object entity)
        {
            return this._Field.GetValue(entity);
        }

        /// <summary>
        /// Sets a value of a field.
        /// </summary>
        /// <param name="entity">The entity to write to.</param>
        /// <param name="value">The field value.</param>
        public void SetValue(object entity, object value)
        {
            this._Field.SetValue(entity, value);
        }

        /// <summary>
        /// Gets custom attributes for the field.
        /// </summary>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public object[] GetCustomAttributes(bool inherit)
        {
            return _Field.GetCustomAttributes(inherit);
        }

        /// <summary>
        /// Gets custom attributes for the field.
        /// </summary>
        /// <param name="attributeType">The type of attribute to get.</param>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>An array of attributes.</returns>
        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return _Field.GetCustomAttributes(attributeType, inherit);
        }

        /// <summary>
        /// Checks if an attribute is defined.
        /// </summary>
        /// <param name="attributeType">The type of attribute to check.</param>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>True if defined.</returns>
        public bool IsDefined(Type attributeType, bool inherit)
        {
            return _Field.IsDefined(attributeType, inherit);
        }

        /// <summary>
        /// Gets an attribute.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to get.</typeparam>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>The attribute or default.</returns>
        public TAttribute GetAttribute<TAttribute>(bool inherit)
        {
            object[] arr = _Field.GetCustomAttributes(typeof(TAttribute), inherit);
            return arr != null && arr.Length > 0 ?
                (TAttribute)arr[0] :
                default(TAttribute);
        }
        /// <summary>
        /// Gets typed attributes.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to get.</typeparam>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>An array of typed attributes.</returns>
        public TAttribute[] GetAttributes<TAttribute>(bool inherit)
        {
            object[] arr = _Field.GetCustomAttributes(typeof(TAttribute), inherit);
            TAttribute[] atts = new TAttribute[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                atts[i] = (TAttribute)arr[i];
            return atts;
        }
        /// <summary>
        /// Indicates that an attribute exists.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attributes.</typeparam>
        /// <param name="inherit">The </param>
        /// <returns></returns>
        public bool HasAttribute<TAttribute>(bool inherit)
        {
            return _Field.IsDefined(typeof(TAttribute), inherit);
        }



        public void SetValue(object entity, object value, IFormatProvider formatProvider)
        {
            _Field.SetValue(entity, Convert.ChangeType(value, _Field.FieldType, formatProvider));
        }


        public bool CanRead
        {
            get { return true; }
        }

        public bool CanWrite
        {
            get { return this._Field.IsLiteral == false && this._Field.IsInitOnly == false; }
        }
    }
}
