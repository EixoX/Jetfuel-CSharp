using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica
{
    public struct Angle
    {
        private double _Value;

        public Angle(double value)
        {
            this._Value = value;
        }

        public double Value
        {
            get { return this._Value; }
            set { this._Value = value; }
        }

        /**
         *  Convert degrees to radians, with error of less than 0.5 ULP
         *  @param x angle in degrees
         *  @return x converted into radians
         */
        public static double ToRadians(double x)
        {
            if (Double.IsInfinity(x) || x == 0.0)
            { // Matches +/- 0.0; return correct sign
                return x;
            }

            // These are PI/180 split into high and low order bits
            double facta = 0.01745329052209854;
            double factb = 1.997844754509471E-9;

            double xa = BitOps.HighPart(x);
            double xb = x - xa;

            double result = xb * factb + xb * facta + xa * factb + xa * facta;
            if (result == 0)
            {
                result = result * x; // ensure correct sign if calculation underflows
            }
            return result;
        }

        /**
         *  Convert radians to degrees, with error of less than 0.5 ULP
         *  @param x angle in radians
         *  @return x converted into degrees
         */
        public static double toDegrees(double x)
        {
            if (Double.IsInfinity(x) || x == 0.0)
            { // Matches +/- 0.0; return correct sign
                return x;
            }

            // These are 180/PI split into high and low order bits
            double facta = 57.2957763671875;
            double factb = 3.145894820876798E-6;

            double xa = BitOps.HighPart(x);
            double xb = x - xa;

            return xb * factb + xb * facta + xa * factb + xa * facta;
        }
    }
}
