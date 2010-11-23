using Bricks.RuntimeFramework;

namespace Bricks.VisualStudio2005
{
    public class StandardTestIdentifier : TestIdentifier
    {
        private readonly BricksCollection<string> nonTestFiles = new BricksCollection<string>();
        private readonly BricksCollection<string> testFiles = new BricksCollection<string>();

        public virtual void AddNonTestFile(string fileName)
        {
            nonTestFiles.Add(fileName);
        }

        public virtual void AddTestFile(string fileName)
        {
            testFiles.Add(fileName);
        }

        public virtual bool IsATest(CSharpProject project, string fileName)
        {
            bool isASpecifiedTestFile = testFiles.Contains(delegate(string obj)
                                                             {
                                                                 return fileName.Contains(obj);
                                                             });
            bool isASpecifiedNonTestFile = nonTestFiles.Contains(delegate(string obj)
                                                                 {
                                                                     return fileName.Contains(obj);                                                                    
                                                                 });
            return (HasAStandardTestName(fileName) || isASpecifiedTestFile) && !isASpecifiedNonTestFile;
        }

        public virtual bool IsTestProject(VisualStudioProject project)
        {
            return project.IsTestProject;
        }

        private static bool HasAStandardTestName(string fileName)
        {
            return fileName.Contains("Test") || fileName.Contains("Mother")|| fileName.Contains("Tester");
        }
    }
}