using Bricks.VisualStudio2005;
using NUnit.Framework;

namespace Bricks.NAnt.VisualStudio
{
    [TestFixture]
    public class VisualStudioProjectFactoryTest
    {
        [Test]
        public void Create()
        {
            Assert.AreEqual(typeof(NullProject), VisualStudioProjectFactory.Create(@"foo\bar.vdproj", "bar").GetType());
            Assert.AreEqual(typeof(NullProject), VisualStudioProjectFactory.Create(@"foo\bar.vbproj", "bar").GetType());
            Assert.AreEqual(typeof(NullProject), VisualStudioProjectFactory.Create(@"foo\bar", "foo...bar").GetType());
            Assert.AreEqual(typeof(NullProject), VisualStudioProjectFactory.Create(@"http://localhost/Foo.Bar", @"http://localhost/Foo.Bar").GetType());
            Assert.AreEqual(typeof(NullProject), VisualStudioProjectFactory.Create(@"http://localhost/Foo.Bar", @"/http://localhost/Foo.Bar/").GetType());
        }
    }
}