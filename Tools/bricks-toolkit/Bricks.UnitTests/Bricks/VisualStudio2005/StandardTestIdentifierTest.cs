using NUnit.Framework;

namespace Bricks.VisualStudio2005
{
    [TestFixture]
    public class StandardTestIdentifierTest
    {
        [Test]
        public void IsATest()
        {
            StandardTestIdentifier identifier = new StandardTestIdentifier();
            identifier.AddNonTestFile("IAmNotATest.cs");
            identifier.AddTestFile("Assert.cs");
            identifier.AddNonTestFile("IAmNotAMother.cs");
            Assert.AreEqual(true, identifier.IsATest(null, "FooTest.cs"));
            Assert.AreEqual(true, identifier.IsATest(null, "Assert.cs"));
            Assert.AreEqual(true, identifier.IsATest(null, @"Foo\Assert.cs"));
            Assert.AreEqual(true, identifier.IsATest(null, @"Foo\FooTester.cs"));
            Assert.AreEqual(false, identifier.IsATest(null, "IAmNotATest.cs"));
            Assert.AreEqual(false, identifier.IsATest(null, @"Foo\IAmNotATest.cs"));
            Assert.AreEqual(true, identifier.IsATest(null, @"Foo\IAmAMother.cs"));
            Assert.AreEqual(false, identifier.IsATest(null, @"Foo\IAmNotAMother.cs"));
        }
    }
}