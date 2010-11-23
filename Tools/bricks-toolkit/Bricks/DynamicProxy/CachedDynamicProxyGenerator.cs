using System;
using System.Collections;
using Castle.Core.Interceptor;

namespace Bricks.DynamicProxy
{
    public class CachedDynamicProxyGenerator : DynamicProxyGenerator
    {
        private Hashtable cache = new Hashtable();

        public virtual void Clear()
        {
            cache.Clear();
        }

        public virtual object GetProxy(Type type, IInterceptor interceptor)
        {
            if (cache.ContainsKey(type))
                return cache[type];
            cache[type] = CreateProxy(interceptor, type);
            return cache[type];
        }
    }
}