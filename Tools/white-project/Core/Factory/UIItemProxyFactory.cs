using Bricks.DynamicProxy;
using White.Core.Interceptors;
using White.Core.UIItems;
using White.Core.UIItems.Actions;

namespace White.Core.Factory
{
    public static class UIItemProxyFactory
    {
        public static IUIItem Create(IUIItem item, ActionListener actionListener)
        {
            return (IUIItem) DynamicProxyGenerator.Instance.CreateProxy(new CoreInterceptor(item, actionListener), item.GetType());
        }
    }
}