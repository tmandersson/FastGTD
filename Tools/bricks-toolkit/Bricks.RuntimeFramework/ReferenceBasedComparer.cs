using System.Collections;

namespace Bricks.RuntimeFramework
{
    public class ReferenceBasedComparer : IEqualityComparer
    {
        bool IEqualityComparer.Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }

        public virtual int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }
}