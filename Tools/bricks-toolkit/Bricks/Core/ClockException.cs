using System;

namespace Bricks.Core
{
    public class ClockException : Exception
    {
        public ClockException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ClockException(string message) : base(message)
        {
        }
    }
}