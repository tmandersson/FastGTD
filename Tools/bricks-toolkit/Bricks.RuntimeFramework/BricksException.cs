using System;

namespace Bricks
{
    public class BricksException : Exception
    {
        public BricksException(string message) : base(message) {}

        public BricksException(string message, Exception innerException):base(message,innerException){}
    }
}