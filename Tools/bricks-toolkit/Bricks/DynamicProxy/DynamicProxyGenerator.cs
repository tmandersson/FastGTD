using System;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;

namespace Bricks.DynamicProxy
{
    public class DynamicProxyGenerator
    {
        private static readonly DynamicProxyGenerator proxyGenerator = new DynamicProxyGenerator();
        private readonly ProxyGenerator generator = new ProxyGenerator();

        public static DynamicProxyGenerator Instance
        {
            get { return proxyGenerator; }
        }

        public virtual object CreateProxy(IInterceptor interceptor, Type type)
        {
            object proxy;
            if (type.IsInterface)
                proxy = generator.CreateInterfaceProxyWithoutTarget(type, interceptor);
            else
                proxy = generator.CreateClassProxy(type, interceptor);
            return proxy;
        }
    }
}