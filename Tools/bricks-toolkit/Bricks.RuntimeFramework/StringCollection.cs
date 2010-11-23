using System.Collections;
using System.Collections.Generic;

namespace Bricks.RuntimeFramework
{
    public class StringCollection : BricksCollection<string>
    {
        public StringCollection(IEnumerable entities) : base(entities) {}
        public StringCollection(int capacity) : base(capacity) {}
        public StringCollection(IEnumerable<string> collection) : base(collection) {}
        public StringCollection(string obj) : base(obj) {}
        public StringCollection() {}

        public static StringCollection FromToString(ICollection collection)
        {
            StringCollection stringCollection = new StringCollection();
            foreach (object o in collection)
            {
                stringCollection.Add(o.ToString());
            }
            return stringCollection;
        }
    }
}