using System;

namespace Bricks.RuntimeFramework
{
    public class LeafRegistry
    {
        private Types leaves = new Types();
        private Types ignored = new Types();
        private BricksDictionary objectsNotToBeMarked = new NullBricksDictionary();

        public void AddLeaf(Type type)
        {
            if (!Contains(type)) leaves.Add(type);
        }

        public bool Contains(Type type)
        {
            return leaves.IsAssignableFrom(type);
        }

        public bool IsIgnored(Type type)
        {
            return ignored.IsAssignableFrom(type);
        }

        public void IgnoreLeaf(Type type)
        {
            ignored.Add(type);
        }

        public void RemoveLeaf(Type type)
        {
            leaves.Remove(type);
        }

        public void DoNotMarkObjectsIn(BricksDictionary dictionary)
        {
            objectsNotToBeMarked = dictionary;
        }

        public bool ShouldNotMark(object o)
        {
            return objectsNotToBeMarked.Exists(o);
        }
    }
}