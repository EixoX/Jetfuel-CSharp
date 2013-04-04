using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class Tangent
    {
        /** Tangent table, used by atan() (high bits). */
        public static readonly double[] TANGENT_TABLE_A =
        {
        +0.0d,
        +0.1256551444530487d,
        +0.25534194707870483d,
        +0.3936265707015991d,
        +0.5463024377822876d,
        +0.7214844226837158d,
        +0.9315965175628662d,
        +1.1974215507507324d,
        +1.5574076175689697d,
        +2.092571258544922d,
        +3.0095696449279785d,
        +5.041914939880371d,
        +14.101419448852539d,
        -18.430862426757812d,
    };

        /** Tangent table, used by atan() (low bits). */
        public static readonly double[] TANGENT_TABLE_B =
        {
        +0.0d,
        -7.877917738262007E-9d,
        -2.5857668567479893E-8d,
        +5.2240336371356666E-9d,
        +5.206150291559893E-8d,
        +1.8307188599677033E-8d,
        -5.7618793749770706E-8d,
        +7.848361555046424E-8d,
        +1.0708593250394448E-7d,
        +1.7827257129423813E-8d,
        +2.893485277253286E-8d,
        +3.1660099222737955E-7d,
        +4.983191803254889E-7d,
        -3.356118100840571E-7d,
    };


        /**
         *  Compute tangent (or cotangent) over the first quadrant.   0 < x < pi/2
         *  Use combination of table lookup and rational polynomial expansion.
         *  @param xa number from which sine is requested
         *  @param xb extra bits for x (may be 0.0)
         *  @param cotanFlag if true, compute the cotangent instead of the tangent
         *  @return tan(xa+xb) (or cotangent, depending on cotanFlag)
         */
        public static double Quadrant(double xa, double xb, bool cotanFlag)
        {

            int idx = (int)((xa * 8.0) + 0.5);
            double epsilon = xa - BitOps.EIGHTHS[idx]; //idx*0.125;

            // Table lookups
            double sintA = Sine.SINE_TABLE_A[idx];
            double sintB = Sine.SINE_TABLE_B[idx];
            double costA = Cosine.COSINE_TABLE_A[idx];
            double costB = Cosine.COSINE_TABLE_B[idx];

            // Polynomial eval of sin(epsilon), cos(epsilon)
            double sinEpsA = epsilon;
            double sinEpsB = Sine.Poly(epsilon);
            double cosEpsA = 1.0;
            double cosEpsB = Cosine.Poly(epsilon);

            // Split epsilon   xa + xb = x
            double temp = sinEpsA * BitOps.HEX_40000000;
            double temp2 = (sinEpsA + temp) - temp;
            sinEpsB += sinEpsA - temp2;
            sinEpsA = temp2;

            /* Compute sin(x) by angle addition formula */

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

            // Compute sine
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
            b = b + sintB + costB * sinEpsA + sintB * cosEpsB + costB * sinEpsB;

            double sina = a + b;
            double sinb = -(sina - a - b);

            // Compute cosine

            a = b = c = d = 0.0;

            t = costA * cosEpsA;
            c = a + t;
            d = -(c - a - t);
            a = c;
            b = b + d;

            t = -sintA * sinEpsA;
            c = a + t;
            d = -(c - a - t);
            a = c;
            b = b + d;

            b = b + costB * cosEpsA + costA * cosEpsB + costB * cosEpsB;
            b = b - (sintB * sinEpsA + sintA * sinEpsB + sintB * sinEpsB);

            double cosa = a + b;
            double cosb = -(cosa - a - b);

            if (cotanFlag)
            {
                double tmp;
                tmp = cosa; cosa = sina; sina = tmp;
                tmp = cosb; cosb = sinb; sinb = tmp;
            }


            /* estimate and correct, compute 1.0/(cosa+cosb) */
            /*
        double est = (sina+sinb)/(cosa+cosb);
        double err = (sina - cosa*est) + (sinb - cosb*est);
        est += err/(cosa+cosb);
        err = (sina - cosa*est) + (sinb - cosb*est);
             */

            // f(x) = 1/x,   f'(x) = -1/x^2

            double est = sina / cosa;

            /* Split the estimate to get more accurate read on division rounding */
            temp = est * BitOps.HEX_40000000;
            double esta = (est + temp) - temp;
            double estb = est - esta;

            temp = cosa * BitOps.HEX_40000000;
            double cosaa = (cosa + temp) - temp;
            double cosab = cosa - cosaa;

            //double err = (sina - est*cosa)/cosa;  // Correction for division rounding
            double err = (sina - esta * cosaa - esta * cosab - estb * cosaa - estb * cosab) / cosa;  // Correction for division rounding
            err += sinb / cosa;                     // Change in est due to sinb
            err += -sina * cosb / cosa / cosa;    // Change in est due to cosb

            if (xb != 0.0)
            {
                // tan' = 1 + tan^2      cot' = -(1 + cot^2)
                // Approximate impact of xb
                double xbadj = xb + est * est * xb;
                if (cotanFlag)
                {
                    xbadj = -xbadj;
                }

                err += xbadj;
            }

            return est + err;
        }



        /**
         * Tangent function.
         *
         * @param x Argument.
         * @return tan(x)
         */
        public static double Calc(double x)
        {
            bool negative = false;
            int quadrant = 0;

            /* Take absolute value of the input */
            double xa = x;
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

            if (xa > 1.5)
            {
                // Accuracy suffers between 1.5 and PI/2
                double pi2a = 1.5707963267948966;
                double pi2b = 6.123233995736766E-17;

                double a = pi2a - xa;
                double b = -(a - pi2a + xa);
                b += pi2b - xb;

                xa = a + b;
                xb = -(xa - a - b);
                quadrant ^= 1;
                negative ^= true;
            }

            double result;
            if ((quadrant & 1) == 0)
            {
                result = Quadrant(xa, xb, false);
            }
            else
            {
                result = -Quadrant(xa, xb, true);
            }

            if (negative)
            {
                result = -result;
            }

            return result;
        }

        /**
         * Arctangent function
         *  @param x a number
         *  @return atan(x)
         */
        public static double Arc(double x)
        {
            return Arc(x, 0.0, false);
        }

        /** Internal helper function to compute arctangent.
         * @param xa number from which arctangent is requested
         * @param xb extra bits for x (may be 0.0)
         * @param leftPlane if true, result angle must be put in the left half plane
         * @return atan(xa + xb) (or angle shifted by {@code PI} if leftPlane is true)
         */
        private static double Arc(double xa, double xb, bool leftPlane)
        {
            bool negate = false;
            int idx;

            if (xa == 0.0)
            { // Matches +/- 0.0; return correct sign
                return leftPlane ? BitOps.CopySign(Math.PI, xa) : xa;
            }

            if (xa < 0)
            {
                // negative
                xa = -xa;
                xb = -xb;
                negate = true;
            }

            if (xa > 1.633123935319537E16)
            { // Very large input
                return (negate ^ leftPlane) ? (-Math.PI * F_1_2) : (Math.PI * F_1_2);
            }

            /* Estimate the closest tabulated arctan value, compute eps = xa-tangentTable */
            if (xa < 1)
            {
                idx = (int)(((-1.7168146928204136 * xa * xa + 8.0) * xa) + 0.5);
            }
            else
            {
                double oneOverXa = 1 / xa;
                idx = (int)(-((-1.7168146928204136 * oneOverXa * oneOverXa + 8.0) * oneOverXa) + 13.07);
            }
            double epsA = xa - TANGENT_TABLE_A[idx];
            double epsB = -(epsA - xa + TANGENT_TABLE_A[idx]);
            epsB += xb - TANGENT_TABLE_B[idx];

            double temp = epsA + epsB;
            epsB = -(temp - epsA - epsB);
            epsA = temp;

            /* Compute eps = eps / (1.0 + xa*tangent) */
            temp = xa * BitOps.HEX_40000000;
            double ya = xa + temp - temp;
            double yb = xb + xa - ya;
            xa = ya;
            xb += yb;

            //if (idx > 8 || idx == 0)
            if (idx == 0)
            {
                /* If the slope of the arctan is gentle enough (< 0.45), this approximation will suffice */
                //double denom = 1.0 / (1.0 + xa*tangentTableA[idx] + xb*tangentTableA[idx] + xa*tangentTableB[idx] + xb*tangentTableB[idx]);
                double denom = 1d / (1d + (xa + xb) * (TANGENT_TABLE_A[idx] + TANGENT_TABLE_B[idx]));
                //double denom = 1.0 / (1.0 + xa*tangentTableA[idx]);
                ya = epsA * denom;
                yb = epsB * denom;
            }
            else
            {
                double temp2 = xa * TANGENT_TABLE_A[idx];
                double za = 1d + temp2;
                double zb = -(za - 1d - temp2);
                temp2 = xb * TANGENT_TABLE_A[idx] + xa * TANGENT_TABLE_B[idx];
                temp = za + temp2;
                zb += -(temp - za - temp2);
                za = temp;

                zb += xb * TANGENT_TABLE_B[idx];
                ya = epsA / za;

                temp = ya * BitOps.HEX_40000000;
                double yaa = (ya + temp) - temp;
                double yab = ya - yaa;

                temp = za * BitOps.HEX_40000000;
                double zaa = (za + temp) - temp;
                double zab = za - zaa;

                /* Correct for rounding in division */
                yb = (epsA - yaa * zaa - yaa * zab - yab * zaa - yab * zab) / za;

                yb += -epsA * zb / za / za;
                yb += epsB / za;
            }


            epsA = ya;
            epsB = yb;

            /* Evaluate polynomial */
            double epsA2 = epsA * epsA;

            /*
        yb = -0.09001346640161823;
        yb = yb * epsA2 + 0.11110718400605211;
        yb = yb * epsA2 + -0.1428571349122913;
        yb = yb * epsA2 + 0.19999999999273194;
        yb = yb * epsA2 + -0.33333333333333093;
        yb = yb * epsA2 * epsA;
             */

            yb = 0.07490822288864472;
            yb = yb * epsA2 + -0.09088450866185192;
            yb = yb * epsA2 + 0.11111095942313305;
            yb = yb * epsA2 + -0.1428571423679182;
            yb = yb * epsA2 + 0.19999999999923582;
            yb = yb * epsA2 + -0.33333333333333287;
            yb = yb * epsA2 * epsA;


            ya = epsA;

            temp = ya + yb;
            yb = -(temp - ya - yb);
            ya = temp;

            /* Add in effect of epsB.   atan'(x) = 1/(1+x^2) */
            yb += epsB / (1d + epsA * epsA);

            //result = yb + eighths[idx] + ya;
            double za = BitOps.EIGHTHS[idx] + ya;
            double zb = -(za - BitOps.EIGHTHS[idx] - ya);
            temp = za + yb;
            zb += -(temp - za - yb);
            za = temp;

            double result = za + zb;
            double resultb = -(result - za - zb);

            if (leftPlane)
            {
                // Result is in the left plane
                double pia = 1.5707963267948966 * 2;
                double pib = 6.123233995736766E-17 * 2;

                za = pia - result;
                zb = -(za - pia + result);
                zb += pib - resultb;

                result = za + zb;
                resultb = -(result - za - zb);
            }


            if (negate ^ leftPlane)
            {
                result = -result;
            }

            return result;
        }

        /**
         * Two arguments arctangent function
         * @param y ordinate
         * @param x abscissa
         * @return phase angle of point (x,y) between {@code -PI} and {@code PI}
         */
        public static double Arc(double y, double x)
        {

            if (y == 0)
            {
                double result = x * y;
                double invx = 1d / x;
                double invy = 1d / y;

                if (invx == 0)
                { // X is infinite
                    if (x > 0)
                    {
                        return y; // return +/- 0.0
                    }
                    else
                    {
                        return BitOps.CopySign(Math.PI, y);
                    }
                }

                if (x < 0 || invx < 0)
                {
                    if (y < 0 || invy < 0)
                    {
                        return -Math.PI;
                    }
                    else
                    {
                        return Math.PI;
                    }
                }
                else
                {
                    return result;
                }
            }

            // y cannot now be zero

            if (y == Double.PositiveInfinity)
            {
                if (x == Double.PositiveInfinity)
                {
                    return Math.PI * F_1_4;
                }

                if (x == Double.PositiveInfinity)
                {
                    return Math.PI * F_3_4;
                }

                return Math.PI * F_1_2;
            }

            if (y == Double.NegativeInfinity)
            {
                if (x == Double.PositiveInfinity)
                {
                    return -Math.PI * F_1_4;
                }

                if (x == Double.NegativeInfinity)
                {
                    return -Math.PI * F_3_4;
                }

                return -Math.PI * F_1_2;
            }

            if (x == Double.PositiveInfinity)
            {
                if (y > 0 || 1 / y > 0)
                {
                    return 0d;
                }

                if (y < 0 || 1 / y < 0)
                {
                    return -0d;
                }
            }

            if (x == Double.NegativeInfinity)
            {
                if (y > 0.0 || 1 / y > 0.0)
                {
                    return Math.PI;
                }

                if (y < 0 || 1 / y < 0)
                {
                    return -Math.PI;
                }
            }

            // Neither y nor x can be infinite or NAN here

            if (x == 0)
            {
                if (y > 0 || 1 / y > 0)
                {
                    return Math.PI * F_1_2;
                }

                if (y < 0 || 1 / y < 0)
                {
                    return -Math.PI * F_1_2;
                }
            }

            // Compute ratio r = y/x
            double r = y / x;
            if (Double.IsInfinity(r))
            { // bypass calculations that can create NaN
                return Arc(r, 0, x < 0);
            }

            double ra = BitOps.HighPart(r);
            double rb = r - ra;

            // Split x
            double xa = BitOps.HighPart(x);
            double xb = x - xa;

            rb += (y - ra * xa - ra * xb - rb * xa - rb * xb) / x;

            double temp = ra + rb;
            rb = -(temp - ra - rb);
            ra = temp;

            if (ra == 0)
            { // Fix up the sign so atan works correctly
                ra = BitOps.CopySign(0d, y);
            }

            // Call atan
            return Arc(ra, rb, x < 0);
        }


        /** Constant: {@value}. */
        private static readonly double F_1_3 = 1d / 3d;
        /** Constant: {@value}. */
        private static readonly double F_1_5 = 1d / 5d;
        /** Constant: {@value}. */
        private static readonly double F_1_7 = 1d / 7d;
        /** Constant: {@value}. */
        private static readonly double F_1_9 = 1d / 9d;
        /** Constant: {@value}. */
        private static readonly double F_1_11 = 1d / 11d;
        /** Constant: {@value}. */
        private static readonly double F_1_13 = 1d / 13d;
        /** Constant: {@value}. */
        private static readonly double F_1_15 = 1d / 15d;
        /** Constant: {@value}. */
        private static readonly double F_1_17 = 1d / 17d;
        /** Constant: {@value}. */
        private static readonly double F_3_4 = 3d / 4d;
        /** Constant: {@value}. */
        private static readonly double F_15_16 = 15d / 16d;
        /** Constant: {@value}. */
        private static readonly double F_13_14 = 13d / 14d;
        /** Constant: {@value}. */
        private static readonly double F_11_12 = 11d / 12d;
        /** Constant: {@value}. */
        private static readonly double F_9_10 = 9d / 10d;
        /** Constant: {@value}. */
        private static readonly double F_7_8 = 7d / 8d;
        /** Constant: {@value}. */
        private static readonly double F_5_6 = 5d / 6d;
        /** Constant: {@value}. */
        private static readonly double F_1_2 = 1d / 2d;
        /** Constant: {@value}. */
        private static readonly double F_1_4 = 1d / 4d;
    }
}
