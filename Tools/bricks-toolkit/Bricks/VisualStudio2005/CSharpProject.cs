using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Bricks.Core;
using Bricks.RuntimeFramework;

namespace Bricks.VisualStudio2005
{
    public class CSharpProject : VisualStudioProject
    {
        private readonly string location;
        private readonly XmlDocument xmlDocument;
        private bool needsTestReference;
        private readonly ProjectName projectName;
        
        public CSharpProject(string location)
        {
            this.location = location;
            xmlDocument = new XmlDocument();
            xmlDocument.Load(location);
            projectName = new ProjectName(new FileInfo(location).Name.Replace(".csproj", string.Empty));
        }

        public virtual ProjectName ProjectName
        {
            get { return projectName; }
        }

        public virtual int RemoveFiles(Predicate<XmlNode> predicate)
        {
            return RemoveNodes("Compile", predicate);
        }

        private int RemoveNodes(string nodeTag, Predicate<XmlNode> predicate)
        {
            List<XmlNode> toBeRemovedItems = XML.MatchingNodes(xmlDocument, nodeTag, predicate);
            if (toBeRemovedItems.Count == 0) return 0;
            XmlNode parentNode = toBeRemovedItems[0].ParentNode;
            foreach (XmlNode node in toBeRemovedItems)
                parentNode.RemoveChild(node);
            return toBeRemovedItems.Count;
        }

        public virtual int RemoveReference(Predicate<XmlNode> predicate)
        {
            return RemoveNodes("Reference", predicate);
        }

        public virtual void RemoveTests(TestIdentifier testIdentifier)
        {
            RemoveFiles(delegate(XmlNode xmlNode)
                            {
                                string fileName = xmlNode.Attributes["Include"].Value;
                                return testIdentifier.IsATest(this, fileName);
                            });
            RemoveReference(delegate(XmlNode xmlNode)
                                {
                                    string include = xmlNode.Attributes["Include"].Value;
                                    return (S.ContainsIgnoreCase(include, "nunit") && !needsTestReference);
                                });
            Save();
        }

        public virtual void Save()
        {
            xmlDocument.Save(location);
        }

        public virtual void DoXmlDocumentation(string buildConfiguration)
        {
            XmlNode propertyGroup = PropertyGroupFor(buildConfiguration);
            XmlNode documentationNode = PropertyGroupChildNode(propertyGroup, "DocumentationFile");
            documentationNode.InnerText = XML.ChildNode(propertyGroup, "OutputPath").InnerText + GetAssemblyName() +
                                          ".xml";
        }

        private XmlNode PropertyGroupChildNode(XmlNode propertyGroup, string nodeName)
        {
            XmlNode documentationNode = XML.ChildNode(propertyGroup, nodeName);
            if (documentationNode == null)
            {
                documentationNode =
                    xmlDocument.CreateNode(XmlNodeType.Element, nodeName, propertyGroup.NamespaceURI);
                propertyGroup.AppendChild(documentationNode);
            }
            return documentationNode;
        }

        private XmlNode PropertyGroupFor(string configurationName)
        {
            List<XmlNode> list = XML.MatchingNodes(xmlDocument, "PropertyGroup", delegate(XmlNode obj)
                                                                                     {
                                                                                         XmlAttribute attribute =
                                                                                             obj.Attributes["Condition"];
                                                                                         return
                                                                                             attribute != null &&
                                                                                             S.ContainsIgnoreCase(
                                                                                                 attribute.Value,
                                                                                                 configurationName);
                                                                                     });
            if (list.Count == 0)
                throw new VisualStudioException("Configuration " + configurationName + " doesn't exist in the project");
            return list[0];
        }

        private XmlNode CommonPropertyGroup()
        {
            List<XmlNode> list = XML.MatchingNodes(xmlDocument, "PropertyGroup", obj => obj.Attributes.Count == 0);
            if (list.Count == 0) throw new VisualStudioException("Common property group doesn't exist in the project");
            return list[0];
        }

        private string GetAssemblyName()
        {
            List<XmlNode> list =
                XML.MatchingNodes(xmlDocument, "PropertyGroup", obj => obj.Attributes.Count == 0);
            return XML.ChildNode(list[0], "AssemblyName").InnerText;
        }

        public virtual void RemoveXmlDocumentation(string buildConfiguration)
        {
            XmlNode propertyGroup = PropertyGroupFor(buildConfiguration);
            XmlNode documentationNode = PropertyGroupChildNode(propertyGroup, "DocumentationFile");
            documentationNode.InnerText = string.Empty;
        }

        public virtual FileInfo ProjectFile
        {
            get { return new FileInfo(location); }
        }

        public virtual string AssemblyName
        {
            get
            {
                XmlNode commonPropertyGroup = CommonPropertyGroup();
                string outputType = XML.ChildNode(commonPropertyGroup, "OutputType").InnerText;
                string extension = "Library".Equals(outputType) ? "dll" : "exe";
                return string.Format("{0}.{1}", XML.ChildNode(commonPropertyGroup, "AssemblyName").InnerText, extension);
            }
        }

        public virtual bool IsTestProject
        {
            get { return projectName.IsTestProject; }
        }

        public virtual DirectoryInfo OutputDir(string configurationName)
        {
            XmlNode propertyGroup = PropertyGroupFor(configurationName);
            string projectOutput = XML.ChildNode(propertyGroup, "OutputPath").InnerText;
            return new DirectoryInfo(string.Format(@"{0}\{1}", new FileInfo(location).Directory.FullName, projectOutput));
        }

        public virtual void NeedsTestReference()
        {
            needsTestReference = true;
        }

        public override string ToString()
        {
            return location;
        }

        public virtual void SetTestLessProjectReferences(BricksCollection<VisualStudioProject> projects)
        {
            List<XmlNode> itemGroups = XML.MatchingNodes(xmlDocument, "ItemGroup",
                                                         obj => obj.ChildNodes.Count != 0 && obj.ChildNodes[0].Name.Equals("ProjectReference"));
            if (itemGroups.Count == 0) return;
            foreach (XmlNode node in itemGroups[0].ChildNodes)
            {
                foreach (VisualStudioProject project in projects)
                {
                    XmlAttribute includeAttribute = node.Attributes["Include"];
                    if (includeAttribute.Value.Contains(project.ProjectName.Name))
                    {
                        includeAttribute.Value = includeAttribute.Value.Replace(project.ProjectName.WithExtension, project.ProjectName.TestLessName.WithExtension);
                    }
                }
            }
            Save();
        }

        public virtual void SignAssembly(string buildConfiguration)
        {
            XmlNode propertyGroup = PropertyGroupFor(buildConfiguration);
            XmlNode signAssemblyNode = PropertyGroupChildNode(propertyGroup, "SignAssembly");
            signAssemblyNode.InnerText = "true";
        }
    }
}