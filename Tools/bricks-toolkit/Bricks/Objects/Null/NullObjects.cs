using System;
using System.Collections;
using System.Collections.Generic;
using Bricks;
using Bricks.DynamicProxy;

namespace Lunar.Client.Common
{
    public class NullObjects
    {
        private IDictionary nullObjects = new Dictionary<Type, object>();
        private IDictionary codedObjects = new Dictionary<Type, object>();
        private IDictionary<string, object> defaultReturnValues = new Dictionary<string, object>();

        public virtual T Get<T>()
        {
            return (T) Get(typeof (T));
        }

        public virtual T Get<T>(IDictionary<string, object> returnValues)
        {
            return (T) Get(typeof (T), returnValues);
        }

        public virtual IDictionary<string, object> DefaultReturnValues
        {
            set { defaultReturnValues = value; }
        }

        public virtual object Get(Type type)
        {
            return Get(type, defaultReturnValues);
        }

        public virtual object Get(Type type, IDictionary<string, object> returnValues)
        {
            if (codedObjects.Contains(type)) return codedObjects[type];
            if (nullObjects.Contains(type)) return nullObjects[type];

            CachedDynamicProxyGenerator proxyGenerator = new CachedDynamicProxyGenerator();
            nullObjects.Add(type, proxyGenerator.GetProxy(type, new NullInterceptor(proxyGenerator, returnValues)));

            return nullObjects[type];
        }

        public virtual void Add(object nullObject)
        {
            if (nullObjects.Contains(nullObject.GetType()) || codedObjects.Contains(nullObject.GetType()))
                throw new BricksException("There is already a NullObject for " + nullObject.GetType().FullName);
        }

        public virtual void Clear(Type type)
        {
            if (nullObjects.Contains(type)) nullObjects.Remove(type);
            if (codedObjects.Contains(type)) codedObjects.Remove(type);
        }

        public virtual void ClearAll()
        {
            nullObjects.Clear();
            codedObjects.Clear();
        }
    }
}