using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Mathematica.Statistics
{
    public class GaussianDistribution
    {

        public static double Calc(double x, double mi, double theta)
        {
            double po = (mi - theta);
            return (1.0 / (Math.Sqrt(2 * Math.PI * theta * theta))) /
                Math.Exp((-1.0 / (2 * theta * theta)) * (po * po));
        }
    }
}
