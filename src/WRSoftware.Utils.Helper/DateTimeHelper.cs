using System;
using System.Globalization;

namespace WRSoftware.Utils.Helper
{
    /// <summary>
    /// Helper for the Datetime, with some conventions
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime">The date time. With the Patern
        /// "yyyy-MM-ddTHH:mm:ss.fffzzz" / "2020-04-18T12:37:44.514-03:00" as GMT -3
        /// "yyyy-MM-dd HH:mm:ss.fffzzz" / "2020-04-18 12:37:44.514-03:00" 
        /// "yyyy-MM-ddTHH:mm:sszzz" / "2020-04-18T12:37:44-03:00" 
        /// "yyyy-MM-dd HH:mm:sszzz" / "2020-04-18 12:37:44-03:00" 
        /// "yyyy-MM-ddTHH:mmzzz" / "2020-04-18T12:37-03:00" 
        /// "yyyy-MM-dd HH:mmzzz" / "2020-04-18 12:37-03:00" </param>
        /// <returns>The DateTime as UTC</returns>
        public static DateTime ToDateTimeUTC(string dateTime)
        {
            DateTime date = DateTime.Parse(dateTime).ToUniversalTime();
            return date;
        }


        /// <summary>
        /// Converts to datetimeutc.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime ToDateTimeUTC(DateTime dateTime)
        {
            DateTime date = DateTime.Parse(dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz")).ToUniversalTime();
            return date;
        }


        /// <summary>
        /// Converts to datetime.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime ToDateTimeWithoutZone(string dateTime)
        {
            if (dateTime.Length > 16)
            {
                if (dateTime.Contains("+"))
                {
                    dateTime = dateTime.Remove(dateTime.LastIndexOf("+"));
                }
                else
                {
                    dateTime = dateTime.Remove(dateTime.LastIndexOf("-"));
                }
            }

            DateTime date = DateTime.Parse(dateTime, CultureInfo.InvariantCulture);
            return date;
        }


        /// <summary>
        /// Converts to datetimelocal.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string dateTime)
        {
            DateTime date = DateTime.Parse(dateTime);
            return date;
        }

        /// <summary>
        /// Formats the date start date UTC.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The Datetime as the Star of Day</returns>
        /// <exception cref="System.Exception">Input Date, should be in format yyyy-MM-dd</exception>
        public static DateTime FormatDateStartDateUTC(string dateTime)
        {
            var date = ToDateTimeUTC(dateTime);

            return date.ChangeTime(0, 0, 0, 0);
        }

        /// <summary>
        /// Formats the date end date UTC.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The Datetime as the End of Day</returns>
        /// <exception cref="System.Exception">Input Date, should be in format yyyy-MM-dd</exception>
        public static DateTime FormatDateEndDateUTC(string dateTime)
        {
            var date = ToDateTimeUTC(dateTime);

            return ChangeTime(date, 23, 59, 59, 999);
        }


        /// <summary>
        /// Formats the date start date.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime FormatDateStartDate(string dateTime)
        {
            var date = ToDateTime(dateTime);

            return ChangeTime(date, 0, 0, 0, 0);
        }


        /// <summary>
        /// Formats the date end date.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime FormatDateEndDate(string dateTime)
        {
            var date = ToDateTime(dateTime);

            return ChangeTime(date, 23, 59, 59, 999);
        }


        /// <summary>
        /// Locals the time.
        /// </summary>
        /// <returns>Local date with the Machine current TimeZone</returns>
        public static DateTime LocalTime()
        {
            return DateTime.Now.ToLocalTime();
        }

        /// <summary>
        /// Locals the time to UTC.
        /// </summary>
        /// <returns>Local date as UTC</returns>
        public static DateTime LocalTimeToUtc()
        {
            return DateTime.Now.ToUniversalTime();
        }


        /// <summary>
        /// The Datetime from the Epoch using the UTC Time
        /// </summary>
        /// <param name="epochSeconds">The epoch seconds.</param>
        /// <returns>The Datetime from the number of Seconds</returns>
        public static DateTime FromEpochUTC(long epochSeconds)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(epochSeconds);
        }


        /// <summary>
        /// Dates to string.
        /// </summary>
        /// <param name="dateTime">The date time</param>
        /// <returns>Date as String in Format yyyy-MM-dd</returns>
        public static string DateToString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }


        /// <summary>
        /// Dates the time to string.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The DateTime as string in format yyyy-MM-dd HH:mm:ss</returns>
        public static string DateTimeToString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }


        /// <summary>
        /// Dates the time to string milliseconds.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string DateTimeToStringMilliseconds(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// Dates the time to string milliseconds z.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string DateTimeToStringMillisecondsZ(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
        }

        /// <summary>
        /// Dates the time to string wiht TimeZone.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string DateTimeToStringZ(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }

        /// <summary>
        /// Return as string with Date and Time with the pattern of Culture.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public static string DateTimeToCulture(DateTime dateTime, string culture)
        {
            return dateTime.ToString(new CultureInfo(culture));
        }

        /// <summary>
        /// Dates the time start to culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public static string DateTimeStartToCulture(string date, string culture)
        {
            var dateTime = FormatDateStartDate(date);

            return dateTime.ToString(new CultureInfo(culture));
        }

        /// <summary>
        /// Dates the time end to culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public static string DateTimeEndToCulture(string date, string culture)
        {
            var dateTime = FormatDateEndDate(date);

            return dateTime.ToString(new CultureInfo(culture));
        }


        /// <summary>
        /// Dates the time start UTC to culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public static string DateTimeStartUTCToCulture(string date, string culture)
        {
            var dateTime = FormatDateStartDateUTC(date);

            return dateTime.ToString(new CultureInfo(culture));
        }

        /// <summary>
        /// Dates the time end UTC to culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public static string DateTimeEndUTCToCulture(string date, string culture)
        {
            var dateTime = FormatDateEndDateUTC(date);

            return dateTime.ToString(new CultureInfo(culture));
        }

        /// <summary>
        /// Changes the time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="hours">The hours.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="seconds">The seconds.</param>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns></returns>
        public static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }


        /// <summary>
        /// Truncates the specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="resolution">The resolution.</param>
        /// <returns></returns>
        ////private static DateTime Truncate(DateTime date, long resolution)
        ////{
        ////    return new DateTime(date.Ticks - (date.Ticks % resolution), date.Kind);
        ////}
    }
}