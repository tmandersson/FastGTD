using System;
using System.Globalization;

namespace Bricks.Core
{
    [Serializable]
    public class Date : IFormattable, IComparable
    {
        private DateTime dateTime;
        private bool isUnknown = true;
        private static readonly Date minValue = new Date(DateTime.MinValue);
        private static readonly Date maxValue = new Date(DateTime.MaxValue);
        private static readonly Date databaseMinValue = new Date(1753, 1, 1);


        private static readonly string[] validFormats =
            new[]
                {
                    "ddMMyy", "ddMMyyyy", "dd/MM/yy", "dd/MM/yyyy", "dd/M/yy", "dd/M/yyyy", "d/M/yy", "d/M/yyyy", "d/MM/yy", "d/MM/yyyy", "dd MMM yyyy",
                    "yyyy-MM-dd"
                };

        public Date() {}

        public static Date Parse(string dateText)
        {
            string text = S.TrimSafe(dateText);
            if (text.Length == 0)
                return new Date();
            return new Date(DateTime.ParseExact(text, validFormats, CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.None));
        }

        public static Date Parse(string dateText, IFormatProvider formatProvider)
        {
            return new Date(DateTime.Parse(dateText, formatProvider));
        }

        public Date(int year, int month, int day)
        {
            dateTime = new DateTime(year, month, day);
            isUnknown = false;
        }

        public Date(int year, Month month, int day)
        {
            dateTime = new DateTime(year, (int) month, day);
            isUnknown = false;
        }

        public Date(DateTime dateTime)
        {
            if (dateTime.Equals(DateTime.MinValue))
                isUnknown = true;
            else
            {
                this.dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
                isUnknown = false;
            }
        }

        public Date(Date date)
        {
            if (date != null)
            {
                dateTime = date.dateTime;
                isUnknown = date.IsUnknown;
            }
            else isUnknown = true;
        }

        public Date(DateTime? dateTime)
        {
            if (dateTime == null)
                isUnknown = true;
            else
            {
                this.dateTime = new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);
                isUnknown = false;
            }
        }

        public virtual bool IsUnknown
        {
            get { return isUnknown; }
        }

        public virtual int MonthAsInt
        {
            get { return dateTime.Month; }
        }

        public virtual Month Month
        {
            get { return (Month) dateTime.Month; }
        }

        public virtual DateTime ToDateTime()
        {
            if (isUnknown) return DateTime.MinValue;
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public virtual DateTime? ToNullableDateTime()
        {
            return isUnknown ? (DateTime?) null : ToDateTime();
        }

        public static object ToDataBaseValue(Date value)
        {
            if (value == null || value.IsUnknown || value.ToDateTime() < new DateTime(1753, 1, 1) || value.ToDateTime() > new DateTime(9999, 12, 31))
                return DBNull.Value;
            else
                return value.ToDateTime();
        }

        public static Date Today
        {
            get { return new Date(DateTime.Today); }
        }

        public static Date DatabaseMinValue
        {
            get { return databaseMinValue; }
        }

        public static Date MinValue
        {
            get { return minValue; }
        }

        public static Date MaxValue
        {
            get { return maxValue; }
        }

        public virtual Date FirstDayOfMonth
        {
            get { return new Date(dateTime.Year, dateTime.Month, 1); }
        }

        public virtual DayOfWeek DayOfWeek
        {
            get { return dateTime.DayOfWeek; }
        }

        public virtual int Year
        {
            get { return dateTime.Year; }
        }

        public virtual int Day
        {
            get { return dateTime.Day; }
        }

        public virtual bool IsKnown
        {
            get { return !isUnknown; }
        }

        public virtual string UnseparatedString
        {
            get
            {
                if (isUnknown) return "";
                return dateTime.ToString("yyyyMMdd");
            }
        }

        public virtual bool IsFuture
        {
            get { return IsKnown && this > Today; }
        }

        public virtual bool IsPast
        {
            get { return IsKnown && this < Today; }
        }

        public virtual bool IsTodayOrPast
        {
            get { return IsPast || Equals(Today); }
        }

        public static Date FirstDayOfThisMonth
        {
            get { return new Date(DateTime.Now.Year, DateTime.Now.Month, 1); }
        }

        public static Date LastDayOfThisMonth
        {
            get { return FirstDayOfThisMonth.AddMonths(1).AddDays(-1); }
        }

        public virtual Date AddMonths(int months)
        {
            if (IsUnknown) return new Date();
            return new Date(dateTime.AddMonths(months));
        }

        public virtual Date AddDays(int days)
        {
            if (IsUnknown) return new Date();
            return new Date(dateTime.AddDays(days));
        }

        public virtual Date AddYears(int years)
        {
            if (IsUnknown) return new Date();
            return new Date(dateTime.AddYears(years));
        }

        public virtual string ToString(string format)
        {
            if (IsUnknown) return "";
            return ToDateTime().ToString(format);
        }

        public virtual int PeriodInMonths(Date value)
        {
            if (IsUnknown || value.IsUnknown) return 0;

            DateTime otherDateTime = value.ToDateTime();
            if (dateTime > otherDateTime)
                return PeriodInMonths(otherDateTime, dateTime);
            else
                return PeriodInMonths(dateTime, otherDateTime);
        }

        private int PeriodInMonths(DateTime fromDateTime, DateTime toDateTime)
        {
            if (fromDateTime.Month > toDateTime.Month)
                return (int) Month.December + toDateTime.Month - fromDateTime.Month + 1;
            else
                return toDateTime.Month - fromDateTime.Month + 1;
        }

        public override string ToString()
        {
            return AsString();
        }

        public virtual string AsString()
        {
            return isUnknown ? S.UNKNOWN : ToString("dd/MM/yyyy");
        }

        public virtual string ToLongDateTimeString()
        {
            return isUnknown ? S.UNKNOWN : dateTime.ToLongDateString() + dateTime.ToString(" HH:mm");
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            Date date = obj as Date;
            if (date == null) return false;
            if (IsUnknown && date.IsUnknown) return true;
            if (dateTime.Date.Equals(date.dateTime.Date))
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            if (isUnknown) return isUnknown.GetHashCode();
            return dateTime.Date.GetHashCode();
        }

        #region operator overloads

        public static bool operator >=(Date x, Date y)
        {
            return x.dateTime >= y.dateTime;
        }

        public static bool operator <=(Date x, Date y)
        {
            return x.dateTime <= y.dateTime;
        }

        public static bool operator >=(DateTime x, Date y)
        {
            return new Date(x) >= y;
        }

        public static bool operator <=(DateTime x, Date y)
        {
            return new Date(x) <= y;
        }

        public static bool operator >=(Date x, DateTime y)
        {
            return x >= new Date(y);
        }

        public static bool operator <=(Date x, DateTime y)
        {
            return x <= new Date(y);
        }

        public static bool operator >(Date x, DateTime y)
        {
            return x > new Date(y);
        }

        public static bool operator <(Date x, DateTime y)
        {
            return x < new Date(y);
        }

        public static bool operator <(Date x, Date y)
        {
            return x.dateTime < y.dateTime;
        }

        public static bool operator >(Date x, Date y)
        {
            return x.dateTime > y.dateTime;
        }

        public static implicit operator Date(string dateAsString)
        {
            return Parse(dateAsString);
        }

        public static implicit operator Date(DateTime? dateTime)
        {
            return new Date(dateTime);
        }

        #endregion

        public virtual Date ToLocalTime()
        {
            return new Date(dateTime.ToLocalTime());
        }

        public virtual string ToShortDateString()
        {
            if (IsUnknown) return "";
            return dateTime.ToShortDateString();
        }

        public virtual string ToString(string format, IFormatProvider formatProvider)
        {
            if (IsUnknown) return "";
            return dateTime.ToString(format, formatProvider);
        }

        public virtual int CompareTo(object obj)
        {
            if (!(obj is Date)) return 1;
            Date date = (Date) obj;
            if (IsUnknown && date.IsUnknown) return 0;
            if (IsUnknown && !date.IsUnknown) return -1;
            if (!IsUnknown && date.IsUnknown) return 1;
            return dateTime.CompareTo(date.dateTime);
        }

        public static bool IsNullOrEmpty(Date date)
        {
            return date == null || date.isUnknown;
        }

        public virtual string InStandardNotation()
        {
            return isUnknown ? S.UNKNOWN : ToString("yyyy/MMM/dd");
        }

        public static string DisplayDate(Date date)
        {
            return date == null || date.IsUnknown ? string.Empty : date.ToString("dd-MMM-yyyy");
        }
    }
}