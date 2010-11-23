using Bricks.RuntimeFramework;
using NUnit.Framework;

namespace Bricks.UnitTests.Bricks.RuntimeFramework
{
    [TestFixture]
    public class MethodTest
    {
        [Test]
        public void TestCallingMethod()
        {
            Method method = Method.CallingMethod(obj => obj.Name.Equals("TestCallingMethod"));
            Assert.AreEqual("TestCallingMethod", method.ToString());
        }

        [Test]
        public void Invoke()
        {
            var @class = new Class(typeof (ClassForMethodTest));
            var method = @class.GetMethod("GetFoo");
            var test = new ClassForMethodTest("bar");
            Assert.AreEqual("bar", method.Invoke(test));
        }

        [Test]
        public void InvokeNonPublicMethod()
        {
            var @class = new Class(typeof(ClassForMethodTest));
            var method = @class.GetMethod("_GetFoo");
            var test = new ClassForMethodTest("bar");
            Assert.AreEqual("bar", method.Invoke(test));            
        }
    }

    public class ClassForMethodTest
    {
        private readonly string foo;

        public ClassForMethodTest(string foo)
        {
            this.foo = foo;
        }

        public object GetFoo()
        {
            return foo;
        }

        private object _GetFoo()
        {
            return foo;
        }
    }
}