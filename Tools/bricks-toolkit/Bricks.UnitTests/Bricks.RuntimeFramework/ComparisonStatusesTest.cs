using NUnit.Framework;

namespace Bricks.RuntimeFramework
{
    [TestFixture]
    public class ComparisonStatusesTest
    {
        [Test]
        public void Status()
        {
            ComparisonStatuses statuses = new ComparisonStatuses();
            statuses.Add(ComparisonStatus.DontKnow);
            statuses.Add(ComparisonStatus.DontKnow);
            statuses.Add(ComparisonStatus.Eager);
            Assert.AreEqual(Tristate.Eager, statuses.State);
        }
    }
}