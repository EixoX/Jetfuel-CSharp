using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public static class DateTimeHelper
    {

        public static int GetAgeInYears(DateTime big, DateTime small)
        {
            if (
                (big.Month > small.Month) ||
                (big.Month == small.Month && big.Day >= small.Day))
            {
                return big.Year - small.Year;
            }
            else
            {
                return big.Year - small.Year - 1;
            }

        }
    }
}
