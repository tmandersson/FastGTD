using System;
using NAnt.Core;

namespace Bricks.NAnt
{
    public class ConsoleLogger
    {
        public void Log(Level messageLevel, string message)
        {
            Console.WriteLine(message);
        }

        public void Log(Exception exception)
        {
            Console.Error.WriteLine(exception);
        }
    }
}