/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace EixoX.Mathematica
{
    /// <summary>
    /// Interface representing <a href="http://mathworld.wolfram.com/Field.html">field</a> elements.
    /// </summary>
    /// <typeparam name="T">The type of field elements.</typeparam>
    public interface FieldElement<T>
    {


        /// <summary>
        /// Compute this + a.
        /// </summary>
        /// <param name="a">a element to add</param>
        /// <returns>a new element representing this + a</returns>
        T Add(T a);

        /// <summary>
        /// Compute this - a.
        /// </summary>
        /// <param name="a">a element to subtract</param>
        /// <returns>a new element representing this - a</returns>
        T Subtract(T a);

        /// <summary>
        /// Returns the additive inverse of {@code this} element.
        /// </summary>
        /// <returns>the opposite of {@code this}.</returns>
        T Negate();

        /// <summary>
        /// Compute n &times; this. Multiplication by an integer number is defined
        /// as the following sum n &times; this = &sum;<sub>i=1</sub><sup>n</sup> this.
        /// </summary>
        /// <param name="n">Number of times {@code this} must be added to itself.</param>
        /// <returns>A new element representing n &times; this.</returns>
        T Multiply(int n);

        /// <summary>
        /// Compute this &times; a.
        /// </summary>
        /// <param name="a">a element to multiply</param>
        /// <returns>a new element representing this &times; a</returns>
        T Multiply(T a);

        /// <summary>
        ///  Compute this &divide; a.
        /// </summary>
        /// <param name="a">a element to add</param>
        /// <returns>a new element representing this &divide; a</returns>
        T Divide(T a);

        /// <summary>
        /// Returns the multiplicative inverse of {@code this} element.
        /// </summary>
        /// <returns>the inverse of {@code this}.</returns>
        T Reciprocal();

        /// <summary>
        /// Get the {@link Field} to which the instance belongs.
        /// </summary>
        Field<T> Field { get; }
    }
}