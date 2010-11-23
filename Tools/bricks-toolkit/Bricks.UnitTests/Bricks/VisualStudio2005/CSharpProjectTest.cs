using System.IO;
using Bricks.Core;
using NUnit.Framework;

namespace Bricks.VisualStudio2005
{
    [TestFixture]
    public class CSharpProjectTest
    {
        private VisualStudioProject cSharpProject;
        private readonly string projectLocation = CurrentAssembly.Location + @"\VisualStudio2005\Cement.csproj";
        private const string tempProjectLocation = "CementTemp.csproj";

        [SetUp]
        public void SetUp()
        {
            File.Copy(projectLocation, tempProjectLocation, true);
            cSharpProject = new CSharpProject(tempProjectLocation);
        }

        [Test]
        public void DoXmlDocumentation()
        {
            string content = File.ReadAllText(projectLocation).ToLower();
            Assert.AreEqual(false, content.Contains(@"<documentationfile>bin\debug\cement.xml</documentationfile>"), content);
            cSharpProject.DoXmlDocumentation("Debug");
            cSharpProject.Save();
            content = File.ReadAllText(tempProjectLocation).ToLower();
            Assert.AreEqual(true, content.Contains(@"<documentationfile>bin\debug\cement.xml</documentationfile>"), content);
            cSharpProject.RemoveXmlDocumentation("Debug");
            cSharpProject.Save();
            content = File.ReadAllText(tempProjectLocation).ToLower();
            Assert.AreEqual(false, content.Contains(@"<documentationfile>bin\debug\cement.xml</documentationfile>"), content);
        }

        [Test]
        public void Name()
        {
            Assert.AreEqual("CementTemp", cSharpProject.ProjectName.Name);
        }

        [Test]
        public void RemoveClasses()
        {
            int numberOfFilesRemoved =
                cSharpProject.RemoveFiles(xmlNode => xmlNode.Attributes["Include"].Value.EndsWith("Test.cs"));
            Assert.AreEqual(4, numberOfFilesRemoved);
        }

        [Test]
        public void RemoveReference()
        {
            int referencesRemoved =
                cSharpProject.RemoveReference(xmlNode => xmlNode.Attributes["Include"].Value.StartsWith("nunit.framework"));
            Assert.AreEqual(1, referencesRemoved);
        }

        [Test]
        public void RemoveTests()
        {
            const string excludedFile = "ProductionCodeWithTestAppearingInIt.cs";
            var standardTestIdentifier = new StandardTestIdentifier();
            standardTestIdentifier.AddNonTestFile("ProductionCodeWithTestAppearingInIt.cs");
            cSharpProject.RemoveTests(standardTestIdentifier);
            string content = File.ReadAllText(tempProjectLocation);
            Assert.AreEqual(true, content.Contains(excludedFile));
        }

        [Test]
        public void OutputPath()
        {
            DirectoryInfo dir = cSharpProject.OutputDir("Debug");
            Assert.AreEqual(CurrentAssembly.Location + @"\bin\Debug\", dir.FullName);
        }

        [Test]
        public void AssemblyName()
        {
            Assert.AreEqual("Cement.dll", cSharpProject.AssemblyName);
        }

        [Test]
        public void SignAssembly()
        {
            string content = File.ReadAllText(projectLocation).ToLower();
            Assert.AreEqual(false, S.ContainsIgnoreCase(content, @"<SignAssembly>true</SignAssembly>"));
            cSharpProject.SignAssembly("Debug");
            cSharpProject.Save();
            content = File.ReadAllText(tempProjectLocation).ToLower();
            Assert.AreEqual(true, S.ContainsIgnoreCase(content, @"<SignAssembly>true</SignAssembly>"));
        }
    }
}