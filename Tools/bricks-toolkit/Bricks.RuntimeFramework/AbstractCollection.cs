using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Bricks.Core
{
    [Serializable]
    public abstract class AbstractCollection<T> : IList, IEnumerable<T>
    {
        protected List<T> entities;
        private RemovedEntities removedEntities;

        public virtual T this[int index]
        {
            get { return Array[index]; }
            set { Array[index] = value; }
        }

        private RemovedEntities RemovedEntities
        {
            get
            {
                if (removedEntities == null) removedEntities = new RemovedEntities();
                return removedEntities;
            }
        }

        public virtual T[] Array
        {
            get { return ToArray(entities); }
        }

        public virtual void CopyTo(Array array, int index)
        {
            entities.CopyTo(ToArray(array), index);
        }

        public virtual int Count
        {
            get { return entities.Count; }
        }

        public virtual object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public virtual bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public virtual IEnumerator GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            List<T> list = new List<T>();
            foreach (T t in entities)
                list.Add(t);
            return list.GetEnumerator();
        }

        public virtual List<T> List
        {
            get { return ToList(entities); }
            set
            {
                entities.Clear();
                entities.AddRange(value);
            }
        }

        public virtual AbstractCollection<T> Add(T entity)
        {
            if (entity != null)
            {
                entities.Add(entity);
                RemovedEntities.Remove(entity);
            }
            return this;
        }

        public virtual AbstractCollection<T> AddSavedEntity(T entity)
        {
            if (entity != null)
            {
                entities.Add(entity);
                RemovedEntities.Remove(entity);
            }
            return this;
        }

        public virtual void Remove(T entity)
        {
            if (entity != null)
            {
                RemovedEntities.Add(entity);
                entities.Remove(entity);
            }
        }

        public virtual void Detach(T entity)
        {
            if (entity != null)
                entities.Remove(entity);
        }

        public virtual ArrayList Deleted
        {
            get { return RemovedEntities; }
        }

        public virtual void AddSavedEntities(AbstractCollection<T> items)
        {
            entities.AddRange(items);
        }

        public virtual AbstractCollection<T> AddRange(ICollection items)
        {
            if (items == null)
                return this;
            entities.AddRange((AbstractCollection<T>) items);
            return this;
        }

        public virtual AbstractCollection<T> AddRangeExcept(ICollection items, params T[] excludeds)
        {
            AddRange(items);
            foreach (T excluded in excludeds)
            {
                if (excluded != null)
                    entities.Remove(excluded);
            }
            return this;
        }

        public virtual AbstractCollection<T> AddRangeExcept(ICollection items, ICollection excludedItems)
        {
            AddRangeExcept(items, delegate(T obj) { return !ListContains(excludedItems, obj); });
            return this;
        }

        private bool ListContains(ICollection items, T t)
        {
            foreach (object o in items)
                if (t.Equals(o)) return true;
            return false;
        }

        public virtual AbstractCollection<T> AddRangeExcept(ICollection items, Predicate<T> predicate)
        {
            foreach (T t in items)
            {
                if (!predicate.Invoke(t))
                    entities.Add(t);
            }
            return this;
        }

        public virtual void RemoveList(ICollection list)
        {
            entities.RemoveAll(delegate(T obj) { return ListContains(list, obj); });
            RemovedEntities.AddRange(list);
        }

        public virtual void DetachAll(ICollection list)
        {
            entities.RemoveAll(delegate(T obj) { return ListContains(list, obj); });
        }

        public override bool Equals(object obj)
        {
            AbstractCollection<T> otherEntities = obj as AbstractCollection<T>;
            if (otherEntities == null) return false;
            return entities.Equals(otherEntities.entities);
        }

        public override int GetHashCode()
        {
            if (entities != null)
                return entities.GetHashCode();
            return base.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (T t in this)
            {
                builder.Append(t.ToString()).Append(" ");
            }
            return builder.ToString();
        }

        public virtual bool Contains(T t)
        {
            return entities.Contains(t);
        }

        public virtual bool HasDuplicates()
        {
            DuplicateComparer<T> comparer = new DuplicateComparer<T>();
            List.Sort(comparer);
            return comparer.FoundDuplicates;
        }

        public virtual void Clear()
        {
            entities.Clear();
            RemovedEntities.AddRange(List);
        }

        int IList.Add(object value)
        {
            Add((T) value);
            return entities.Count - 1;
        }

        bool IList.Contains(object value)
        {
            return entities.Contains((T) value);
        }

        int IList.IndexOf(object value)
        {
            int i = 0;
            foreach (T entity in entities)
            {
                if (entity.Equals(value))
                    return i;
                i++;
            }
            return -1;
        }

        void IList.Insert(int index, object value)
        {
            Add((T) value);
        }

        void IList.Remove(object value)
        {
            Remove((T) value);
        }

        void IList.RemoveAt(int index)
        {
            Remove(this[index]);
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = (T) value; }
        }

        bool IList.IsReadOnly
        {
            get { return false; }
        }

        public virtual bool IsFixedSize
        {
            get { return false; }
        }

        public static T[] ToArray(ICollection collection)
        {
            if (collection == null) return new T[0];
            T[] array = new T[collection.Count];
            int count = 0;
            IEnumerator enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
                array[count++] = (T) enumerator.Current;
            return array;
        }

        public static List<T> ToList(IList entities)
        {
            if (entities == null) return new List<T>();
            List<T> list = new List<T>();

            IEnumerator enumerator = entities.GetEnumerator();
            while (enumerator.MoveNext())
                list.Add((T) enumerator.Current);

            return list;
        }
    }

    [Serializable]
    public class RemovedEntities : ArrayList
    {
        public delegate void EntitiesRemoved(ICollection entities);

        public static event EntitiesRemoved LunarEntitiesRemoved = delegate { };

        [ThreadStatic] private static EntityRemovedListener entityRemovedListener;

        public override int Add(object value)
        {
            int index = base.Add(value);
            ArrayList list = new ArrayList();
            list.Add(value);
            ProcessEntitiesRemoved(list);
            return index;
        }

        public override void AddRange(ICollection c)
        {
            base.AddRange(c);
            ProcessEntitiesRemoved(c);
        }

        private static void ProcessEntitiesRemoved(ICollection c)
        {
            LunarEntitiesRemoved(c);
            if (entityRemovedListener != null)
                entityRemovedListener.Removed(c);
        }

        public static void AddListener(EntityRemovedListener listener)
        {
            entityRemovedListener = listener;
        }
    }

    public interface EntityRemovedListener
    {
        void Removed(ICollection entities);
    }
}