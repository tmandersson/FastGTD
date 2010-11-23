using System;
using System.Collections.Generic;
using System.Xml;

namespace Bricks
{
    public class XML
    {
        public static XmlNode ChildNode(XmlNode parentNode, string childNodeName)
        {
            foreach (XmlNode childNode in parentNode.ChildNodes)
            {
                if (childNode.Name.Equals(childNodeName)) return childNode;
            }
            return null;
        }

        public static List<XmlNode> MatchingNodes(XmlDocument xmlDocument, string nodeTag, Predicate<XmlNode> predicate)
        {
            XmlNodeList nodes = xmlDocument.GetElementsByTagName(nodeTag);
            List<XmlNode> matchingNodes = new List<XmlNode>();
            foreach (XmlNode xmlNode in nodes)
            {
                if (predicate.Invoke(xmlNode))
                    matchingNodes.Add(xmlNode);
            }
            return matchingNodes;
        }
    }
}