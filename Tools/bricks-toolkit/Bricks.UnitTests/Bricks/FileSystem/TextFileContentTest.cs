using NUnit.Framework;

namespace Bricks.FileSystem
{
    [TestFixture]
    public class TextFileContentTest
    {
        [Test]
        public void Lines()
        {
            TextFileContent fileContent = new TextFileContent(@"
a
b
c");
            Assert.AreEqual(3, fileContent.Lines.Count);
        }
    }
}