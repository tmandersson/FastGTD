using Bricks.VisualStudio2005;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;

namespace Bricks.Nant
{
    public abstract class CodeDocumentationTask : Task
    {
        protected string projectLocation;
        protected string configuration;

        [TaskAttribute("project")]
        public virtual string ProjectLocation
        {
            get { return projectLocation; }
            set { projectLocation = value; }
        }

        [TaskAttribute("conf")]
        public virtual string Configuration
        {
            get { return configuration; }
            set { configuration = value; }
        }
    }

    [TaskName("addcodedoc")]
    public class AddCodeDocumentationTask : CodeDocumentationTask
    {
        protected override void ExecuteTask()
        {
            Project.Log(Level.Info, "Adding code documentation to " + projectLocation + " in configuration " + configuration);
            VisualStudioProject project = new CSharpProject(projectLocation);
            project.DoXmlDocumentation(configuration);
            project.Save();
        }
    }

    [TaskName("removecodedoc")]
    public class RemoveCodeDocumentationTask : CodeDocumentationTask
    {
        protected override void ExecuteTask()
        {
            Project.Log(Level.Info, "Removing code documentation from " + projectLocation + " in configuration " + configuration);
            VisualStudioProject project = new CSharpProject(projectLocation);
            project.RemoveXmlDocumentation(configuration);
            project.Save();
        }
    }
}