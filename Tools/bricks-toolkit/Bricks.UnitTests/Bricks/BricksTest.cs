using System.Reflection;
using Bricks.Core;
using Bricks.DynamicProxy;
using Bricks.RuntimeFramework;
using Castle.Core.Interceptor;
using NUnit.Framework;

namespace Bricks
{
    //TODO: Verify that NullObjects override all methods
    [TestFixture]
    public class BricksTest
    {
        [Test]
        public void CheckAllMethodsAreVirtual()
        {
            AssemblyTest.AllMethodsVirtual(typeof(Clock).Assembly);
        }

        [Test]
        public void CallingActualInterceptedMethod()
        {
            var proxy = (TestInterceptedObject) DynamicProxyGenerator.Instance.CreateProxy(new TestInterceptor(), typeof(TestInterceptedObject));
            Assert.AreEqual(5, proxy.Return());
        }
    }

    public class TestInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }

    public class TestInterceptedObject
    {
        public virtual int Return()
        {
            return 5;
        }
    }
}