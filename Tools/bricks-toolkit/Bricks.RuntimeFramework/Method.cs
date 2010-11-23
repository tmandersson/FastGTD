using System;
using System.Diagnostics;
using System.Reflection;

namespace Bricks.RuntimeFramework
{
    public class Method : CodeMember
    {
        public Method(MethodInfo methodInfo) : base(methodInfo) {}

        public override string ToString()
        {
            return memberInfo.Name;
        }

        public static Method CallingMethod(Predicate<MethodBase> predicate)
        {
            var stackTrace = new StackTrace();
            StackFrame[] frames = stackTrace.GetFrames();
            if (frames == null) return null;
            foreach (StackFrame stackFrame in frames)
            {
                MethodBase method = stackFrame.GetMethod();
                if (predicate.Invoke(method)) return new Method((MethodInfo) method);
            }
            return null;
        }

        public virtual MethodInfo MethodInfo
        {
            get { return (MethodInfo) memberInfo; }
        }

        public object Invoke(object parent, params object[] arguments)
        {
            return MethodInfo.Invoke(parent, arguments);
        }
    }
}