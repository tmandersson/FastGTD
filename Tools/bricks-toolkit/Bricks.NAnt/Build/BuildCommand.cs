using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bricks.NAnt.Build
{
    public class BuildCommand
    {
        private readonly List<BuildStep> buildSteps = new List<BuildStep>();

        public BuildCommand(Type programType, string[] arguments)
        {
            if (arguments.Length == 0) throw new BricksBuildException("Default command is not supported yet");
            if (arguments.Length == 1 && "-projecthelp".Equals(arguments[0]))
            {
                Console.WriteLine("Available commands");
                List<MethodInfo> methods = new List<MethodInfo>(programType.GetMethods(BindingFlags.Public | BindingFlags.Static));
                foreach (MethodInfo methodInfo in methods)
                {
                    Console.WriteLine(methodInfo.Name);
                }
                return;
            }
            StringBuilder builder = new StringBuilder();
            foreach (string argument in arguments)
                builder.Append(argument).Append(" ");
            string[] commands = builder.ToString().Split(',');
            foreach (string command in commands)
            {
                string[] parts = command.Trim().Split(' ');
                buildSteps.Add(new BuildStep(programType, parts));
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>number of steps executed</returns>
        public virtual void Execute()
        {
            foreach (BuildStep buildStep in buildSteps)
            {
                buildStep.Execute();
            }
        }
    }
}