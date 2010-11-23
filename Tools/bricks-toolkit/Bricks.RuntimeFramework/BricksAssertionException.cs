using System;

namespace Bricks.RuntimeFramework
{
    public class BricksAssertionException : Exception
    {
        public BricksAssertionException(string message) : base(message) {}
        public BricksAssertionException(string message, Exception exception) : base(message, exception) {}
    }
}