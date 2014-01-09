using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public class Power
    {
        /**
     * Power function.  Compute x^y.
     *
     * @param x   a double
     * @param y   a double
     * @return double
     */
        public static double pow(double x, double y) {
        final double lns[] = new double[2];

        if (y == 0.0) {
            return 1.0;
        }

        if (x != x) { // X is NaN
            return x;
        }


        if (x == 0) {
            long bits = Double.doubleToLongBits(x);
            if ((bits & 0x8000000000000000L) != 0) {
                // -zero
                long yi = (long) y;

                if (y < 0 && y == yi && (yi & 1) == 1) {
                    return Double.NEGATIVE_INFINITY;
                }

                if (y > 0 && y == yi && (yi & 1) == 1) {
                    return -0.0;
                }
            }

            if (y < 0) {
                return Double.POSITIVE_INFINITY;
            }
            if (y > 0) {
                return 0.0;
            }

            return Double.NaN;
        }

        if (x == Double.POSITIVE_INFINITY) {
            if (y != y) { // y is NaN
                return y;
            }
            if (y < 0.0) {
                return 0.0;
            } else {
                return Double.POSITIVE_INFINITY;
            }
        }

        if (y == Double.POSITIVE_INFINITY) {
            if (x * x == 1.0) {
                return Double.NaN;
            }

            if (x * x > 1.0) {
                return Double.POSITIVE_INFINITY;
            } else {
                return 0.0;
            }
        }

        if (x == Double.NEGATIVE_INFINITY) {
            if (y != y) { // y is NaN
                return y;
            }

            if (y < 0) {
                long yi = (long) y;
                if (y == yi && (yi & 1) == 1) {
                    return -0.0;
                }

                return 0.0;
            }

            if (y > 0)  {
                long yi = (long) y;
                if (y == yi && (yi & 1) == 1) {
                    return Double.NEGATIVE_INFINITY;
                }

                return Double.POSITIVE_INFINITY;
            }
        }

        if (y == Double.NEGATIVE_INFINITY) {

            if (x * x == 1.0) {
                return Double.NaN;
            }

            if (x * x < 1.0) {
                return Double.POSITIVE_INFINITY;
            } else {
                return 0.0;
            }
        }

        /* Handle special case x<0 */
        if (x < 0) {
            // y is an even integer in this case
            if (y >= TWO_POWER_53 || y <= -TWO_POWER_53) {
                return pow(-x, y);
            }

            if (y == (long) y) {
                // If y is an integer
                return ((long)y & 1) == 0 ? pow(-x, y) : -pow(-x, y);
            } else {
                return Double.NaN;
            }
        }

        /* Split y into ya and yb such that y = ya+yb */
        double ya;
        double yb;
        if (y < 8e298 && y > -8e298) {
            double tmp1 = y * HEX_40000000;
            ya = y + tmp1 - tmp1;
            yb = y - ya;
        } else {
            double tmp1 = y * 9.31322574615478515625E-10;
            double tmp2 = tmp1 * 9.31322574615478515625E-10;
            ya = (tmp1 + tmp2 - tmp1) * HEX_40000000 * HEX_40000000;
            yb = y - ya;
        }

        /* Compute ln(x) */
        final double lores = log(x, lns);
        if (Double.isInfinite(lores)){ // don't allow this to be converted to NaN
            return lores;
        }

        double lna = lns[0];
        double lnb = lns[1];

        /* resplit lns */
        double tmp1 = lna * HEX_40000000;
        double tmp2 = lna + tmp1 - tmp1;
        lnb += lna - tmp2;
        lna = tmp2;

        // y*ln(x) = (aa+ab)
        final double aa = lna * ya;
        final double ab = lna * yb + lnb * ya + lnb * yb;

        lna = aa+ab;
        lnb = -(lna - aa - ab);

        double z = 1.0 / 120.0;
        z = z * lnb + (1.0 / 24.0);
        z = z * lnb + (1.0 / 6.0);
        z = z * lnb + 0.5;
        z = z * lnb + 1.0;
        z = z * lnb;

        final double result = exp(lna, z, null);
        //result = result + result * z;
        return result;
    }


        /**
         * Raise a double to an int power.
         *
         * @param d Number to raise.
         * @param e Exponent.
         * @return d<sup>e</sup>
         * @since 3.1
         */
        public static double pow(double d, int e) {

        if (e == 0) {
            return 1.0;
        } else if (e < 0) {
            e = -e;
            d = 1.0 / d;
        }

        // split d as two 26 bits numbers
        // beware the following expressions must NOT be simplified, they rely on floating point arithmetic properties
        final int splitFactor = 0x8000001;
        final double cd       = splitFactor * d;
        final double d1High   = cd - (cd - d);
        final double d1Low    = d - d1High;

        // prepare result
        double resultHigh = 1;
        double resultLow  = 0;

        // d^(2p)
        double d2p     = d;
        double d2pHigh = d1High;
        double d2pLow  = d1Low;

        while (e != 0) {

            if ((e & 0x1) != 0) {
                // accurate multiplication result = result * d^(2p) using Veltkamp TwoProduct algorithm
                // beware the following expressions must NOT be simplified, they rely on floating point arithmetic properties
                final double tmpHigh = resultHigh * d2p;
                final double cRH     = splitFactor * resultHigh;
                final double rHH     = cRH - (cRH - resultHigh);
                final double rHL     = resultHigh - rHH;
                final double tmpLow  = rHL * d2pLow - (((tmpHigh - rHH * d2pHigh) - rHL * d2pHigh) - rHH * d2pLow);
                resultHigh = tmpHigh;
                resultLow  = resultLow * d2p + tmpLow;
            }

            // accurate squaring d^(2(p+1)) = d^(2p) * d^(2p) using Veltkamp TwoProduct algorithm
            // beware the following expressions must NOT be simplified, they rely on floating point arithmetic properties
            final double tmpHigh = d2pHigh * d2p;
            final double cD2pH   = splitFactor * d2pHigh;
            final double d2pHH   = cD2pH - (cD2pH - d2pHigh);
            final double d2pHL   = d2pHigh - d2pHH;
            final double tmpLow  = d2pHL * d2pLow - (((tmpHigh - d2pHH * d2pHigh) - d2pHL * d2pHigh) - d2pHH * d2pLow);
            final double cTmpH   = splitFactor * tmpHigh;
            d2pHigh = cTmpH - (cTmpH - tmpHigh);
            d2pLow  = d2pLow * d2p + tmpLow + (tmpHigh - d2pHigh);
            d2p     = d2pHigh + d2pLow;

            e = e >> 1;

        }

        return resultHigh + resultLow;

    }
    }
}
