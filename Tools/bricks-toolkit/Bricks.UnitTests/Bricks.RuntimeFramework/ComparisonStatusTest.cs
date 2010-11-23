using NUnit.Framework;

namespace Bricks.RuntimeFramework
{
    [TestFixture]
    public class ComparisonStatusTest
    {
        [Test]
        public void JustOneNoIsEnough()
        {
            Assert.AreEqual(ComparisonStatus.Eager, ComparisonStatus.Lazy.Combine(ComparisonStatus.Eager));
            Assert.AreEqual(ComparisonStatus.Eager, ComparisonStatus.Eager.Combine(ComparisonStatus.Lazy));
            Assert.AreEqual(ComparisonStatus.Eager, ComparisonStatus.Eager.Combine(ComparisonStatus.Eager));
        }

        [Test]
        public void OnlyAllYesIsYes()
        {
            Assert.AreEqual(ComparisonStatus.Lazy, ComparisonStatus.Lazy.Combine(ComparisonStatus.Lazy));
        }

        [Test]
        public void IgnoreMeWhenIDontKnow()
        {
            Assert.AreEqual(ComparisonStatus.Lazy, ComparisonStatus.Lazy.Combine(ComparisonStatus.DontKnow));
            Assert.AreEqual(ComparisonStatus.Lazy, ComparisonStatus.DontKnow.Combine(ComparisonStatus.Lazy));
            Assert.AreEqual(ComparisonStatus.Eager, ComparisonStatus.Eager.Combine(ComparisonStatus.DontKnow));
            Assert.AreEqual(ComparisonStatus.Eager, ComparisonStatus.DontKnow.Combine(ComparisonStatus.Eager));
            Assert.AreEqual(ComparisonStatus.DontKnow, ComparisonStatus.DontKnow.Combine(ComparisonStatus.DontKnow));
        }

        [Test]
        public void StoreDontKnowsForLaterEvaluation()
        {
            ComparisonStatus status = ComparisonStatus.DontKnow;
            status.Combine(ComparisonStatus.DontKnow);
            Assert.AreEqual(true, status.Unknown);
        }

        [Test]
        public void DiscardChildrenWhenStatusIsEager()
        {
            ComparisonStatus status = ComparisonStatus.DontKnow;
            status.Combine(ComparisonStatus.DontKnow);
            Assert.AreEqual(true, status.Unknown);
            status.Combine(ComparisonStatus.Eager);
            Assert.AreEqual(true, status.Known);

            status = ComparisonStatus.DontKnow;
            status.Combine(ComparisonStatus.DontKnow);
            Assert.AreEqual(true, status.Unknown);
            status.Combine(ComparisonStatus.Lazy);
            Assert.AreEqual(false, status.Known);
        }

        [Test]
        public void StatusShouldReadStatusOfChildrenInCaseSelfStatusIsUnknown()
        {
            ComparisonStatus dontKnow = ComparisonStatus.DontKnow;

            ComparisonStatus status = ComparisonStatus.DontKnow;
            status.Combine(dontKnow);
            Assert.AreEqual(Tristate.DontKnow, status.State);

            dontKnow.Combine(ComparisonStatus.Eager);
            Assert.AreEqual(Tristate.Eager, status.State);
        }
        
        
        [Test]
        public void StackOverflow()
        {
            ComparisonStatus dontKnow = ComparisonStatus.DontKnow;
            ComparisonStatus status = ComparisonStatus.DontKnow;
            
            status.Combine(dontKnow);
            dontKnow.Combine(status);

            Assert.AreEqual(Tristate.DontKnow, status.State);

        }

        [Test]
        public void TristateAndTest()
        {
            ComparisonStatus status = ComparisonStatus.DontKnow;
            Assert.AreEqual(Tristate.DontKnow,status.And(Tristate.DontKnow,Tristate.DontKnow));
            Assert.AreEqual(Tristate.Lazy, status.And(Tristate.DontKnow,Tristate.Lazy));
            Assert.AreEqual(Tristate.Eager,status.And(Tristate.DontKnow,Tristate.Eager));
            Assert.AreEqual(Tristate.Lazy, status.And(Tristate.Lazy, Tristate.DontKnow));
            Assert.AreEqual(Tristate.Lazy, status.And(Tristate.Lazy, Tristate.Lazy));
            Assert.AreEqual(Tristate.Eager, status.And(Tristate.Lazy, Tristate.Eager));

            Assert.AreEqual(Tristate.Eager,status.And(Tristate.Eager,Tristate.DontKnow));
            Assert.AreEqual(Tristate.Eager,status.And(Tristate.Eager,Tristate.Eager));
            Assert.AreEqual(Tristate.Eager,status.And(Tristate.Eager,Tristate.Lazy));
        }
    }
}