using System.Collections.Generic;
using Bricks.RuntimeFramework;

namespace Bricks.Core
{
    public class DistinctFinder<T>
    {
        private readonly string propertyName;
        private readonly Dictionary<object, T> distinctList;

        public DistinctFinder(string propertyName)
        {
            this.propertyName = propertyName;
            distinctList = new Dictionary<object, T>();
        }

        public virtual BricksCollection<T> DistinctList
        {
            get { return new BricksCollection<T>(distinctList.Values); }
        }

        public virtual bool Find(T obj)
        {
            object propertyValue = typeof (T).GetProperty(propertyName).GetValue(obj, null);
            if (!distinctList.ContainsKey(propertyValue)) distinctList.Add(propertyValue, obj);
            return true;
        }
    }
}