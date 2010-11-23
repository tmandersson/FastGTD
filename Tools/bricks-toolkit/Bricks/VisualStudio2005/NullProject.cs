using System;
using System.IO;
using System.Xml;
using Bricks.RuntimeFramework;

namespace Bricks.VisualStudio2005
{
    public class NullProject : VisualStudioProject
    {
        private readonly ProjectName projectName;

        public NullProject(string name)
        {
            projectName = new ProjectName(name);
        }

        public virtual ProjectName ProjectName
        {
            get { return projectName; }
        }

        public virtual int RemoveFiles(Predicate<XmlNode> predicate)
        {
            return 0;
        }

        public virtual int RemoveReference(Predicate<XmlNode> predicate)
        {
            return 0;
        }

        public virtual void RemoveTests(TestIdentifier testIdentifier) {}

        public virtual void Save() {}

        public virtual void DoXmlDocumentation(string buildConfiguration) {}

        public virtual void RemoveXmlDocumentation(string buildConfiguration) {}

        public virtual FileInfo ProjectFile
        {
            get { return null; }
        }

        public virtual string AssemblyName
        {
            get { return null; }
        }

        public virtual bool IsTestProject
        {
            get { return false; }
        }

        public virtual DirectoryInfo OutputDir(string configurationName)
        {
            return null;
        }

        public virtual void NeedsTestReference() {}

        public virtual void SetTestLessProjectReferences(BricksCollection<VisualStudioProject> projects)
        {
        }

        public virtual void SignAssembly(string value)
        {
        }
    }
}