using NUnit.Framework;

namespace Bricks.Core
{
    [TestFixture]
    public class MoneyTest
    {
        private Money money = new Money("10000");

        [Test]
        public void Equals()
        {
            Assert.AreEqual(true, money.Equals((object) money));
            Assert.AreEqual(false, new Money("10001").Equals((object) money));
        }

        [Test]
        public void InstantiatingMonies()
        {
            Assert.AreEqual(money, new Money("10000"));
            Assert.AreEqual(money, new Money("  10000  "));
            Assert.AreEqual(money, new Money("£   10000"));
            Assert.AreEqual(money, new Money("£10000"));
            Assert.AreEqual(money, new Money("   £   10000 "));
        }

        [Test]
        public void AsString()
        {
            Assert.AreEqual("10000", money.ToString());
        }

        [Test]
        public void IdentifiesEmptyMonies()
        {
            Assert.AreEqual(true, Money.IsNullOrEmpty(null));
            Assert.AreEqual(true, Money.IsNullOrEmpty(Money.NULL));
            Assert.AreEqual(false, Money.IsNullOrEmpty(new Money(100)));
        }
    }
}