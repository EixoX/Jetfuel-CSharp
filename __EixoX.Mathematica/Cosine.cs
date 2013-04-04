using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class Cosine
    {
        /** Cosine table (high bits). */
        public static readonly double[] COSINE_TABLE_A =
        {
        +1.0d,
        +0.9921976327896118d,
        +0.9689123630523682d,
        +0.9305076599121094d,
        +0.8775825500488281d,
        +0.8109631538391113d,
        +0.7316888570785522d,
        +0.6409968137741089d,
        +0.5403022766113281d,
        +0.4311765432357788d,
        +0.3153223395347595d,
        +0.19454771280288696d,
        +0.07073719799518585d,
        -0.05417713522911072d,
    };

        /** Cosine table (low bits). */
        public static readonly double[] COSINE_TABLE_B =
        {
        +0.0d,
        +3.4439717236742845E-8d,
        +5.865827662008209E-8d,
        -3.7999795083850525E-8d,
        +1.184154459111628E-8d,
        -3.43338934259355E-8d,
        +1.1795268640216787E-8d,
        +4.438921624363781E-8d,
        +2.925681159240093E-8d,
        -2.6437112632041807E-8d,
        +2.2860509143963117E-8d,
        -4.813899778443457E-9d,
        +3.6725170580355583E-9d,
        +2.0217439756338078E-10d,
    };


        /**
         *  Computes cos(x) - 1, where |x| < 1/16.
         *  Use a Remez polynomial approximation.
         *  @param x a number smaller than 1/16
         *  @return cos(x) - 1
         */
        public static double Poly(double x)
        {
            double x2 = x * x;

            double p = 2.479773539153719E-5;
            p = p * x2 + -0.0013888888689039883;
            p = p * x2 + 0.041666666666621166;
            p = p * x2 + -0.49999999999999994;
            p *= x2;

            return p;
        }



        /**
         * Compute cosine in the first quadrant by subtracting input from PI/2 and
         * then calling sinQ.  This is more accurate as the input approaches PI/2.
         *  @param xa number from which cosine is requested
         *  @param xb extra bits for x (may be 0.0)
         *  @return cos(xa + xb)
         */
        public static double Quadrant(double xa, double xb)
        {
            double pi2a = 1.5707963267948966;
            double pi2b = 6.123233995736766E-17;
            double a = pi2a - xa;
            double b = -(a - pi2a + xa);
            b += pi2b - xb;

            return Sine.Quadrant(a, b);
        }

        /**
             * Cosine function.
             *
             * @param x Argument.
             * @return cos(x)
             */
        public static double Calc(double x)
        {
            int quadrant = 0;

            /* Take absolute value of the input */
            double xa = x;
            if (x < 0)
            {
                xa = -xa;
            }

            if (xa == double.PositiveInfinity)
            {
                return Double.NaN;
            }

            /* Perform any argument reduction */
            double xb = 0;
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

            //if (negative)
            //  quadrant = (quadrant + 2) % 4;

            switch (quadrant)
            {
                case 0:
                    return cosQ(xa, xb);
                case 1:
                    return -sinQ(xa, xb);
                case 2:
                    return -cosQ(xa, xb);
                case 3:
                    return sinQ(xa, xb);
                default:
                    return Double.NaN;
            }
        }



        /** Compute the arc cosine of a number.
         * @param x number on which evaluation is done
         * @return arc cosine of x
         */
        public static double Arc(double x)
        {

            if (x > 1.0 || x < -1.0)
            {
                return Double.NaN;
            }

            if (x == -1.0)
            {
                return Math.PI;
            }

            if (x == 1.0)
            {
                return 0.0;
            }

            if (x == 0)
            {
                return Math.PI / 2.0;
            }

            /* Compute acos(x) = atan(sqrt(1-x*x)/x) */

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
            double y = Math.Sqrt(za);
            temp = y * BitOps.HEX_40000000;
            ya = y + temp - temp;
            yb = y - ya;

            /* Extend precision of sqrt */
            yb += (za - ya * ya - 2 * ya * yb - yb * yb) / (2.0 * y);

            /* Contribution of zb to sqrt */
            yb += zb / (2.0 * y);
            y = ya + yb;
            yb = -(y - ya - yb);

            // Compute ratio r = y/x
            double r = y / x;

            // Did r overflow?
            if (double.IsInfinity(r))
            { // x is effectively zero
                return Math.PI / 2; // so return the appropriate value
            }

            double ra = BitOps.HighPart(r);
            double rb = r - ra;

            rb += (y - ra * xa - ra * xb - rb * xa - rb * xb) / x;  // Correct for rounding in division
            rb += yb / x;  // Add in effect additional bits of sqrt.

            temp = ra + rb;
            rb = -(temp - ra - rb);
            ra = temp;

            return atan(ra, rb, x < 0);
        }

    }
}
