using System.Collections.Generic;
using System.Reflection;

namespace Bricks.NAnt.Build
{
    public class BuildStepArguments
    {
        private readonly List<string> collection = new List<string>();
        public BuildStepArguments(string[] parts, MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length != parts.Length - 1 && !LastParameterIsVarArgs(parameters[parameters.Length - 1])) throw new BricksBuildException("The build steps takes " + parameters.Length + " arguments");
            collection.AddRange(parts);
            collection.RemoveAt(0);
        }

        public virtual object[] MethodParameters
        {
            get { return collection.ToArray(); }
        }

        private static bool LastParameterIsVarArgs(ParameterInfo info)
        {
            return info.ParameterType.Equals(typeof (string[]));
        }
    }
}