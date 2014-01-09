using System;
using System.Collections.Generic;
using System.Text;
 /*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX
{
    /// <summary>
    /// Encapsulates aspect member values.
    /// </summary>
    public struct AspectMemberValue
    {
        private readonly Aspect _Aspect;
        private readonly int _Ordinal;
        private readonly object _Value;

        /// <summary>
        /// Constructs an aspect member value.
        /// </summary>
        /// <param name="aspect">The aspect of the class.</param>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value of the member.</param>
        public AspectMemberValue(Aspect aspect, int ordinal, object value)
        {
            this._Aspect = aspect;
            this._Ordinal = ordinal;
            this._Value = value;
        }

        /// <summary>
        /// Constructs an aspect member value.
        /// </summary>
        /// <param name="aspect">The aspect of the class.</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value of the member.</param>
        public AspectMemberValue(Aspect aspect, string name, object value)
            : this(aspect, aspect.GetOrdinalOrException(name), value) { }


        /// <summary>
        /// Gets the aspect of the class.
        /// </summary>
        public Aspect Aspect { get { return this._Aspect; } }
        /// <summary>
        /// Gets the ordinal position of the member.
        /// </summary>
        public int Ordinal { get { return this._Ordinal; } }
        /// <summary>
        /// Gets the member value.
        /// </summary>
        public object Value { get { return this._Value; } }
        /// <summary>
        /// Gets the member.
        /// </summary>
        public AspectMember Member { get { return _Aspect.GetMember(_Ordinal); } }

        /// <summary>
        /// Gets a string representation of the aspect member value.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat(Member.Name, "=", _Value);
        }
    }
}
