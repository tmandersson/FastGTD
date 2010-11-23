using System;
using System.Collections.Generic;

namespace Bricks.Core
{
    public class DuplicateComparer<T> : IComparer<T>
    {
        private bool duplicateFound = false;

        public virtual int Compare(T one, T other)
        {
            int compare = ((IComparable) one).CompareTo(other);
            if (compare == 0 && !one.Equals(other))
                duplicateFound = true;
            return compare;
        }

        public virtual bool FoundDuplicates
        {
            get { return duplicateFound; }
        }
    }
}