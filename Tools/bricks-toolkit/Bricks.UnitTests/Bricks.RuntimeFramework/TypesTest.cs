using NUnit.Framework;

namespace Bricks.RuntimeFramework
{
    [TestFixture]
    public class TypesTest
    {
        [Test]
        public void IsNotAssignableFrom()
        {
            Types types = new Types(typeof (string), typeof (int));
            Assert.AreEqual(false, types.IsAssignableFrom(typeof (object)));
        }

        [Test]
        public void IsAssignableFrom()
        {
            Types types = new Types(typeof(object));
            Assert.AreEqual(true, types.IsAssignableFrom(typeof(string)));
            Assert.AreEqual(true, types.IsAssignableFrom(typeof(object)));
        }
    }
}