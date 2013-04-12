using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using EixoX.Globalization;
using EixoX.Restrictions;
using EixoX.Interceptors;

namespace EixoX.UI
{
    /// <summary>
    /// Represents an user interface aspect.
    /// </summary>
    public class UIAspect
        : SingleAnnotationAspect<UIControlAttribute>
    {
        /// <summary>
        /// Constructs a user interface aspect.
        /// </summary>
        /// <param name="dataType">The entity data type.</param>
        public UIAspect(Type dataType) : base(dataType) { }
    }

    /// <summary>
    /// Represents an user interface aspect.
    /// </summary>
    public class UIAspect<T>
        : UIAspect
    {
        private static UIAspect<T> _Instance;
        private UIAspect() : base(typeof(T)) { }

        /// <summary>
        /// Gets the only intance of the typed ui aspect.
        /// </summary>
        public static UIAspect<T> Instance
        {
            get { return _Instance ?? (_Instance = new UIAspect<T>()); }
        }
    }
}
