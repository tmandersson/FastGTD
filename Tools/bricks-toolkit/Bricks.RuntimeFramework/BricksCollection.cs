using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Bricks.Core;

namespace Bricks.RuntimeFramework
{
    [Serializable]
    public class BricksCollection<T> : List<T>, TypeCollection
    {
        public BricksCollection() { }

        public BricksCollection(T obj)
            : this()
        {
            Add(obj);
        }

        public BricksCollection(IEnumerable<T> collection) : base(collection) { }

        public BricksCollection(int capacity) : base(capacity) { }

        public BricksCollection(params T[] collection)
        {
            foreach (T t in collection)
            {
                Add(t);
            }
        }

        public virtual Type ItemType
        {
            get { return typeof(T); }
        }

        public BricksCollection(IEnumerable entities)
            : this()
        {
            foreach (T o in entities) Add(o);
        }

        public virtual BricksCollection<T> Clone()
        {
            return new BricksCollection<T>(this);
        }

        public static BricksCollection<T> Clone(ICollection collection)
        {
            return new BricksCollection<T>(collection);
        }

        public virtual T Last
        {
            get { return this[Count - 1]; }
        }

        public virtual T First
        {
            get { return this[0]; }
        }

        public virtual bool IsEmpty
        {
            get { return Count == 0; }
        }

        public virtual bool IsNotEmpty
        {
            get { return !IsEmpty; }
        }

        public virtual void AddRange(ICollection collection)
        {
            foreach (T t in collection)
            {
                Add(t);
            }
        }

        public virtual List<T> SortedList(ListSortDirection direction, params string[] propertyNames)
        {
            List<T> sortedList = Clone();
            sortedList.Sort(new TComparer<T>(direction, propertyNames));
            return sortedList;
        }

        public virtual List<T> SortedList(IComparer<T> comparer)
        {
            return SortedList(comparer.Compare);
        }

        public virtual List<T> SortedList(Comparison<T> comparison)
        {
            List<T> sortedList = Clone();
            sortedList.Sort(comparison);
            return sortedList;
        }

        public virtual ArrayList SortedList(IComparer comparer)
        {
            ArrayList sortedList = new ArrayList(Clone());
            sortedList.Sort(comparer);
            return sortedList;
        }

        public virtual bool Has(Predicate<T> predicate)
        {
            return Filter(predicate).Count > 0 ? true : false;
        }

        public virtual BricksCollection<T> Filter(Predicate<T> predicate)
        {
            BricksCollection<T> filtered = new BricksCollection<T>();
            foreach (T entity in this)
                if (predicate(entity)) filtered.Add(entity);
            return filtered;
        }

        public virtual BricksCollection<T> Filter(Filter<T> filter)
        {
            BricksCollection<T> filtered = new BricksCollection<T>();
            foreach (T entity in this)
                if (filter.Matches(entity)) filtered.Add(entity);
            return filtered;
        }

        public virtual BricksCollection<T> Filter<U>(PropertyEvaluator<T, U> delegateMethod, U expectedValue)
        {
            BricksCollection<T> filtered = new BricksCollection<T>();
            foreach (T entity in this)
                if (delegateMethod.Invoke(entity, expectedValue)) filtered.Add(entity);
            return filtered;
        }

        public virtual BricksCollection<T> Distinct(string property)
        {
            DistinctFinder<T> distinctFinder = new DistinctFinder<T>(property);
            Clone().FindAll(distinctFinder.Find);
            return distinctFinder.DistinctList;
        }

        public virtual void MoveRange(BricksCollection<T> collection)
        {
            AddRange(collection);
            collection.Clear();
        }

        public virtual void Replace(BricksCollection<T> collection)
        {
            Clear();
            AddRange(collection);
        }

        public virtual void Merge(ICollection collection)
        {
            if (collection == null) return;
            foreach (T t in collection)
            {
                if (Contains(t))
                    Remove(t);
                Add(t);
            }
        }

        public virtual bool AtleastOneMatches(Predicate<T> predicate)
        {
            foreach (T entity in this)
                if (predicate.Invoke(entity)) return true;
            return false;
        }

        public override string ToString()
        {
            string[] strings = new string[Count];
            for(int i = 0; i < Count; i++)
            {
                strings[i] = this[i].ToString();
            }
            return string.Join(",", strings);
        }

        public virtual bool Contains(Predicate<T> predicate)
        {
            return !Equals(Find(predicate), default(T));
        }

        /// <summary>
        /// Dependent on the order of the items
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool ItemwiseEquals(BricksCollection<T> other)
        {
            if (other == null || other.Count != Count) return false;
            for(int i = 0; i < Count; i++)
            {
                if (!other[i].Equals(this[i])) return false;
            }
            return true;
        }
    }

    public interface TypeCollection
    {
        Type ItemType { get; }
    }

    public delegate void EachDelegate<T>(T entity);

    public delegate bool PropertyEvaluator<T, U>(T entity, U expectedValue);
}