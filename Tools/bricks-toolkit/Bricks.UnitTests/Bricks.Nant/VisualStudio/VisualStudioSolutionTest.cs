using System.IO;
using NUnit.Framework;

namespace Bricks.NAnt.VisualStudio
{
    [TestFixture]
    public class VisualStudioSolutionTest
    {
        private VisualStudioSolution visualStudioSolution;

        [SetUp]
        public void SetUp()
        {
            visualStudioSolution = new VisualStudioSolution(@"Bricks.Nant\VisualStudio\SampleSolution.txt", "v3.5");
        }

        [Test]
        public void Projects()
        {
            Assert.AreEqual(2, visualStudioSolution.Count);
        }

        [Test]
        public void TestLessCopy()
        {
            VisualStudioSolution testLessSolution = visualStudioSolution.TestLessCopy();
            Assert.AreEqual(2, testLessSolution.Count);
            Assert.AreEqual(visualStudioSolution[0].ProjectName.Name + "TestLess" , testLessSolution[0].ProjectName.Name);
            string content = File.ReadAllText(testLessSolution["Bricks.NAntTestLess"].ProjectFile.FullName);
            Assert.AreEqual(true, content.Contains("BricksTestLess.csproj"), content);
            Assert.AreEqual(false, content.Contains(@"TestLessBricks\"), content);
        }
    }
}
