using System;

namespace Bricks.NAnt.Build
{
    public class BricksBuildException : Exception
    {
        public BricksBuildException(string message) : base(message) {}
        public BricksBuildException(string message, Exception exception) : base(message, exception) {}
    }
}