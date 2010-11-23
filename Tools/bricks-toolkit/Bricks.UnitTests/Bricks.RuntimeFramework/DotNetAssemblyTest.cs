using System.Reflection;
using NUnit.Framework;

namespace Bricks.RuntimeFramework
{
    [TestFixture]
    public class DotNetAssemblyTest
    {
        [Test]
        public void Location()
        {
            DotNetAssembly dotNetAssembly = new DotNetAssembly(Assembly.GetExecutingAssembly());
            Assert.AreNotEqual(null, dotNetAssembly.File);
        }
    }
}