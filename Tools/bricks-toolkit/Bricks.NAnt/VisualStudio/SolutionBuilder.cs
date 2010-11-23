using Bricks.Nant;
using Bricks.VisualStudio2005;

namespace Bricks.NAnt.VisualStudio
{
    public class SolutionBuilder
    {
        private readonly VisualStudioSolution solution;
        public delegate void PreCompile(VisualStudioProject visualStudioProject);

        public SolutionBuilder(VisualStudioSolution solution)
        {
            this.solution = solution;
        }

        public virtual void BuildWithoutTests(NantProject nantProject, TestIdentifier testIdentifier, string workingDirectory, bool useMSBuild)
        {
            BuildWithoutTests(nantProject, testIdentifier, workingDirectory, useMSBuild, delegate {});
        }

        public virtual void BuildWithoutTests(NantProject nantProject, TestIdentifier testIdentifier, string workingDirectory, bool useMSBuild, PreCompile preCompile)
        {
            VisualStudioSolution testLessSolution = solution.TestLessCopy();
            testLessSolution.ForEach(delegate(VisualStudioProject project)
                              {
                                  if (testIdentifier.IsTestProject(project))
                                  {
                                      return;
                                  }
                                  project.RemoveTests(testIdentifier);
                              });
            testLessSolution.ForEach(obj => preCompile(obj));
            if (useMSBuild)
                testLessSolution.MSBuild(nantProject, true, workingDirectory);
            else
                testLessSolution.Compile(nantProject);
        }

        /// <summary>
        /// Would use StandardTestIdentifier
        /// </summary>
        public virtual void BuildWithoutTests(NantProject nantProject)
        {
            BuildWithoutTests(nantProject, delegate {});
        }

        /// <summary>
        /// Would use StandardTestIdentifier
        /// </summary>
        public virtual void BuildWithoutTests(NantProject nantProject, PreCompile preCompile)
        {
            BuildWithoutTests(nantProject, new StandardTestIdentifier(), ".", true, preCompile);
        }

        public virtual void MSBuild(NantProject nantProject, bool rebuild, string workingDirectory)
        {
            solution.MSBuild(nantProject, rebuild, workingDirectory);
        }

        public virtual void BuildWithoutTests(NantProject nantProject, TestIdentifier testIdentifier, PreCompile preCompile)
        {
            BuildWithoutTests(nantProject, testIdentifier, ".", true, preCompile);
        }
    }
}