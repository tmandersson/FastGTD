using System;

namespace Bricks.VisualStudio2005
{
    public class VisualStudioException : Exception
    {
        public VisualStudioException(string message) : base(message) {}
        public VisualStudioException(string message, Exception exception) : base(message, exception) {}
    }
}