using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public static class BitOps
    {
        /** Exponent offset in IEEE754 representation. */
        public static readonly long EXPONENT_OFFSET = 1023L;

        /**
     * 0x40000000 - used to split a double into two parts, both with the low order bits cleared.
     * Equivalent to 2^30.
     */
        public static readonly long HEX_40000000 = 0x40000000L; // 1073741824L


        /** Mask used to clear low order 30 bits */
        public static readonly long MASK_30BITS = -1L - (HEX_40000000 - 1); // 0xFFFFFFFFC0000000L;

        /** 2^52 - double numbers this large must be integral (no fraction) or NaN or Infinite */
        public static readonly double TWO_POWER_52 = 4503599627370496.0;
        /** 2^53 - double numbers this large must be even. */
        public static readonly double TWO_POWER_53 = 2 * TWO_POWER_52;

        /** Eighths.
     * This is used by sinQ, because its faster to do a table lookup than
     * a multiply in this time-critical routine
     */
        public static readonly double[] EIGHTHS = { 0, 0.125, 0.25, 0.375, 0.5, 0.625, 0.75, 0.875, 1.0, 1.125, 1.25, 1.375, 1.5, 1.625 };
        /*
         * 
         * This was previously expressed as = 0x1.0p-1022;
         * However, OpenJDK (Sparc Solaris) cannot handle such small
         * constants: MATH-721
         */
        public static readonly double SAFE_MIN = BitConverter.Int64BitsToDouble((EXPONENT_OFFSET - 1022L) << 52);

        /**
     * Get the high order bits from the mantissa.
     * Equivalent to adding and subtracting HEX_40000 but also works for very large numbers
     *
     * @param d the value to split
     * @return the high order part of the mantissa
     */
        public static double HighPart(double d)
        {
            if (d > -SAFE_MIN && d < SAFE_MIN)
            {
                return d; // These are un-normalised - don't try to convert
            }
            long xl = BitConverter.DoubleToInt64Bits(d);
            xl = xl & MASK_30BITS; // Drop low order bits
            return BitConverter.Int64BitsToDouble(xl);
        }


        /**
     * Returns the first argument with the sign of the second argument.
     * A NaN {@code sign} argument is treated as positive.
     *
     * @param magnitude the value to return
     * @param sign the sign for the returned value
     * @return the magnitude with the same sign as the {@code sign} argument
     */
        public static double CopySign(double magnitude, double sign)
        {
            long m = BitConverter.DoubleToInt64Bits(magnitude);
            long s = BitConverter.DoubleToInt64Bits(sign);
            if ((m >= 0 && s >= 0) || (m < 0 && s < 0))
            { // Sign is currently OK
                return magnitude;
            }
            return -magnitude; // flip sign
        }
    }
}
