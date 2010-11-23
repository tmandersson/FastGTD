using System.Reflection;
using System.Text;

namespace Bricks.RuntimeFramework
{
    public class MethodInvocation
    {
        private readonly Method method;
        private readonly object[] args;

        public MethodInvocation(MethodInfo methodInfo, object[] args)
        {
            method = new Method(methodInfo);
            this.args = args;
        }

        public virtual Method Method
        {
            get { return method; }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(method.ToString());
            if (args.Length == 0)
            {
                builder.Append(" with no arguments");
            }
            foreach (object arg in args)
            {
                builder.Append(arg ?? "null").Append(",");
            }
            return builder.ToString();
        }
    }
}