using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace EixoX
{
    /// <summary>
    /// The basic property acessor.
    /// </summary>
    public struct ClassProperty : ClassAcessor
    {
        private readonly PropertyInfo _Property;

        /// <summary>
        /// Construct as class property object.
        /// </summary>
        /// <param name="property">The class property to wrap.</param>
        public ClassProperty(PropertyInfo property)
        {
            this._Property = property;
        }

        /// <summary>
        /// Gets the datat type of the property.
        /// </summary>
        public Type DataType
        {
            get { return this._Property.PropertyType; }
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string Name
        {
            get { return this._Property.Name; }
        }

        /// <summary>
        /// Gets the member info for the property.
        /// </summary>
        public MemberInfo MemberInfo
        {
            get { return this._Property; }
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="entity">The entity to read from.</param>
        /// <returns>The property value.</returns>
        public object GetValue(object entity)
        {
            try
            {
                return this._Property.GetValue(entity, null);
            }
            catch (Exception ex)
            {
                throw new Exception(this.Name + " had a problem: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Sets a value of a property.
        /// </summary>
        /// <param name="entity">The entity to write to.</param>
        /// <param name="value">The property value.</param>
        public void SetValue(object entity, object value)
        {
            this._Property.SetValue(entity, value, null);
        }

        /// <summary>
        /// Gets custom attributes for the property.
        /// </summary>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public object[] GetCustomAttributes(bool inherit)
        {
            return _Property.GetCustomAttributes(inherit);
        }

        /// <summary>
        /// Gets custom attributes for the property.
        /// </summary>
        /// <param name="attributeType">The type of attribute to get.</param>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>An array of attributes.</returns>
        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return _Property.GetCustomAttributes(attributeType, inherit);
        }

        /// <summary>
        /// Checks if an attribute is defined.
        /// </summary>
        /// <param name="attributeType">The type of attribute to check.</param>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>True if defined.</returns>
        public bool IsDefined(Type attributeType, bool inherit)
        {
            return _Property.IsDefined(attributeType, inherit);
        }

        /// <summary>
        /// Gets an attribute.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to get.</typeparam>
        /// <param name="inherit">Indicates that it may be inherited.</param>
        /// <returns>The attribute or default.</returns>
        public TAttribute GetAttribute<TAttribute>(bool inherit)
        {
            object[] arr = _Property.GetCustomAttributes(typeof(TAttribute), inherit);
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
            object[] arr = _Property.GetCustomAttributes(typeof(TAttribute), inherit);
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
            return _Property.IsDefined(typeof(TAttribute), inherit);
        }


        public void SetValue(object entity, object value, IFormatProvider formatProvider)
        {
            _Property.SetValue(entity, Convert.ChangeType(value, _Property.PropertyType, formatProvider), null);
        }


        public bool CanRead
        {
            get { return this._Property.CanRead; }
        }

        public bool CanWrite
        {
            get { return this._Property.CanWrite; }
        }
    }
}
