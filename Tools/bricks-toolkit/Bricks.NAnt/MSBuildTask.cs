using System.IO;
using Bricks.NAnt.VisualStudio;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace Bricks.Nant
{
    [TaskName("msbuild")]
    public class MSBuildTask : Task
    {
        private string projectName;
        private bool rebuild;
        private string solution;
        private string workingDirectory;
        private string dotNetVersion;

        [TaskAttribute("solution",Required = true)]
        public string Solution
        {
            get { return solution; }
            set { solution = value; }
        }

        [TaskAttribute("projectName",Required = true)]
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        [TaskAttribute("rebuild", Required = true)]
        public bool Rebuild
        {
            get { return rebuild; }
            set { rebuild = value; }
        }

        [TaskAttribute("workingDirectory", Required = true)]
        public string WorkingDirectory
        {
            get { return workingDirectory; }
            set { workingDirectory = value; }
        }

        protected override void ExecuteTask()
        {
            SolutionBuilder solutionBuilder = new SolutionBuilder(new VisualStudioSolution(Solution, dotNetVersion ?? "v2.0.50727"));
            NantProject project = new NantProject(projectName);
            Project.Log(Level.Info, "Doing MSBuild for project " + project);
            solutionBuilder.MSBuild(project, Rebuild, WorkingDirectory);
            Project.Log(Level.Info, "Finished MSBuild for project " + project);
        }

        [TaskAttribute("dotNetVersion")]
        public string DotNetVersion
        {
            get { return dotNetVersion; }
            set { dotNetVersion = value; }
        }
    }
}
