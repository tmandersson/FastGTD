using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Bricks.NAnt.Build
{
    //TODO: Add feature so that the only public methods can be executed. So that the rest can be treated as methods only.
    [TestFixture]
    public class BuildCommandTest
    {
        [Test, ExpectedException(typeof(BricksBuildException))]
        public void NoCommand()
        {
            TestBuildCommand();
        }

        [Test]
        public void SingleCommand()
        {
            TestBuildCommand("Compile");
        }

        [Test]
        public void MultipleCommands()
        {
            TestBuildCommand("Compile", ",", "Test");
        }

        [Test, Ignore]
        public void When_the_command_takes_variable_number_of_arguments()
        {
            TestBuildCommand("TakesVariableArgument", "arg1", "arg2");
        }

        [Test]
        public void When_BuildProgram_has_two_methods_with_same_name()
        {
            TestBuildCommand("TwoMethodsWithSameName");
        }

        [Test]
        public void ShowProjectHelp()
        {
            TestBuildCommand("-projecthelp");
        }

        private static void TestBuildCommand(params string[] commands)
        {
            List<string> completeCommand = new List<string>();
            completeCommand.AddRange(commands);
            new BuildCommand(typeof(TestBuildProgram), completeCommand.ToArray());
        }
    }

    internal class TestBuildProgram
    {
        public static void Compile(){}
        public static void Test(){}
        public static void ThrowException()
        {
            throw new NotImplementedException();
        }

        public static void TakesVariableArgument(params string[] arguments)
        {
        }

        public static void TwoMethodsWithSameName(){}

        private static void TwoMethodsWithSameName(string s) {}
    }
}