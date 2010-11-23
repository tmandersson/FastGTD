using System;
using System.Reflection;
using Bricks.RuntimeFramework;
using NUnit.Framework;

namespace Bricks.Testing
{
    public class AssemblyUnderTest
    {
        public static void AssertAllMethodsAreVirtual(Assembly assembly)
        {
            var classes = new Classes(assembly);
            Classes nonTestClasses = classes.WithoutAttribute(typeof(TestFixtureAttribute));
            nonTestClasses = nonTestClasses.Filter(@class => !@class.Name.EndsWith("Test"));
            MethodInfos nonVirtuals = nonTestClasses.NonVirtuals;
            BricksCollection<MethodInfo> oughtToBeVirtual = nonVirtuals.Filter(obj => !obj.Name.Contains("NonVirtual"));
            nonVirtuals.ForEach(entity => Console.WriteLine(entity.DeclaringType.FullName + "." + entity.Name));
            Assert.AreEqual(0, oughtToBeVirtual.Count);
        }
    }
}