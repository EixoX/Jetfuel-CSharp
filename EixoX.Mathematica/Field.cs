using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    /// <summary>
    /// A field is any set of elements that satisfies the field axioms for 
    /// both addition and multiplication and is a commutative division algebra. 
    /// An archaic name for a field is rational domain. The French term for a field is corps and the German word is Körper, 
    /// both meaning "body." A field with a finite number of members is known as a finite field or Galois field.
    /// </summary>
    /// <remarks>
    /// http://mathworld.wolfram.com/Field.html
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public interface Field<T>
        : IFormattable, IComparable<T>, IEquatable<T>
    {
        T Add(T other);
        T Subtract(T other);
        T Multiply(T other);
        T Divide(T other);
        T Negate();
        T Inverse();
        bool EqualTo(T other);
        bool GreaterThan(T other);
        bool GreaterOrEqual(T other);
        bool LowerThan(T other);
        bool LowerOrEqual(T other);
    }
}
