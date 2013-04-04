using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class PayneHanek
    {

        /** Bits of 1/(2*pi), need for reducePayneHanek(). */
        private static readonly ulong[] RECIP_2PI = new ulong[] {
        (0x28be60dbL << 32) | 0x9391054aL,
        (0x7f09d5f4L << 32) | 0x7d4d3770L,
        (0x36d8a566L << 32) | 0x4f10e410L,
        (0x7f9458eaL << 32) | 0xf7aef158L,
        (0x6dc91b8eL << 32) | 0x909374b8L,
        (0x01924bbaL << 32) | 0x82746487L,
        (0x3f877ac7L << 32) | 0x2c4a69cfL,
        (ulong)(0xba208d7dL << 32) | 0x4baed121L,
        (0x3a671c09L << 32) | 0xad17df90L,
        (0x4e64758eL << 32) | 0x60d4ce7dL,
        (0x272117e2L << 32) | 0xef7e4a0eL,
        (ulong)(0xc7fe25ffL << 32) | 0xf7816603L,
        (ulong)(0xfbcbc462L << 32) | 0xd6829b47L,
        (ulong)(0xdb4d9fb3L << 32) | 0xc9f2c26dL,
        (ulong)(0xd3d18fd9L << 32) | 0xa797fa8bL,
        (0x5d49eeb1L << 32) | 0xfaf97c5eL,
        (ulong)(0xcf41ce7dL << 32) | 0xe294a4baL,
         (ulong)0x9afed7ecL << 32  };

        /** Bits of pi/4, need for reducePayneHanek(). */
        private static readonly ulong[] PI_O_4_BITS = new ulong[] {
        (ulong)(0xc90fdaa2L << 32) | 0x2168c234L,
        (ulong)(0xc4c6628bL << 32) | 0x80dc1cd1L };

        /** Reduce the input argument using the Payne and Hanek method.
     *  This is good for all inputs 0.0 < x < inf
     *  Output is remainder after dividing by PI/2
     *  The result array should contain 3 numbers.
     *  result[0] is the integer portion, so mod 4 this gives the quadrant.
     *  result[1] is the upper bits of the remainder
     *  result[2] is the lower bits of the remainder
     *
     * @param x number to reduce
     * @param result placeholder where to put the result
     */
        public static void Reduce(double x, double[] result)
        {
            /* Convert input double to bits */
            ulong inbits = (ulong)BitConverter.DoubleToInt64Bits(x);
            int exponent = (int)((inbits >> 52) & 0x7ff) - 1023;

            /* Convert to fixed point representation */
            inbits &= 0x000fffffffffffffL;
            inbits |= 0x0010000000000000L;

            /* Normalize input to be between 0.5 and 1.0 */
            exponent++;
            inbits <<= 11;

            /* Based on the exponent, get a shifted copy of recip2pi */
            ulong shpi0;
            ulong shpiA;
            ulong shpiB;
            int idx = exponent >> 6;
            int shift = exponent - (idx << 6);

            if (shift != 0)
            {
                shpi0 = (idx == 0) ? 0 : (RECIP_2PI[idx - 1] << shift);
                shpi0 |= RECIP_2PI[idx] >> (64 - shift);
                shpiA = (RECIP_2PI[idx] << shift) | (RECIP_2PI[idx + 1] >> (64 - shift));
                shpiB = (RECIP_2PI[idx + 1] << shift) | (RECIP_2PI[idx + 2] >> (64 - shift));
            }
            else
            {
                shpi0 = (idx == 0) ? 0 : RECIP_2PI[idx - 1];
                shpiA = RECIP_2PI[idx];
                shpiB = RECIP_2PI[idx + 1];
            }

            /* Multiply input by shpiA */
            ulong a = inbits >> 32;
            ulong b = inbits & 0xffffffffL;

            ulong c = shpiA >> 32;
            ulong d = shpiA & 0xffffffffL;

            ulong ac = a * c;
            ulong bd = b * d;
            ulong bc = b * c;
            ulong ad = a * d;

            ulong prodB = bd + (ad << 32);
            ulong prodA = ac + (ad >> 32);

            bool bita = (bd & 0x8000000000000000L) != 0;
            bool bitb = (ad & 0x80000000L) != 0;
            bool bitsum = (prodB & 0x8000000000000000L) != 0;

            /* Carry */
            if ((bita && bitb) ||
                    ((bita || bitb) && !bitsum))
            {
                prodA++;
            }

            bita = (prodB & 0x8000000000000000L) != 0;
            bitb = (bc & 0x80000000L) != 0;

            prodB = prodB + (bc << 32);
            prodA = prodA + (bc >> 32);

            bitsum = (prodB & 0x8000000000000000L) != 0;

            /* Carry */
            if ((bita && bitb) ||
                    ((bita || bitb) && !bitsum))
            {
                prodA++;
            }

            /* Multiply input by shpiB */
            c = shpiB >> 32;
            d = shpiB & 0xffffffffL;
            ac = a * c;
            bc = b * c;
            ad = a * d;

            /* Collect terms */
            ac = ac + ((bc + ad) >> 32);

            bita = (prodB & 0x8000000000000000L) != 0;
            bitb = (ac & 0x8000000000000000L) != 0;
            prodB += ac;
            bitsum = (prodB & 0x8000000000000000L) != 0;
            /* Carry */
            if ((bita && bitb) ||
                    ((bita || bitb) && !bitsum))
            {
                prodA++;
            }

            /* Multiply by shpi0 */
            c = shpi0 >> 32;
            d = shpi0 & 0xffffffffL;

            bd = b * d;
            bc = b * c;
            ad = a * d;

            prodA += bd + ((bc + ad) << 32);

            /*
             * prodA, prodB now contain the remainder as a fraction of PI.  We want this as a fraction of
             * PI/2, so use the following steps:
             * 1.) multiply by 4.
             * 2.) do a fixed point muliply by PI/4.
             * 3.) Convert to floating point.
             * 4.) Multiply by 2
             */

            /* This identifies the quadrant */
            int intPart = (int)(prodA >> 62);

            /* Multiply by 4 */
            prodA <<= 2;
            prodA |= prodB >> 62;
            prodB <<= 2;

            /* Multiply by PI/4 */
            a = prodA >> 32;
            b = prodA & 0xffffffffL;

            c = PI_O_4_BITS[0] >> 32;
            d = PI_O_4_BITS[0] & 0xffffffffL;

            ac = a * c;
            bd = b * d;
            bc = b * c;
            ad = a * d;

            ulong prod2B = bd + (ad << 32);
            ulong prod2A = ac + (ad >> 32);

            bita = (bd & 0x8000000000000000L) != 0;
            bitb = (ad & 0x80000000L) != 0;
            bitsum = (prod2B & 0x8000000000000000L) != 0;

            /* Carry */
            if ((bita && bitb) ||
                    ((bita || bitb) && !bitsum))
            {
                prod2A++;
            }

            bita = (prod2B & 0x8000000000000000L) != 0;
            bitb = (bc & 0x80000000L) != 0;

            prod2B = prod2B + (bc << 32);
            prod2A = prod2A + (bc >> 32);

            bitsum = (prod2B & 0x8000000000000000L) != 0;

            /* Carry */
            if ((bita && bitb) ||
                    ((bita || bitb) && !bitsum))
            {
                prod2A++;
            }

            /* Multiply input by pio4bits[1] */
            c = PI_O_4_BITS[1] >> 32;
            d = PI_O_4_BITS[1] & 0xffffffffL;
            ac = a * c;
            bc = b * c;
            ad = a * d;

            /* Collect terms */
            ac = ac + ((bc + ad) >> 32);

            bita = (prod2B & 0x8000000000000000L) != 0;
            bitb = (ac & 0x8000000000000000L) != 0;
            prod2B += ac;
            bitsum = (prod2B & 0x8000000000000000L) != 0;
            /* Carry */
            if ((bita && bitb) ||
                    ((bita || bitb) && !bitsum))
            {
                prod2A++;
            }

            /* Multiply inputB by pio4bits[0] */
            a = prodB >> 32;
            b = prodB & 0xffffffffL;
            c = PI_O_4_BITS[0] >> 32;
            d = PI_O_4_BITS[0] & 0xffffffffL;
            ac = a * c;
            bc = b * c;
            ad = a * d;

            /* Collect terms */
            ac = ac + ((bc + ad) >> 32);

            bita = (prod2B & 0x8000000000000000L) != 0;
            bitb = (ac & 0x8000000000000000L) != 0;
            prod2B += ac;
            bitsum = (prod2B & 0x8000000000000000L) != 0;
            /* Carry */
            if ((bita && bitb) ||
                    ((bita || bitb) && !bitsum))
            {
                prod2A++;
            }

            /* Convert to double */
            double tmpA = (prod2A >> 12) / BitOps.TWO_POWER_52;  // High order 52 bits
            double tmpB = (((prod2A & 0xfffL) << 40) + (prod2B >> 24)) / BitOps.TWO_POWER_52 / BitOps.TWO_POWER_52; // Low bits

            double sumA = tmpA + tmpB;
            double sumB = -(sumA - tmpA - tmpB);

            /* Multiply by PI/2 and return */
            result[0] = intPart;
            result[1] = sumA * 2.0;
            result[2] = sumB * 2.0;
        }


    }
}
