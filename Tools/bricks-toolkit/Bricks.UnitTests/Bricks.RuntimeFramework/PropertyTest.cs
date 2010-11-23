using Bricks.RuntimeFramework;
using NUnit.Framework;

namespace Bricks.UnitTests.Bricks.RuntimeFramework
{
    [TestFixture]
    public class PropertyTest
    {
        private ForPropertyTest forPropertyTest;
        private Class @class;

        [SetUp]
        public void SetUp()
        {
            forPropertyTest = new ForPropertyTest("bar");
            @class = new Class(typeof(ForPropertyTest));
        }

        [Test]
        public void Get()
        {
            var property = @class.GetProperty("Foo");
            Assert.AreEqual("bar", property.Get(forPropertyTest));
        }

        [Test]
        public void Set()
        {
            var property = @class.GetProperty("Foo");
            property.Set(forPropertyTest, "baz");
            Assert.AreEqual("baz", forPropertyTest.Foo);            
        }
    }

    public class ForPropertyTest
    {
        private string foo;

        public ForPropertyTest(string foo)
        {
            this.foo = foo;
        }

        public string Foo
        {
            get { return foo; }
            set { foo = value; }
        }
    }
}