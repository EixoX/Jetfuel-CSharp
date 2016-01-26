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

        public static long UnixTime(DateTime dt)
        {
            return (long)(dt.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static bool TryParseYMD(string input, DateTime defaultDate, out DateTime date)
        {
            if (string.IsNullOrEmpty(input))
            {
                date = defaultDate;
                return false;
            }

            input = input.Trim();
            try
            {
                switch (input.Length)
                {
                    case 0:
                        date = defaultDate;
                        return false;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        date = new DateTime(int.Parse(input), 1, 1);
                        return true;
                    case 5:
                        date = new DateTime(
                            int.Parse(input.Substring(0, 4)),
                            int.Parse(input.Substring(4, 1)),
                            1);
                        return true;
                    case 6:
                        date = new DateTime(
                            int.Parse(input.Substring(0, 2)),
                            int.Parse(input.Substring(2, 2)),
                            int.Parse(input.Substring(4, 2)));
                        return true;
                    case 7:
                        date = new DateTime(
                            int.Parse(input.Substring(0, 4)),
                            int.Parse(input.Substring(4, 2)),
                            int.Parse(input.Substring(6, 1)));
                        return true;
                    default:
                        date = new DateTime(
                            int.Parse(input.Substring(0, 4)),
                            int.Parse(input.Substring(4, 2)),
                            int.Parse(input.Substring(6, 2)));
                        return true;
                }
            }
            catch
            {
                date = defaultDate;
                return false;
            }


        }

        public static bool TryAppendTimeHMS(DateTime date, string input, DateTime defaultDate, out DateTime dateOutput)
        {
            if (string.IsNullOrEmpty(input))
            {
                dateOutput = defaultDate;
                return false;
            }

            input = input.Trim();
            if (input.Length == 0)
            {
                dateOutput = defaultDate;
                return false;
            }

            try
            {
                switch (input.Length)
                {
                    case 1:
                    case 2:
                        dateOutput = date.AddHours(int.Parse(input));
                        return true;
                    case 3:
                        dateOutput = date.AddHours(int.Parse(input.Substring(0, 2)));
                        dateOutput = dateOutput.AddMinutes(int.Parse(input.Substring(2, 1)));
                        return true;
                    case 4:
                        dateOutput = date.AddHours(int.Parse(input.Substring(0, 2)));
                        dateOutput = dateOutput.AddMinutes(int.Parse(input.Substring(2, 2)));
                        return true;
                    case 5:
                        dateOutput = date.AddHours(int.Parse(input.Substring(0, 2)));
                        dateOutput = dateOutput.AddMinutes(int.Parse(input.Substring(2, 2)));
                        dateOutput = dateOutput.AddSeconds(int.Parse(input.Substring(4, 1)));
                        return true;
                    default:
                        dateOutput = date.AddHours(int.Parse(input.Substring(0, 2)));
                        dateOutput = dateOutput.AddMinutes(int.Parse(input.Substring(2, 2)));
                        dateOutput = dateOutput.AddSeconds(int.Parse(input.Substring(4, 2)));
                        return true;
                }
            }
            catch
            {
                dateOutput = defaultDate;
                return false;
            }
        }
        
        public static bool TryParse(string input, IFormatProvider formatProvider, DateTime defaultDate, out DateTime date)
        {

            if (string.IsNullOrEmpty(input))
            {
                date = defaultDate;
                return false;
            }
            else
            {
                if (DateTime.TryParse(input, formatProvider, System.Globalization.DateTimeStyles.None, out date))
                    return true;

                if (input.IndexOf(' ') > 0)
                {
                    string[] dateAndTime = input.Split(' ');
                    if (dateAndTime.Length == 0)
                        return false;
                    else
                    {
                        if (!TryParseYMD(dateAndTime[0], defaultDate, out date))
                            return false;

                        if (dateAndTime.Length > 1)
                            TryAppendTimeHMS(date, dateAndTime[1], date, out date);
                        return true;

                    }
                }
                else
                    return TryParseYMD(input, defaultDate, out date);
            }
        }

        public static DateTime EnsureBusinessDay(DateTime date, bool up)
        {
            return EnsureBusinessDay(date, up ? 1 : -1);
        }

        public static DateTime EnsureBusinessDay(DateTime date, int direction)
        {
            if (direction != 1 && direction != -1)
                throw new ArgumentException("Direction must be 1 or -1");
            
            if (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday)
                return date;
            
            return EnsureBusinessDay(date.AddDays(direction), direction);
        }
    }
}