using System.Collections.Generic;
using System.Reflection;
using Bricks;
using Bricks.DynamicProxy;
using Bricks.Objects;
using Castle.Core.Interceptor;

namespace Lunar.Client.Common
{
    public class NullInterceptor : IInterceptor
    {
        private CachedDynamicProxyGenerator generator;
        private readonly IDictionary<string, object> returnValues;

        public NullInterceptor(CachedDynamicProxyGenerator generator, IDictionary<string, object> returnValues)
        {
            this.generator = generator;
            this.returnValues = returnValues;
        }

        public virtual void Intercept(IInvocation invocation)
        {
            MethodInfo method = invocation.Method;
            if (method.IsPublic && method.Name == "get_" + CodePath.Get(CodePath.New<NullableObject>().IsNull))
            {
                invocation.ReturnValue = true;
                return;
            }
            if (method.IsPublic && method.Name == "get_" + CodePath.Get(CodePath.New<NullableObject>().IsNotNull))
            {
                invocation.ReturnValue = false;
                return;
            }
            if (method.IsPublic && returnValues.ContainsKey(method.Name))
            {
                invocation.ReturnValue = returnValues[method.Name];
                return;
            }

            invocation.Proceed();
            if (invocation.ReturnValue != null)
            {
                invocation.ReturnValue = generator.GetProxy(method.ReturnType, this);
            }
        }
    }
}