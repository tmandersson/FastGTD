using System;
using Bricks.Core;
using NUnit.Framework;

namespace Bricks.UnitTests.Bricks.Core
{
    [TestFixture]
    public class DateTest
    {
        private readonly Date x = new Date(2006, 1, 1);
        private readonly Date y = new Date(2006, 1, 1);
        private readonly Date z = new Date(2006, 1, 2);

        [Test]
        public void NewDatehasNoText()
        {
            Assert.AreEqual("", new Date().ToString());
        }

        [Test]
        public void FutureDate()
        {
            Date date = Date.Today;
            Assert.AreEqual(false, date.IsFuture);
            Assert.AreEqual(true, date.AddDays(10).IsFuture);
        }

        [Test]
        public void CannotCompareObjectsOfDifferentTypes()
        {
            Assert.IsFalse(new Date().Equals(new object()));
        }

        [Test]
        public void CannotCompareDateToNull()
        {
            Assert.IsFalse(new Date().Equals(null));
        }

        [Test]
        public void TwoDatesWithTheSameDateAreEqual()
        {
            Assert.AreEqual(x, y);
        }

        [Test]
        public void TwoDatesWithDifferentDatesAreNotEqual()
        {
            Assert.AreNotEqual(x, z);
        }

        [Test]
        public void CompareTwoDates()
        {
            Date d1 = new Date(1954, 3, 14);
            Date d2 = Date.Today.AddMonths(-12);
            Assert.IsTrue(d1.CompareTo(d2) < 0);
        }

        [Test]
        public void CanAssignStringToDate()
        {
            Date date;
            DateTime.Parse("12 Dec 2006").GetDateTimeFormats();
            date = "12 Dec 2006";
            Assert.AreEqual(new Date(2006, 12, 12), date);
        }

        [Test]
        public void UnseperatedDateString()
        {
            Date date = new Date(2006, 12, 13);
            Assert.AreEqual("20061213", date.UnseparatedString);
        }

        [Test]
        public void GetPeriodsInMonths()
        {
            Date date1 = new Date(2006, Month.July, 25);
            Date date2 = new Date(2006, Month.August, 30);
            Assert.AreEqual(2, date1.PeriodInMonths(date2));

            date1 = new Date(2007, Month.February, 25);
            date2 = new Date(2006, Month.August, 30);
            Assert.AreEqual(7, date1.PeriodInMonths(date2));

            date1 = new Date(2006, Month.October, 25);
            date2 = new Date(2006, Month.August, 30);
            Assert.AreEqual(3, date1.PeriodInMonths(date2));

            date1 = new Date(2006, Month.October, 25);
            date2 = new Date(2006, Month.October, 30);
            Assert.AreEqual(1, date1.PeriodInMonths(date2));

            Date nullDate = new Date();
            date2 = new Date(2006, Month.August, 30);
            Assert.AreEqual(0, nullDate.PeriodInMonths(date2));

            date1 = new Date(2006, Month.August, 30);
            Assert.AreEqual(0, date1.PeriodInMonths(nullDate));

            Assert.AreEqual(0, new Date().PeriodInMonths(nullDate));
        }

        [Test]
        public void ConvertToDatabaseValue()
        {
            Assert.AreEqual(DBNull.Value, Date.ToDataBaseValue(null));
            Assert.AreEqual(DBNull.Value, Date.ToDataBaseValue(new Date()));
            Assert.AreEqual(DBNull.Value, Date.ToDataBaseValue(new Date(1751, Month.January, 1)));
            var extremeDate = new Date(9999, Month.December, 31);
            Assert.AreEqual(extremeDate.ToDateTime(), Date.ToDataBaseValue(extremeDate));
        }

        [Test, Ignore]
        public void DateParse()
        {
            Assert.AreEqual(x, Date.Parse("010106"));
            Assert.AreEqual(x, Date.Parse("01012006"));
            Assert.AreEqual(x, Date.Parse("01/01/2006"));
            Assert.AreEqual(x, Date.Parse("1/1/2006"));
            Assert.AreEqual(x, Date.Parse("1/1/06"));
            Assert.AreEqual(x, Date.Parse("01/1/06"));
            Assert.AreEqual(x, Date.Parse("01/1/2006"));
            Assert.AreEqual(x, Date.Parse("1/01/06"));
            Assert.AreEqual(x, Date.Parse("1/01/2006"));
        }

        [Test]
        public void FirstDayOfMonth()
        {
            Assert.AreEqual(new Date(2006, Month.May, 1), new Date(2006, Month.May, 11).FirstDayOfMonth);
            Assert.AreEqual(new Date(2006, Month.May, 1), new Date(2006, Month.May, 31).FirstDayOfMonth);
        }

        [Test, ExpectedException(typeof (FormatException))]
        public void InvalidDate()
        {
            Date.Parse("ss");
        }

        [Test, Ignore]
        public void ShouldDisplayLongDateString()
        {
            var date = new Date(2006, Month.January, 2);
            Assert.AreEqual("02/01/2006", date.ToString());
        }

        [Test]
        public void AcceptsDatesInTheInvariantCultureFormat()
        {
            Assert.AreEqual(new Date(2006, Month.February, 26), Date.Parse("2006-02-26"));
        }
    }
}