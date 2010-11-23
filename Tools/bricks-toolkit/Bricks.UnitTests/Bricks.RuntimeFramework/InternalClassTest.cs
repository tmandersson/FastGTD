using Bricks.RuntimeFramework;
using NUnit.Framework;

namespace Bricks.UnitTests.Bricks.RuntimeFramework
{
    [TestFixture]
    public class InternalClassTest
    {
        [Test]
        public void InternalConstructor()
        {
            var @class = new Class(typeof(InternalConstructorClass));
            object o = @class.New();
            Assert.AreNotEqual(null, o);
        }
    }
}