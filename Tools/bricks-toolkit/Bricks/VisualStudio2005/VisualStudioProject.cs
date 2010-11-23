using System;
using System.IO;
using System.Xml;
using Bricks.RuntimeFramework;

namespace Bricks.VisualStudio2005
{
    public interface VisualStudioProject
    {
        ProjectName ProjectName { get; }
        int RemoveFiles(Predicate<XmlNode> predicate);
        int RemoveReference(Predicate<XmlNode> predicate);
        void RemoveTests(TestIdentifier @delegate);
        void Save();
        void DoXmlDocumentation(string buildConfiguration);
        void RemoveXmlDocumentation(string buildConfiguration);
        FileInfo ProjectFile { get; }
        string AssemblyName { get; }
        bool IsTestProject { get; }
        DirectoryInfo OutputDir(string configurationName);
        void NeedsTestReference();
        void SetTestLessProjectReferences(BricksCollection<VisualStudioProject> projects);
        void SignAssembly(string value);
    }
}