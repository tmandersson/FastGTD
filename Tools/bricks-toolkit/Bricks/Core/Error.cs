using System;

namespace Bricks.Core
{
    public class Error
    {
        private readonly object key;
        private readonly string message;

        public Error(object key) : this(key, "") {}

        public Error(object key, string message)
        {
            this.key = key;
            this.message = message;
        }

        public virtual object Key
        {
            get { return key; }
        }

        public virtual string Message
        {
            get { return message; }
        }

        public override bool Equals(object obj)
        {
            Error error = obj as Error;
            if (obj == null) return false;
            return key.Equals(error.key);
        }

        public override int GetHashCode()
        {
            return key.GetHashCode();
        }

        public static Error Make(Enum key)
        {
            return new Error(key, key.ToString());
        }
    }

    public class ListItemError : Error
    {
        private readonly int index;

        public ListItemError(object property, string message, int index) : base(property, message)
        {
            this.index = index;
        }

        public virtual int Index
        {
            get { return index; }
        }
    }

    public class LogicalListItemError : Error
    {
        private readonly object obj;

        public LogicalListItemError(object property, string message, object obj) : base(property, message)
        {
            this.obj = obj;
        }

        public virtual object Obj
        {
            get { return obj; }
        }
    }
}