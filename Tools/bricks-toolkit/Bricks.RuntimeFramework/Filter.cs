using System;
using System.Collections;
using System.Collections.Generic;

namespace Bricks.Core
{
    public interface Filter<T>
    {
        bool Matches(T thing);
    }

    public abstract class BaseFilter<T> : Filter<T>
    {
        public virtual BaseFilter<T> Or(Filter<T> right)
        {
            return new OrFilter<T>(this, right);
        }

        public virtual Filter<T> And(Filter<T> right)
        {
            return new AndFilter<T>(this, right);
        }

        public abstract bool Matches(T thing);
    }

    public class AllFilter<T> : BaseFilter<T>
    {
        public override bool Matches(T thing)
        {
            throw new NotImplementedException();
        }
    }

    internal class AndFilter<T> : Filter<T>
    {
        private readonly Filter<T>[] filters;

        public AndFilter(params Filter<T>[] filters)
        {
            this.filters = filters;
        }

        public virtual bool Matches(T thing)
        {
            foreach (Filter<T> filter in filters)
            {
                if (!filter.Matches(thing))
                    return false;
            }
            return true;
        }
    }

    public class OrFilter<T> : BaseFilter<T>
    {
        private readonly Filter<T>[] filters;

        public OrFilter(params Filter<T>[] filters)
        {
            this.filters = filters;
        }

        public override bool Matches(T thing)
        {
            foreach (Filter<T> filter in filters)
            {
                if (filter.Matches(thing))
                    return true;
            }
            return false;
        }
    }

    public class ListFilter<T>
    {
        private readonly Filter<T> filter;

        public ListFilter(Filter<T> filter)
        {
            this.filter = filter;
        }

        public virtual IList<T> select(IEnumerable e)
        {
            List<T> matches = new List<T>();
            foreach (T thing in e)
            {
                if (filter.Matches(thing))
                    matches.Add(thing);
            }
            return matches;
        }
    }
}