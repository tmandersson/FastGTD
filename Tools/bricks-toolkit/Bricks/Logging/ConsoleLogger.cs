using System;
using log4net;
using log4net.Core;

namespace Bricks.Logging
{
    public class ConsoleLogger : ILog {
        public virtual void Debug(object message)
        {
            Info(message);
        }

        public virtual void Debug(object message, Exception exception)
        {
            Info(message, exception);
        }

        public virtual void DebugFormat(string format, params object[] args)
        {
            InfoFormat(format, args);
        }

        public virtual void DebugFormat(string format, object arg0)
        {
            InfoFormat(format, arg0);
        }

        public virtual void DebugFormat(string format, object arg0, object arg1)
        {
            InfoFormat(format, arg0, arg1);
        }

        public virtual void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            InfoFormat(format, arg0, arg1, arg2);
        }

        public virtual void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            InfoFormat(provider, format, args);
        }

        public virtual void Info(object message)
        {
            Console.WriteLine(message);
        }

        public virtual void Info(object message, Exception exception)
        {
            Console.WriteLine(message + "." + exception);
        }

        public virtual void InfoFormat(string format, params object[] args)
        {
            Console.WriteLine(string.Format(format, args));
        }

        public virtual void InfoFormat(string format, object arg0)
        {
            Console.WriteLine(string.Format(format, new object[]{arg0}));
        }

        public virtual void InfoFormat(string format, object arg0, object arg1)
        {
            Console.WriteLine(string.Format(format, new object[] { arg0, arg1 }));
        }

        public virtual void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            Console.WriteLine(string.Format(format, new object[] { arg0, arg1, arg2 }));
        }

        public virtual void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            Console.WriteLine(string.Format(provider, format, args));
        }

        public virtual void Warn(object message)
        {
            Info(message);
        }

        public virtual void Warn(object message, Exception exception)
        {
            Info(message, exception);
        }

        public virtual void WarnFormat(string format, params object[] args)
        {
            InfoFormat(format, args);
        }

        public virtual void WarnFormat(string format, object arg0)
        {
            InfoFormat(format, arg0);
        }

        public virtual void WarnFormat(string format, object arg0, object arg1)
        {
            InfoFormat(format, arg0, arg1);
        }

        public virtual void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            InfoFormat(format, arg0, arg1, arg2);
        }

        public virtual void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            InfoFormat(provider, format, args);
        }

        public virtual void Error(object message)
        {
            Info(message);
        }

        public virtual void Error(object message, Exception exception)
        {
            Info(message, exception);
        }

        public virtual void ErrorFormat(string format, params object[] args)
        {
            InfoFormat(format, args);
        }

        public virtual void ErrorFormat(string format, object arg0)
        {
            InfoFormat(format, arg0);
        }

        public virtual void ErrorFormat(string format, object arg0, object arg1)
        {
            InfoFormat(format, arg0, arg1);
        }

        public virtual void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            InfoFormat(format, arg0, arg1, arg2);
        }

        public virtual void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            InfoFormat(provider, format, args);
        }

        public virtual void Fatal(object message)
        {
            Info(message);
        }

        public virtual void Fatal(object message, Exception exception)
        {
            Info(message, exception);
        }

        public virtual void FatalFormat(string format, params object[] args)
        {
            InfoFormat(format, args);
        }

        public virtual void FatalFormat(string format, object arg0)
        {
            InfoFormat(format, arg0);
        }

        public virtual void FatalFormat(string format, object arg0, object arg1)
        {
            InfoFormat(format, arg0, arg1);
        }

        public virtual void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            InfoFormat(format, arg0, arg1, arg2);
        }

        public virtual void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            InfoFormat(provider, format, args);
        }

        public virtual bool IsDebugEnabled
        {
            get { return true; }
        }

        public virtual bool IsInfoEnabled
        {
            get { return true; }
        }

        public virtual bool IsWarnEnabled
        {
            get { return true; }
        }

        public virtual bool IsErrorEnabled
        {
            get { return true; }
        }

        public virtual bool IsFatalEnabled
        {
            get { return true; }
        }

        public virtual ILogger Logger
        {
            get { return null; }
        }
    }
}