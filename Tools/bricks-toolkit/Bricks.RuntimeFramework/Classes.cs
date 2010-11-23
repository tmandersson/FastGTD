using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Bricks.RuntimeFramework
{
    public class Classes : List<Class>
    {
        public Classes(IEnumerable types)
        {
            Initialize(types);
        }

        public Classes(IEnumerable<Class> classes)
        {
            AddRange(classes);
        }

        public Classes(params Class[] classes)
        {
            AddRange(classes);
        }

        public Classes(Assembly assembly) : this(assembly.GetTypes()) {}

        private void Initialize(IEnumerable types)
        {
            foreach (Type type in types) Add(new Class(type));
        }

        public virtual MethodInfos NonVirtuals
        {
            get
            {
                MethodInfos methodInfos = new MethodInfos();
                foreach (Class @class in this) methodInfos.AddRange(@class.NonVirtuals);
                return methodInfos;
            }
        }

        public virtual Classes WithoutAttribute(Type attributeType)
        {
            return new Classes(FindAll(delegate(Class @class) { return !@class.HasAttribute(attributeType); }));
        }

        public virtual Classes Filter(Predicate<Class> predicate)
        {
            return new Classes(FindAll(predicate));
        }
    }
}