using System.IO;
using System.Reflection;

namespace Bricks.VisualStudio2005
{
    public class CurrentAssembly
    {
        public static string Location
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                var info = new FileInfo(assembly.CodeBase.Substring(8));
                return info.DirectoryName;
            }
        }
    }
}