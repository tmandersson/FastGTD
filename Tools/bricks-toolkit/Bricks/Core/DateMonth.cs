using System;

namespace Bricks.Core
{
    [Serializable]
    public class DateMonth
    {
        private Span span;
        private int spanValue;
        private Month month = Month.January;

        public DateMonth() {}

        public DateMonth(Span span)
        {
            this.span = span;
        }

        public DateMonth(Span span, int spanValue, Month month)
        {
            this.span = span;
            this.spanValue = spanValue;
            this.month = month;
        }

        public virtual Span Span
        {
            get { return span; }
            set { span = value; }
        }

        public virtual int SpanValue
        {
            get { return spanValue; }
            set { spanValue = value; }
        }

        public virtual Month Month
        {
            get { return month; }
            set { month = value; }
        }

        public virtual int GetDaysInMonth(int year)
        {
            return DateTime.DaysInMonth(year, (int) month);
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            if (!(obj is DateMonth)) return false;
            return Equals((DateMonth) obj);
        }

        public virtual bool Equals(DateMonth other)
        {
            return span == other.span && spanValue == other.spanValue && month.Equals(other.month);
        }

        public override string ToString()
        {
            return spanValue + " " + month.ToString();
        }

        public override int GetHashCode()
        {
            return 5*spanValue + month.GetHashCode()*7 + 11*(int) span;
        }
    }

    public enum Span
    {
        DateOfMonth,
        Year
    }
}