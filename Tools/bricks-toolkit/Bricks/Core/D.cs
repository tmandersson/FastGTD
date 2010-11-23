using System;

namespace Bricks.Core
{
    public class D
    {
        public static bool IsEmpty(DateTime? dateTime)
        {
            return dateTime == null || dateTime.Value == DateTime.MinValue;
        }

        public static Date UnknownIfNull(object o)
        {
            return o == DBNull.Value ? new Date() : new Date((DateTime) o);
        }

        public static bool IsEmpty(Date date)
        {
            return date == null || date.IsUnknown;
        }

        public static DateTime Convert(DateTime? dateTime)
        {
            return dateTime ?? DateTime.MinValue;
        }

        public static DateTime? Convert(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue) return null;
            return dateTime;
        }

        public static string Format(DateTime date)
        {
            return String.Format("{0:dd/MM/yyyy}", date);
        }

        public static string FormatDateTime(DateTime date)
        {
            return String.Format("{0:dd/MM/yyyy hh:mm}", date);
        }

        public static string FormatDateTimeToTwentyFourHour(DateTime date)
        {
            return String.Format("{0:HH:mm:ss dd/MM/yyyy}", date);
        }

        public static string ToLongDateTimeString(DateTime dateTime)
        {
            return dateTime.ToLongDateString() + dateTime.ToString(" HH:mm");
        }
    }
}