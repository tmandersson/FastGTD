using System;
using System.Reflection;

namespace Bricks.NAnt.Build
{
    /// <summary>
    /// Build step understands a part of the command specified from command line
    /// </summary>
    public class BuildStep
    {
        private readonly MethodInfo method;
        private readonly BuildStepArguments arguments;

        public BuildStep(Type programType, params string[] parts)
        {
            if (parts.Length == 0) throw new BricksBuildException("Build step cannot be empty.");

            method = programType.GetMethod(MethodName(parts), BindingFlags.Static | BindingFlags.Public);
            if (method == null) throw new BricksBuildException("Target action " + parts[0] + " doesn't exist");

            arguments = new BuildStepArguments(parts, method);
        }

        private static string MethodName(string[] parts)
        {
            string name = parts[0];
            return name.Substring(0, 1).ToUpper() + name.Substring(1, name.Length - 1);
        }

        public virtual void Execute()
        {
            method.Invoke(null, arguments.MethodParameters);
        }
    }
}