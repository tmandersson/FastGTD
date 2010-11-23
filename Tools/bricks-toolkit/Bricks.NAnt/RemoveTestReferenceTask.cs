using Bricks.NAnt.VisualStudio;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace Bricks.Nant
{
    [TaskName("removetest")]
    public class RemoveTestReferenceTask : Task
    {
        private string solution;
        private string projectName;
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

        protected override void ExecuteTask()
        {
            SolutionBuilder solutionBuilder = new SolutionBuilder(new VisualStudioSolution(Solution, dotNetVersion ?? "v2.0.50727"));
            NantProject project = new NantProject(projectName);
            Project.Log(Level.Info, "Removing Test classes from project " + project + " and nunit dll reference");
            solutionBuilder.BuildWithoutTests(project);
            Project.Log(Level.Info, "Done Test classes from project " + project + " and nunit dll reference");
        }

        [TaskAttribute("dotNetVersion")]
        public string DotNetVersion
        {
            get { return dotNetVersion; }
            set {dotNetVersion =value; }
        }
    }
}
