using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace Bricks.RuntimeFramework
{
    public class AssemblyTest
    {
        public static void AllMethodsVirtual(Assembly assembly)
        {
            AllMethodsVirtual(assembly, delegate { return true; });
        }

        public static void AllMethodsVirtual(Assembly assembly, Predicate<Class> predicate)
        {
            var classes = new Classes(assembly);
            List<Class> nonTestClasses = classes.WithoutAttribute(typeof(TestFixtureAttribute)).FindAll(obj => !obj.Name.Contains("Test"));
            var filteredClasses = new Classes(nonTestClasses.FindAll(predicate));
            MethodInfos nonVirtuals = filteredClasses.NonVirtuals;
            BricksCollection<MethodInfo> oughtToBeVirtual = nonVirtuals.Filter(delegate(MethodInfo obj) { return !obj.Name.Contains("NonVirtual"); });
            Assert.AreEqual(0, oughtToBeVirtual.Count, oughtToBeVirtual.ToString());
        }
    }
}