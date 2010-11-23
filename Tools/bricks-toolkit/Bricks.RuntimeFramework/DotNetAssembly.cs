using System.IO;
using System.Reflection;

namespace Bricks.RuntimeFramework
{
    public class DotNetAssembly
    {
        private readonly Assembly assembly;

        public DotNetAssembly(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public virtual FileInfo File
        {
            get {return new FileInfo(assembly.CodeBase.Replace("file:///", string.Empty));}
        }
    }
}