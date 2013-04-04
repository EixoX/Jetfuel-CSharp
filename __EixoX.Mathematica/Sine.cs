using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class Sine
    {

        /** Sine, Cosine, Tangent tables are for 0, 1/8, 2/8, ... 13/8 = PI/2 approx. */
        public static readonly int SINE_TABLE_LEN = 14;

        /** Sine table (high bits). */
        public static readonly double[] SINE_TABLE_A =
        {
        +0.0d,
        +0.1246747374534607d,
        +0.24740394949913025d,
        +0.366272509098053d,
        +0.4794255495071411d,
        +0.5850973129272461d,
        +0.6816387176513672d,
        +0.7675435543060303d,
        +0.8414709568023682d,
        +0.902267575263977d,
        +0.9489846229553223d,
        +0.9808930158615112d,
        +0.9974949359893799d,
        +0.9985313415527344d,
    };

        /** Sine table (low bits). */
        public static readonly double[] SINE_TABLE_B =
        {
        +0.0d,
        -4.068233003401932E-9d,
        +9.755392680573412E-9d,
        +1.9987994582857286E-8d,
        -1.0902938113007961E-8d,
        -3.9986783938944604E-8d,
        +4.23719669792332E-8d,
        -5.207000323380292E-8d,
        +2.800552834259E-8d,
        +1.883511811213715E-8d,
        -3.5997360512765566E-9d,
        +4.116164446561962E-8d,
        +5.0614674548127384E-8d,
        -1.0129027912496858E-9d,
    };


        /**
         *  Computes sin(x) - x, where |x| < 1/16.
         *  Use a Remez polynomial approximation.
         *  @param x a number smaller than 1/16
         *  @return sin(x) - x
         */
        public static double Poly(double x)
        {
            double x2 = x * x;

            double p = 2.7553817452272217E-6;
            p = p * x2 + -1.9841269659586505E-4;
            p = p * x2 + 0.008333333333329196;
            p = p * x2 + -0.16666666666666666;
            //p *= x2;
            //p *= x;
            p = p * x2 * x;

            return p;
        }

        /**
         *  Compute sine over the first quadrant (0 < x < pi/2).
         *  Use combination of table lookup and rational polynomial expansion.
         *  @param xa number from which sine is requested
         *  @param xb extra bits for x (may be 0.0)
         *  @return sin(xa + xb)
         */
        public static double Quadrant(double xa, double xb)
        {
            int idx = (int)((xa * 8.0) + 0.5);
            double epsilon = xa - BitOps.EIGHTHS[idx]; //idx*0.125;

            // Table lookups
            double sintA = SINE_TABLE_A[idx];
            double sintB = SINE_TABLE_B[idx];
            double costA = Cosine.COSINE_TABLE_A[idx];
            double costB = Cosine.COSINE_TABLE_B[idx];

            // Polynomial eval of sin(epsilon), cos(epsilon)
            double sinEpsA = epsilon;
            double sinEpsB = Poly(epsilon);
            double cosEpsA = 1.0;
            double cosEpsB = Cosine.Poly(epsilon);

            // Split epsilon   xa + xb = x
            double temp = sinEpsA * BitOps.HEX_40000000;
            double temp2 = (sinEpsA + temp) - temp;
            sinEpsB += sinEpsA - temp2;
            sinEpsA = temp2;

            /* Compute sin(x) by angle addition formula */
            double result;

            /* Compute the following sum:
             *
             * result = sintA + costA*sinEpsA + sintA*cosEpsB + costA*sinEpsB +
             *          sintB + costB*sinEpsA + sintB*cosEpsB + costB*sinEpsB;
             *
             * Ranges of elements
             *
             * xxxtA   0            PI/2
             * xxxtB   -1.5e-9      1.5e-9
             * sinEpsA -0.0625      0.0625
             * sinEpsB -6e-11       6e-11
             * cosEpsA  1.0
             * cosEpsB  0           -0.0625
             *
             */

            //result = sintA + costA*sinEpsA + sintA*cosEpsB + costA*sinEpsB +
            //          sintB + costB*sinEpsA + sintB*cosEpsB + costB*sinEpsB;

            //result = sintA + sintA*cosEpsB + sintB + sintB * cosEpsB;
            //result += costA*sinEpsA + costA*sinEpsB + costB*sinEpsA + costB * sinEpsB;
            double a = 0;
            double b = 0;

            double t = sintA;
            double c = a + t;
            double d = -(c - a - t);
            a = c;
            b = b + d;

            t = costA * sinEpsA;
            c = a + t;
            d = -(c - a - t);
            a = c;
            b = b + d;

            b = b + sintA * cosEpsB + costA * sinEpsB;
            /*
        t = sintA*cosEpsB;
        c = a + t;
        d = -(c - a - t);
        a = c;
        b = b + d;

        t = costA*sinEpsB;
        c = a + t;
        d = -(c - a - t);
        a = c;
        b = b + d;
             */

            b = b + sintB + costB * sinEpsA + sintB * cosEpsB + costB * sinEpsB;
            /*
        t = sintB;
        c = a + t;
        d = -(c - a - t);
        a = c;
        b = b + d;

        t = costB*sinEpsA;
        c = a + t;
        d = -(c - a - t);
        a = c;
        b = b + d;

        t = sintB*cosEpsB;
        c = a + t;
        d = -(c - a - t);
        a = c;
        b = b + d;

        t = costB*sinEpsB;
        c = a + t;
        d = -(c - a - t);
        a = c;
        b = b + d;
             */

            if (xb != 0.0)
            {
                t = ((costA + costB) * (cosEpsA + cosEpsB) -
                     (sintA + sintB) * (sinEpsA + sinEpsB)) * xb;  // approximate cosine*xb
                c = a + t;
                d = -(c - a - t);
                a = c;
                b = b + d;
            }

            result = a + b;

            return result;
        }

        /**
             * Sine function.
             *
             * @param x Argument.
             * @return sin(x)
             */
        public static double Calc(double x)
        {
            bool negative = false;
            int quadrant = 0;
            double xa;
            double xb = 0.0;

            /* Take absolute value of the input */
            xa = x;
            if (x < 0)
            {
                negative = true;
                xa = -xa;
            }

            /* Check for zero and negative zero */
            if (xa == 0.0)
            {
                long bits = BitConverter.DoubleToInt64Bits(x);
                if (bits < 0)
                {
                    return -0.0;
                }
                return 0.0;
            }

            if (xa == Double.PositiveInfinity)
            {
                return Double.NaN;
            }

            /* Perform any argument reduction */
            if (xa > 3294198.0)
            {
                // PI * (2**20)
                // Argument too big for CodyWaite reduction.  Must use
                // PayneHanek.
                double[] reduceResults = new double[3];
                PayneHanek.Reduce(xa, reduceResults);
                quadrant = ((int)reduceResults[0]) & 3;
                xa = reduceResults[1];
                xb = reduceResults[2];
            }
            else if (xa > 1.5707963267948966)
            {
                CodyWaite cw = new CodyWaite(xa);
                quadrant = cw.getK() & 3;
                xa = cw.getRemA();
                xb = cw.getRemB();
            }

            if (negative)
            {
                quadrant ^= 2;  // Flip bit 1
            }

            switch (quadrant)
            {
                case 0:
                    return Quadrant(xa, xb);
                case 1:
                    return Cosine.Quadrant(xa, xb);
                case 2:
                    return -Quadrant(xa, xb);
                case 3:
                    return -Cosine.Quadrant(xa, xb);
                default:
                    return Double.NaN;
            }
        }

        /** Compute the arc sine of a number.
             * @param x number on which evaluation is done
             * @return arc sine of x
             */
        public static double Arc(double x)
        {

            if (x > 1.0 || x < -1.0)
            {
                return Double.NaN;
            }

            if (x == 1.0)
            {
                return Math.PI / 2.0;
            }

            if (x == -1.0)
            {
                return -Math.PI / 2.0;
            }

            if (x == 0.0)
            { // Matches +/- 0.0; return correct sign
                return x;
            }

            /* Compute asin(x) = atan(x/sqrt(1-x*x)) */

            /* Split x */
            double temp = x * BitOps.HEX_40000000;
            double xa = x + temp - temp;
            double xb = x - xa;

            /* Square it */
            double ya = xa * xa;
            double yb = xa * xb * 2.0 + xb * xb;

            /* Subtract from 1 */
            ya = -ya;
            yb = -yb;

            double za = 1.0 + ya;
            double zb = -(za - 1.0 - ya);

            temp = za + yb;
            zb += -(temp - za - yb);
            za = temp;

            /* Square root */
            double y;
            y = Math.Sqrt(za);
            temp = y * BitOps.HEX_40000000;
            ya = y + temp - temp;
            yb = y - ya;

            /* Extend precision of sqrt */
            yb += (za - ya * ya - 2 * ya * yb - yb * yb) / (2.0 * y);

            /* Contribution of zb to sqrt */
            double dx = zb / (2.0 * y);

            // Compute ratio r = x/y
            double r = x / y;
            temp = r * BitOps.HEX_40000000;
            double ra = r + temp - temp;
            double rb = r - ra;

            rb += (x - ra * ya - ra * yb - rb * ya - rb * yb) / y;  // Correct for rounding in division
            rb += -x * dx / y / y;  // Add in effect additional bits of sqrt.

            temp = ra + rb;
            rb = -(temp - ra - rb);
            ra = temp;

            return atan(ra, rb, false);
        }

    }
}
