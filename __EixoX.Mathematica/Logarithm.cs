using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class Logarithm
    {
        /// <summary>
        ///  0x40000000 - used to split a double into two parts, both with the low order bits cleared.
        ///  Equivalent to 2^30.
        /// </summary>
        public static readonly long HEX_40000000 = 0x40000000L; // 1073741824L
        /** log(2) (high bits). */
        private static readonly double LN_2_A = 0.693147063255310059;

        /** log(2) (low bits). */
        private static readonly double LN_2_B = 1.17304635250823482e-7;

        /** Coefficients for log, when input 0.99 < x < 1.01. */
        private static readonly double[][] LN_QUICK_COEF = {
        {1.0, 5.669184079525E-24},
        {-0.25, -0.25},
        {0.3333333134651184, 1.986821492305628E-8},
        {-0.25, -6.663542893624021E-14},
        {0.19999998807907104, 1.1921056801463227E-8},
        {-0.1666666567325592, -7.800414592973399E-9},
        {0.1428571343421936, 5.650007086920087E-9},
        {-0.12502530217170715, -7.44321345601866E-11},
        {0.11113807559013367, 9.219544613762692E-9},
    };

        /** Coefficients for log in the range of 1.0 < x < 1.0 + 2^-10. */
        private static readonly double[][] LN_HI_PREC_COEF = {
        {1.0, -6.032174644509064E-23},
        {-0.25, -0.25},
        {0.3333333134651184, 1.9868161777724352E-8},
        {-0.2499999701976776, -2.957007209750105E-8},
        {0.19999954104423523, 1.5830993332061267E-10},
        {-0.16624879837036133, -2.6033824355191673E-8}
    };


        /// <summary>
        ///  /Compute the base 10 logarithm.
        /// </summary>
        /// <param name="x">a number</param>
        /// <returns>The log 10</returns>
        public static double Log10(double x)
        {

            double[] hiPrec = new double[2];
            double lores = log(x, hiPrec);

            if (double.IsInfinity(lores))
            { // don't allow this to be converted to NaN
                return lores;
            }

            double tmp = hiPrec[0] * HEX_40000000;
            double lna = hiPrec[0] + tmp - tmp;
            double lnb = hiPrec[0] - lna + hiPrec[1];

            double rln10a = 0.4342944622039795;
            double rln10b = 1.9699272335463627E-8;

            return rln10b * lnb + rln10b * lna + rln10a * lnb + rln10a * lna;
        }

        
    /**
     * Natural logarithm.
     *
     * @param x   a double
     * @return log(x)
     */
    public static double log(final double x) {
        return log(x, null);
    }

    /**
     * Internal helper method for natural logarithm function.
     * @param x original argument of the natural logarithm function
     * @param hiPrec extra bits of precision on output (To Be Confirmed)
     * @return log(x)
     */
    private static double log(final double x, final double[] hiPrec) {
        if (x==0) { // Handle special case of +0/-0
            return Double.NEGATIVE_INFINITY;
        }
        long bits = Double.doubleToLongBits(x);

        /* Handle special cases of negative input, and NaN */
        if ((bits & 0x8000000000000000L) != 0 || x != x) {
            if (x != 0.0) {
                if (hiPrec != null) {
                    hiPrec[0] = Double.NaN;
                }

                return Double.NaN;
            }
        }

        /* Handle special cases of Positive infinity. */
        if (x == Double.POSITIVE_INFINITY) {
            if (hiPrec != null) {
                hiPrec[0] = Double.POSITIVE_INFINITY;
            }

            return Double.POSITIVE_INFINITY;
        }

        /* Extract the exponent */
        int exp = (int)(bits >> 52)-1023;

        if ((bits & 0x7ff0000000000000L) == 0) {
            // Subnormal!
            if (x == 0) {
                // Zero
                if (hiPrec != null) {
                    hiPrec[0] = Double.NEGATIVE_INFINITY;
                }

                return Double.NEGATIVE_INFINITY;
            }

            /* Normalize the subnormal number. */
            bits <<= 1;
            while ( (bits & 0x0010000000000000L) == 0) {
                --exp;
                bits <<= 1;
            }
        }


        if (exp == -1 || exp == 0) {
            if (x < 1.01 && x > 0.99 && hiPrec == null) {
                /* The normal method doesn't work well in the range [0.99, 1.01], so call do a straight
           polynomial expansion in higer precision. */

               /* Compute x - 1.0 and split it */
                double xa = x - 1.0;
                double xb = xa - x + 1.0;
                double tmp = xa * HEX_40000000;
                double aa = xa + tmp - tmp;
                double ab = xa - aa;
                xa = aa;
                xb = ab;

                final double[] lnCoef_last = LN_QUICK_COEF[LN_QUICK_COEF.length - 1];
                double ya = lnCoef_last[0];
                double yb = lnCoef_last[1];

                for (int i = LN_QUICK_COEF.length - 2; i >= 0; i--) {
                    /* Multiply a = y * x */
                    aa = ya * xa;
                    ab = ya * xb + yb * xa + yb * xb;
                    /* split, so now y = a */
                    tmp = aa * HEX_40000000;
                    ya = aa + tmp - tmp;
                    yb = aa - ya + ab;

                    /* Add  a = y + lnQuickCoef */
                    final double[] lnCoef_i = LN_QUICK_COEF[i];
                    aa = ya + lnCoef_i[0];
                    ab = yb + lnCoef_i[1];
                    /* Split y = a */
                    tmp = aa * HEX_40000000;
                    ya = aa + tmp - tmp;
                    yb = aa - ya + ab;
                }

                /* Multiply a = y * x */
                aa = ya * xa;
                ab = ya * xb + yb * xa + yb * xb;
                /* split, so now y = a */
                tmp = aa * HEX_40000000;
                ya = aa + tmp - tmp;
                yb = aa - ya + ab;

                return ya + yb;
            }
        }

        // lnm is a log of a number in the range of 1.0 - 2.0, so 0 <= lnm < ln(2)
        final double[] lnm = lnMant.LN_MANT[(int)((bits & 0x000ffc0000000000L) >> 42)];

        /*
    double epsilon = x / Double.longBitsToDouble(bits & 0xfffffc0000000000L);

    epsilon -= 1.0;
         */

        // y is the most significant 10 bits of the mantissa
        //double y = Double.longBitsToDouble(bits & 0xfffffc0000000000L);
        //double epsilon = (x - y) / y;
        final double epsilon = (bits & 0x3ffffffffffL) / (TWO_POWER_52 + (bits & 0x000ffc0000000000L));

        double lnza = 0.0;
        double lnzb = 0.0;

        if (hiPrec != null) {
            /* split epsilon -> x */
            double tmp = epsilon * HEX_40000000;
            double aa = epsilon + tmp - tmp;
            double ab = epsilon - aa;
            double xa = aa;
            double xb = ab;

            /* Need a more accurate epsilon, so adjust the division. */
            final double numer = bits & 0x3ffffffffffL;
            final double denom = TWO_POWER_52 + (bits & 0x000ffc0000000000L);
            aa = numer - xa*denom - xb * denom;
            xb += aa / denom;

            /* Remez polynomial evaluation */
            final double[] lnCoef_last = LN_HI_PREC_COEF[LN_HI_PREC_COEF.length-1];
            double ya = lnCoef_last[0];
            double yb = lnCoef_last[1];

            for (int i = LN_HI_PREC_COEF.length - 2; i >= 0; i--) {
                /* Multiply a = y * x */
                aa = ya * xa;
                ab = ya * xb + yb * xa + yb * xb;
                /* split, so now y = a */
                tmp = aa * HEX_40000000;
                ya = aa + tmp - tmp;
                yb = aa - ya + ab;

                /* Add  a = y + lnHiPrecCoef */
                final double[] lnCoef_i = LN_HI_PREC_COEF[i];
                aa = ya + lnCoef_i[0];
                ab = yb + lnCoef_i[1];
                /* Split y = a */
                tmp = aa * HEX_40000000;
                ya = aa + tmp - tmp;
                yb = aa - ya + ab;
            }

            /* Multiply a = y * x */
            aa = ya * xa;
            ab = ya * xb + yb * xa + yb * xb;

            /* split, so now lnz = a */
            /*
      tmp = aa * 1073741824.0;
      lnza = aa + tmp - tmp;
      lnzb = aa - lnza + ab;
             */
            lnza = aa + ab;
            lnzb = -(lnza - aa - ab);
        } else {
            /* High precision not required.  Eval Remez polynomial
         using standard double precision */
            lnza = -0.16624882440418567;
            lnza = lnza * epsilon + 0.19999954120254515;
            lnza = lnza * epsilon + -0.2499999997677497;
            lnza = lnza * epsilon + 0.3333333333332802;
            lnza = lnza * epsilon + -0.5;
            lnza = lnza * epsilon + 1.0;
            lnza = lnza * epsilon;
        }

        /* Relative sizes:
         * lnzb     [0, 2.33E-10]
         * lnm[1]   [0, 1.17E-7]
         * ln2B*exp [0, 1.12E-4]
         * lnza      [0, 9.7E-4]
         * lnm[0]   [0, 0.692]
         * ln2A*exp [0, 709]
         */

        /* Compute the following sum:
         * lnzb + lnm[1] + ln2B*exp + lnza + lnm[0] + ln2A*exp;
         */

        //return lnzb + lnm[1] + ln2B*exp + lnza + lnm[0] + ln2A*exp;
        double a = LN_2_A*exp;
        double b = 0.0;
        double c = a+lnm[0];
        double d = -(c-a-lnm[0]);
        a = c;
        b = b + d;

        c = a + lnza;
        d = -(c - a - lnza);
        a = c;
        b = b + d;

        c = a + LN_2_B*exp;
        d = -(c - a - LN_2_B*exp);
        a = c;
        b = b + d;

        c = a + lnm[1];
        d = -(c - a - lnm[1]);
        a = c;
        b = b + d;

        c = a + lnzb;
        d = -(c - a - lnzb);
        a = c;
        b = b + d;

        if (hiPrec != null) {
            hiPrec[0] = a;
            hiPrec[1] = b;
        }

        return a + b;
    }

    /**
     * Computes log(1 + x).
     *
     * @param x Number.
     * @return {@code log(1 + x)}.
     */
    public static double log1p(final double x) {
        if (x == -1) {
            return Double.NEGATIVE_INFINITY;
        }

        if (x == Double.POSITIVE_INFINITY) {
            return Double.POSITIVE_INFINITY;
        }

        if (x > 1e-6 ||
            x < -1e-6) {
            final double xpa = 1 + x;
            final double xpb = -(xpa - 1 - x);

            final double[] hiPrec = new double[2];
            final double lores = log(xpa, hiPrec);
            if (Double.isInfinite(lores)) { // Don't allow this to be converted to NaN
                return lores;
            }

            // Do a taylor series expansion around xpa:
            //   f(x+y) = f(x) + f'(x) y + f''(x)/2 y^2
            final double fx1 = xpb / xpa;
            final double epsilon = 0.5 * fx1 + 1;
            return epsilon * fx1 + hiPrec[1] + hiPrec[0];
        } else {
            // Value is small |x| < 1e6, do a Taylor series centered on 1.
            final double y = (x * F_1_3 - F_1_2) * x + 1;
            return y * x;
        }
    }

    

    /**
     * Computes the <a href="http://mathworld.wolfram.com/Logarithm.html">
     * logarithm</a> in a given base.
     *
     * Returns {@code NaN} if either argument is negative.
     * If {@code base} is 0 and {@code x} is positive, 0 is returned.
     * If {@code base} is positive and {@code x} is 0,
     * {@code Double.NEGATIVE_INFINITY} is returned.
     * If both arguments are 0, the result is {@code NaN}.
     *
     * @param base Base of the logarithm, must be greater than 0.
     * @param x Argument, must be greater than 0.
     * @return the value of the logarithm, i.e. the number {@code y} such that
     * <code>base<sup>y</sup> = x</code>.
     * @since 1.2 (previously in {@code MathUtils}, moved as of version 3.0)
     */
    public static double log(double base, double x) {
        return log(x) / log(base);
    }
    }
}
