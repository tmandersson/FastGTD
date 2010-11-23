using Bricks.Objects;

namespace Lunar.Shared.Common.DynamicProxy
{
    public class ClassForNullTest : NullableObject
    {
        public virtual bool IsNull
        {
            get { return false; }
        }

        public virtual bool IsNotNull
        {
            get { return true; }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}