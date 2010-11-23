using System;
using NUnit.Framework;

namespace Bricks.Core
{
    [TestFixture]
    public class DTest
    {
        [Test]
        public void IsEmpty()
        {
            DateTime? d = null;
            Assert.IsTrue(D.IsEmpty(d));
            Assert.IsTrue(D.IsEmpty(DateTime.MinValue));
            Assert.IsFalse(D.IsEmpty(DateTime.Today));
        }

        [Test]
        public void ShouldGetDateTimeFromNullableDateTime()
        {
            Assert.AreEqual(DateTime.MinValue, D.Convert(null));
            DateTime? today = DateTime.Today;
            Assert.AreEqual(today.Value, D.Convert(today));
        }

        [Test]
        public void ShouldGetNullableDateTimeFromDateTime()
        {
            Assert.IsNull(D.Convert(DateTime.MinValue));
            DateTime today = DateTime.Today;
            Assert.AreEqual(today, D.Convert(today).Value);
        }

        [Test]
        public void LongDateTimeString()
        {
            DateTime dateTime = new DateTime(2006, 1, 2, 3, 4, 5);
            Assert.AreEqual(dateTime.ToLongDateString() + " 03:04", D.ToLongDateTimeString(dateTime));
        }
    }
}