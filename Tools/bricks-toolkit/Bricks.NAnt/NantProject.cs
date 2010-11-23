using System;
using System.Xml;
using Bricks.RuntimeFramework;
using NAnt.Core;

namespace Bricks.Nant
{
    //TODO: Remove all tests
    public class NantProject
    {
        private readonly Project project;

        public NantProject(string projectName)
        {
            var document = new XmlDocument();
            XmlElement projectNode = document.CreateElement("project");
            document.AppendChild(projectNode);
            XmlAttribute attribute = document.CreateAttribute("name");
            attribute.Value = projectName;
            projectNode.Attributes.Append(attribute);
            document.AppendChild(projectNode);
            project = new Project(document, Level.Info, 0);
        }

        public virtual void Log(Level messageLevel, string message)
        {
            project.Log(messageLevel, message);
        }

        public virtual void Log(Exception exception)
        {
            project.Log(Level.Error, exception.ToString());
        }

        public virtual T NewTask<T>() where T : Task
        {
            var @class = new Class(typeof(T));
            var t = (T) @class.New();
            t.Project = project;
            return t;
        }

        public virtual T NewCommand<T>()
        {
            var @class = new Class(typeof(T));
            return (T)@class.New(this);
        }
    }
}