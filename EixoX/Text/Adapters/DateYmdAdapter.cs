using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public class DateYmdAdapter
        : AbstractTextAdapter<DateTime>
    {

        protected override DateTime Parse(string text, IFormatProvider formatProvider)
        {
            int year;
            int month;
            int day;
            int hour = 0;
            int minute = 0;
            int second = 0;

            text = text.Trim();

            switch (text.Length)
            {
                case 4:
                    year = int.Parse(text, formatProvider);
                    month = 12;
                    day = 31;
                    break;
                case 5:
                    year = int.Parse(text.Substring(0, 4), formatProvider);
                    month = int.Parse(text.Substring(4, 1), formatProvider);
                    day = DateTime.DaysInMonth(year, month);
                    break;
                case 6:
                    year = int.Parse(text.Substring(0, 4), formatProvider);
                    month = int.Parse(text.Substring(4, 2), formatProvider);
                    day = DateTime.DaysInMonth(year, month);
                    break;
                case 7:
                    year = int.Parse(text.Substring(0, 4), formatProvider);
                    month = int.Parse(text.Substring(4, 2), formatProvider);
                    day = int.Parse(text.Substring(6, 1), formatProvider);
                    break;
                case 8:
                    year = int.Parse(text.Substring(0, 4), formatProvider);
                    month = int.Parse(text.Substring(4, 2), formatProvider);
                    day = int.Parse(text.Substring(6, 2), formatProvider);
                    break;
                case 9:
                    year = int.Parse(text.Substring(0, 4), formatProvider);
                    month = int.Parse(text.Substring(4, 2), formatProvider);
                    day = int.Parse(text.Substring(6, 2), formatProvider);
                    hour = int.Parse(text.Substring(8, 1), formatProvider);
                    break;
                case 10:
                    year = int.Parse(text.Substring(0, 4), formatProvider);
                    month = int.Parse(text.Substring(4, 2), formatProvider);
                    day = int.Parse(text.Substring(6, 2), formatProvider);
                    hour = int.Parse(text.Substring(8, 2), formatProvider);
                    break;
                case 11:
                    year = int.Parse(text.Substring(0, 4), formatProvider);
                    month = int.Parse(text.Substring(4, 2), formatProvider);
                    day = int.Parse(text.Substring(6, 2), formatProvider);
                    hour = int.Parse(text.Substring(8, 2), formatProvider);
                    minute = int.Parse(text.Substring(10, 1), formatProvider);
                    break;
                case 12:
                    year = int.Parse(text.Substring(0, 4), formatProvider);
                    month = int.Parse(text.Substring(4, 2), formatProvider);
                    day = int.Parse(text.Substring(6, 2), formatProvider);
                    hour = int.Parse(text.Substring(8, 2), formatProvider);
                    minute = int.Parse(text.Substring(10, 2), formatProvider);
                    break;
                case 13:
                    year = int.Parse(text.Substring(0, 4), formatProvider);
                    month = int.Parse(text.Substring(4, 2), formatProvider);
                    day = int.Parse(text.Substring(6, 2), formatProvider);
                    hour = int.Parse(text.Substring(8, 2), formatProvider);
                    minute = int.Parse(text.Substring(11, 2), formatProvider);
                    break;
                case 14:
                    year = int.Parse(text.Substring(0, 4), formatProvider);
                    month = int.Parse(text.Substring(4, 2), formatProvider);
                    day = int.Parse(text.Substring(6, 2), formatProvider);
                    hour = int.Parse(text.Substring(8, 2), formatProvider);
                    minute = int.Parse(text.Substring(10, 2), formatProvider);
                    second = int.Parse(text.Substring(12, 2), formatProvider);
                    break;
                case 16:
                    year = int.Parse(text.Substring(0, 4), formatProvider);
                    month = int.Parse(text.Substring(4, 2), formatProvider);
                    day = int.Parse(text.Substring(6, 2), formatProvider);
                    hour = int.Parse(text.Substring(8, 2), formatProvider);
                    minute = int.Parse(text.Substring(11, 2), formatProvider);
                    second = int.Parse(text.Substring(14, 2), formatProvider);
                    break;
                default:
                    throw new ArgumentException("Cant date ymd parse " + text + " [length invalid]");
            }

            return new DateTime(year, month, day, hour, minute, second);
        }

        protected override string Format(DateTime value, IFormatProvider formatProvider)
        {
            if (value.Hour != 0 || value.Minute != 0 || value.Second != 0)
                return string.Concat(
                    value.Year.ToString("0000"),
                    value.Month.ToString("00"),
                    value.Day.ToString("00"),
                    value.Hour.ToString("00"), ":",
                    value.Minute.ToString("00"), ":",
                    value.Second.ToString("00"));
            else
                return string.Concat(
                    value.Year.ToString("0000"),
                    value.Month.ToString("00"),
                    value.Day.ToString("00"));
        }
    }
}
