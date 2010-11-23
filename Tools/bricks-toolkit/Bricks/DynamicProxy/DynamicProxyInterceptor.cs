using Castle.Core.Interceptor;

namespace Bricks.DynamicProxy
{
    public interface DynamicProxyInterceptor
    {
        void PreProcess(IInvocation invocation, object target);
        void PostProcess(IInvocation invocation, object target);
    }
}