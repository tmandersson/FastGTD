using System.Collections.Generic;
using Bricks.Core;
using Bricks.RuntimeFramework;
using Castle.Core.Interceptor;

namespace Bricks.DynamicProxy
{
    public class DynamicProxyInterceptors : BricksCollection<DynamicProxyInterceptor>
    {
        public virtual void Process(IInvocation invocation)
        {
            ForEach(obj => obj.PreProcess(invocation, null));
            invocation.Proceed();
            ForEach(obj => obj.PostProcess(invocation, null));
        }

        public virtual void Process(IInvocation invocation, InterceptContext interceptedContext)
        {
            ForEach(obj => obj.PreProcess(invocation, interceptedContext));
            invocation.ReturnValue = new ReflectedObject(interceptedContext.Target).Invoke(invocation.Method, invocation.Arguments);
            ForEach(obj => obj.PostProcess(invocation, interceptedContext));
        }

        public static string ToString(IInvocation invocation)
        {
            string message = string.Format("Error when invoking {0}, on {1} with parameters: ", invocation.Method.Name, invocation.TargetType.Name);
            var argumentsAsString = new List<string>();
            foreach (object argument in invocation.Arguments)
            {
                argumentsAsString.Add(S.ToString(argument));
            }
            return message + string.Join(",", argumentsAsString.ToArray());
        }
    }
}