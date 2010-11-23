using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Bricks.Core
{
    public class TComparer<T> : IComparer<T>
    {
        protected readonly string[] propertyNames;
        private readonly ListSortDirection direction;

        public TComparer(ListSortDirection direction, params string[] propertyName)
        {
            propertyNames = propertyName;
            this.direction = direction;
        }

        public virtual int Compare(T x, T y)
        {
            int comparison = ComparePropertyValues(x, y, propertyNames[0], 0, false);
            return direction == ListSortDirection.Ascending ? comparison : -comparison;
        }

        protected virtual int ComparePropertyValues(T x, T y, string propertyName, int propertyIndex, bool ignoreCase)
        {
            PropertyInfo property = typeof (T).GetProperty(propertyName);
            object xValue = property.GetValue(x, null);
            object yValue = property.GetValue(y, null);
            if (xValue == null) return yValue == null ? 0 : -1;
            if (yValue == null) return 1;
            IComparable xComparable = xValue as IComparable;
            IComparable yComparable = yValue as IComparable;
            if (ignoreCase && xComparable == null)
            {
                xValue = xValue.ToString().ToUpper();
                yValue = yValue.ToString().ToUpper();
            }
            int comparison = (xComparable == null) ? xValue.ToString().CompareTo(yValue.ToString()) : xComparable.CompareTo(yComparable);
            if (comparison == 0 && propertyIndex < propertyNames.Length)
                return ComparePropertyValues(x, y, propertyNames[propertyIndex], ++propertyIndex, ignoreCase);
            else
                return comparison;
        }
    }
}