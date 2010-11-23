using System;
using System.Threading;

namespace Bricks.Core
{
    public class Clock
    {
        private readonly int timeout;
        private readonly int recheckAfterInMilliSeconds;
        private readonly long startTicks;

        public delegate object Do();
        public delegate bool Matched(object obj);
        
        public delegate bool Condition();

        public delegate void DoAndNoReturn();

        public delegate object Expired();

        public Clock(int timeoutInMilliseconds) : this(timeoutInMilliseconds, CoreAppXmlConfiguration.Instance.RecheckDurationInMilliseconds) {}

        public Clock(int timeoutInMilliseconds, int recheckAfterInMilliSeconds)
        {
            startTicks = DateTime.Now.Ticks;
            timeout = timeoutInMilliseconds;
            this.recheckAfterInMilliSeconds = recheckAfterInMilliSeconds;
        }

        /// <summary>
        /// Perform certain task till specified condition is matched
        /// </summary>
        /// <param name="do">Delegate executed repeatedly at recheck interval of Clock. The return value is used as argument for "matched" delegate.</param>
        /// <param name="matched">The return value of the "do" delegate passed as argument to this delegate. Return value of true would cause the
        /// Clock to stop.</param>
        /// <param name="expired">In case of a no match this delegate is called</param>
        /// <returns></returns>
        public virtual object Perform(Do @do, Matched matched, Expired expired)
        {
            while (true)
            {
                object o = @do();
                if (matched(o)) return o;
                if (TimeUp) return expired();
                Thread.Sleep(recheckAfterInMilliSeconds);
            }
        }

        public virtual void RunWhile(DoAndNoReturn @do, Condition condition, Expired expired)
        {
            while(condition())
            {
                @do();
                if (TimeUp) expired();
            }
        }

        public virtual bool TimeUp
        {
            get
            {
                long currentTicks = DateTime.Now.Ticks;
                return ((currentTicks - startTicks)/10000) > timeout;
            }
        }
    }
}