using NUnit.Framework;

namespace Bricks.UnitTests.Bricks
{
    [TestFixture]
    public class CoreAppXmlConfigurationTest
    {
        [Test]
        public void SectionGroupAndSectionNameIsSame()
        {
            int milliseconds = CoreAppXmlConfiguration.Instance.RecheckDurationInMilliseconds;
            Assert.AreEqual(101, milliseconds);
        }
    }
}