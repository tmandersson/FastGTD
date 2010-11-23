using System;
using System.Reflection;
using Bricks.DynamicProxy;
using Castle.Core.Interceptor;

namespace Bricks
{
    internal class CodePathInterceptor : IInterceptor
    {
        private readonly CachedDynamicProxyGenerator generator;
        private string method = null;
        private string className = null;

        public CodePathInterceptor(CachedDynamicProxyGenerator generator)
        {
            this.generator = generator;
        }

        public virtual string Method
        {
            get { return method; }
        }

        public virtual string Class
        {
            get { return className; }
        }

        public virtual void Intercept(IInvocation invocation)
        {
            MethodInfo methodInfo = RecordMethodName(invocation);

            Type returnType = methodInfo.ReturnType;

            if (returnType.IsEnum) invocation.ReturnValue = 0;
            else if (returnType.FullName == "System.Void") invocation.ReturnValue = null;
            else if (returnType.Equals(typeof (string))) invocation.ReturnValue = null;
            else if (returnType.Equals(typeof (bool)) || returnType.Equals(typeof (bool?))) invocation.ReturnValue = true;
            else if (returnType.Equals(typeof (DateTime)) || returnType.Equals(typeof (DateTime?))) invocation.ReturnValue = DateTime.MinValue;
            else if (returnType.Equals(typeof (int)) || returnType.Equals(typeof (int?))) invocation.ReturnValue = 0;
            else if (returnType.Equals(typeof (long)) || returnType.Equals(typeof (long?))) invocation.ReturnValue = 0L;
            else if (returnType.Equals(typeof (float)) || returnType.Equals(typeof (float?))) invocation.ReturnValue = 0.0f;
            else if (returnType.Equals(typeof (double)) || returnType.Equals(typeof (double?))) invocation.ReturnValue = 0.0;
            else if (returnType.Equals(typeof (char)) || returnType.Equals(typeof (char?))) invocation.ReturnValue = 0;
            else if (returnType.Equals(typeof (decimal?)) || returnType.Equals(typeof (decimal))) invocation.ReturnValue = decimal.MinValue;
            else if (returnType.Equals(typeof (TimeSpan)) || returnType.Equals(typeof (TimeSpan?))) invocation.ReturnValue = TimeSpan.MinValue;
            else if (returnType.BaseType == null || ((returnType)).BaseType.Equals(typeof (Array))) invocation.ReturnValue = null;
            else if (returnType.IsAbstract) invocation.ReturnValue = null;
            else invocation.ReturnValue = Proxy(returnType);
        }

        private object Proxy(Type returnType)
        {
            if (returnType.IsSealed) return null;
            return generator.GetProxy(returnType, this);
        }

        private MethodInfo RecordMethodName(IInvocation invocation)
        {
            if (className == null) className = invocation.Method.DeclaringType.Name;
            MethodInfo methodInfo = invocation.Method;
            string propertyName = getProperty(methodInfo.Name);
            method += method == null ? propertyName : "." + propertyName;
            return methodInfo;
        }

        private static string getProperty(string methodOrProperty)
        {
            if (methodOrProperty.StartsWith("get_"))
                return methodOrProperty.Substring(4);
            if (methodOrProperty.StartsWith("set_"))
                return methodOrProperty.Substring(4);
            return methodOrProperty;
        }

        public virtual void Clear()
        {
            method = className = null;
        }
    }
}