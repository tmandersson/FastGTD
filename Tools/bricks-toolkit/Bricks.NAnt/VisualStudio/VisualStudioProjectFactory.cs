using Bricks.RuntimeFramework;
using Bricks.VisualStudio2005;

namespace Bricks.NAnt.VisualStudio
{
    public class VisualStudioProjectFactory
    {
        private static readonly BricksCollection<string> ignoredExtensions = new BricksCollection<string>();
        static VisualStudioProjectFactory()
        {
            ignoredExtensions.Add("vdproj");
            ignoredExtensions.Add("vbproj");
            ignoredExtensions.Add("rptproj");
        }

        public static VisualStudioProject Create(string location, string projectName)
        {
            if (IsWebServiceProject(projectName, location) || IsNonCodeProject(location))
            {
                return new NullProject(location);
            }
            return new CSharpProject(location);
        }

        private static bool IsNonCodeProject(string location)
        {
            return ignoredExtensions.Find(location.Contains) != null;
        }

        private static bool IsWebServiceProject(string projectName, string location)
        {
            return projectName.Contains("...") || location.StartsWith("http");
        }
    }
}