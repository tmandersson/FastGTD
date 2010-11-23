using System.IO;
using Bricks.Nant;
using Bricks.RuntimeFramework;
using Bricks.VisualStudio2005;
using NAnt.Core;
using NAnt.Core.Tasks;
using NAnt.Core.Types;

namespace Bricks.NAnt.VisualStudio
{
    public class VisualStudioSolution : BricksCollection<VisualStudioProject>
    {
        private readonly string solutionFile;
        private readonly string dotnetVersion;

        public VisualStudioSolution(string solutionFile, string dotnetVersion)
        {
            this.solutionFile = solutionFile;
            this.dotnetVersion = dotnetVersion;
            using (TextReader textReader = new StreamReader(solutionFile))
            {
                string line;
                while ((line = textReader.ReadLine()) != null)
                {
                    if (line.Contains("\"Solution Items\", \"Solution Items\"")) continue;
                    if (line.StartsWith("Project(\"{"))
                    {
                        string[] words = line.Split(' ');
                        string projectNameInDoubleQuotes = words[words.Length - 2];
                        string projectLocation = projectNameInDoubleQuotes.Substring(1, projectNameInDoubleQuotes.Length - 3);
                        string projectName = words[words.Length - 3];
                        Add(VisualStudioProjectFactory.Create(new FileInfo(solutionFile).DirectoryName + @"\" + projectLocation, projectName));
                    }
                }
            }
        }

        public virtual void DoXmlDocumentation(string configuration)
        {
            ForEach(obj => obj.DoXmlDocumentation(configuration));
        }

        public virtual void Compile(NantProject nantProject)
        {
            const string buildLog = "build.log";

            nantProject.Log(Level.Info, "Compiling solution " + solutionFile + " logging output to: " + buildLog);

            var deleteTask = nantProject.NewTask<DeleteTask>();
            deleteTask.File = new FileInfo(buildLog);
            deleteTask.Execute();

            var execTask = nantProject.NewTask<ExecTask>();
            execTask.FileName = @"C:\Program Files\Microsoft Visual Studio 8\Common7\IDE\devenv.exe";
            execTask.Arguments.Add(Argument(solutionFile));
            execTask.Arguments.Add(Argument("/rebuild"));
            execTask.Arguments.Add(Argument("Debug"));
            execTask.Arguments.Add(Argument("/out"));
            execTask.Arguments.Add(Argument(buildLog));
            execTask.Execute();
        }

        public virtual void MSBuild(NantProject nantProject, bool rebuild, string workingDirectory)
        {
            nantProject.Log(Level.Info, "Compiling solution " + solutionFile + " using MSBUILD.");

            var execTask = nantProject.NewTask<ExecTask>();
            execTask.WorkingDirectory = new DirectoryInfo(workingDirectory);
            execTask.FileName = @"c:\windows\microsoft.net\framework\" + dotnetVersion + @"\msbuild.exe";
            string buildCommand = rebuild ? "Rebuild" : "Build";
            execTask.Arguments.Add(Argument("/t:" + buildCommand));
            execTask.Arguments.Add(Argument("/verbosity:quiet"));
            execTask.Arguments.Add(Argument("/nologo"));
            execTask.Arguments.Add(Argument(solutionFile));
            execTask.Arguments.Add(Argument("/p:Configuration=Debug"));
            execTask.Execute();
        }

        private static Argument Argument(string value)
        {
            var argument = new Argument {Value = value};
            return argument;
        }

        public virtual VisualStudioProject this[string projectName]
        {
            get { return Find(project => project.ProjectName.Equals(projectName)); }
        }

        public virtual VisualStudioSolution TestLessCopy()
        {
            var solutionFileInfo = new FileInfo(solutionFile);
            string solutionFileContent = File.ReadAllText(solutionFileInfo.FullName);
            string content = solutionFileContent;
            foreach (VisualStudioProject cSharpProject in this)
            {
                if (!(cSharpProject is CSharpProject)) continue;

                content = content.Replace(@"\" + cSharpProject.ProjectName.WithExtension, @"\" + cSharpProject.ProjectName.TestLessFileWithExtension);
                string testLessProjectFile =
                    cSharpProject.ProjectFile.FullName.Replace(cSharpProject.ProjectName.WithExtension, cSharpProject.ProjectName.TestLessName.WithExtension);
                File.Copy(cSharpProject.ProjectFile.FullName, testLessProjectFile, true);
            }
            string copiedFileName = solutionFileInfo.FullName.Replace(solutionFileInfo.Extension, "TestLess" + solutionFileInfo.Extension);
            File.WriteAllText(copiedFileName, content);
            var testLessSolution = new VisualStudioSolution(copiedFileName, dotnetVersion);
            testLessSolution.ReplaceProjectReferences(this);
            return testLessSolution;
        }

        private void ReplaceProjectReferences(VisualStudioSolution originalSolution)
        {
            foreach (VisualStudioProject visualStudioProject in this)
            {
                if (visualStudioProject is CSharpProject)
                    visualStudioProject.SetTestLessProjectReferences(originalSolution);
            }
        }
    }
}