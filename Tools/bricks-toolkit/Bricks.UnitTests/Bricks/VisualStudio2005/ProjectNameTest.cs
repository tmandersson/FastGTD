using NUnit.Framework;

namespace Bricks.VisualStudio2005
{
    [TestFixture]
    public class ProjectNameTest
    {
        [Test]
        public void IsTestProject()
        {
            Assert.AreEqual(false, new ProjectName("Foo").IsTestProject);
            Assert.AreEqual(true, new ProjectName("FooTest").IsTestProject);
            Assert.AreEqual(true, new ProjectName("TestLessFooTest").IsTestProject);
            Assert.AreEqual(false, new ProjectName("TestLessFoo").IsTestProject);
        }
    }
}