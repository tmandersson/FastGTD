using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Bricks.RuntimeFramework
{
    /// <summary>
    /// Represents a Type in .NET. It uses reflection to provide access to Type information.
    /// </summary>
    public class Class : CodeMember
    {
        private const string startStringOfProxyClass = "CProxyType";
        private const BindingFlags bindingFlag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase;
        private MethodInfos nonVirtualMethodInfos;

        public delegate void FieldDelegate(FieldInfo fieldInfo);
        public delegate void ConstructorDelegate(ConstructorInfo constructorInfo);

        private Classes classes;
        private Type ignoredType;

        public Class(Type type) : base(type)
        {
        }

        public virtual Type ClassType
        {
            get { return (Type) memberInfo; }
        }

        private Classes classHierarchy
        {
            get
            {
                if (classes != null) return classes;

                classes = new Classes(this);
                Type currentType = ClassType.BaseType;
                while (typeof(object) != currentType && !currentType.Equals(ignoredType))
                {
                    classes.Add(new Class(currentType));
                    currentType = currentType.BaseType;
                }
                return classes;
            }
        }

        /// <summary>
        /// Providers a list of all the Non-virtual methods and properties. MethodInfos would get created first time they are accessed.
        /// </summary>
        public virtual MethodInfos NonVirtuals
        {
            get
            {
                if (nonVirtualMethodInfos == null)
                {
                    nonVirtualMethodInfos = new MethodInfos();
                    MethodInfo[] methodInfos = ClassType.GetMethods(bindingFlag);
                    foreach (MethodInfo methodInfo in methodInfos)
                    {
                        if (methodInfo.IsPrivate || !methodInfo.DeclaringType.Equals(ClassType) || methodInfo.Name.StartsWith("<")) continue;
                        if (!methodInfo.IsVirtual || methodInfo.IsFinal) nonVirtualMethodInfos.Add(methodInfo);
                    }
                }
                return nonVirtualMethodInfos;
            }
        }

        public virtual object New(params object[] arguments)
        {
            var types = new ArrayList();
            foreach (object o in arguments)
                types.Add(o.GetType());

            ConstructorInfo constructor = ClassType.GetConstructor(bindingFlag, null, (Type[]) types.ToArray(typeof (Type)), null);
            if (constructor == null) throw new BricksException("No constructor found with matching arguments found in type " + ClassType.FullName);
            return constructor.Invoke(arguments);
        }

        public virtual void EachField(FieldDelegate fieldDelegate)
        {
            foreach (Class @class in classHierarchy)
            {
                foreach (FieldInfo field in @class.ClassType.GetFields(bindingFlag))
                {
                    fieldDelegate.Invoke(field);
                }
            }
        }

        public virtual void EachConstructor(ConstructorDelegate constructorDelegate)
        {
            foreach (ConstructorInfo constructor in ClassType.GetConstructors(bindingFlag))
            {
                constructorDelegate.Invoke(constructor);
            }
        }

        public static bool IsProxy(object o)
        {
            return o.GetType().Name.StartsWith(startStringOfProxyClass);
        }

        public virtual FieldInfo GetField(string fieldName)
        {
            FieldInfo field = ClassType.GetField(fieldName, bindingFlag);
            if (field != null) return field;
            Class baseClass = BaseClass;
            if (baseClass != null) return baseClass.GetField(fieldName);
            return null;
        }

        private Class BaseClass
        {
            get
            {
                if (ClassType.BaseType.Equals(typeof(object))) return null;
                return new Class(ClassType.BaseType);
            }
        }

        public virtual string Name
        {
            get { return ClassType.Name; }
        }
        public virtual Classes SubClassesInAssembly()
        {
            Types types = new Types(ClassType.Assembly.GetTypes());
            return new Classes(types.FindAll(delegate(Type item)
                                                 {
                                                     if (!item.IsClass || ClassType.Equals(item)) return false;
                                                     return ClassType.IsAssignableFrom(item);
                                                 }));
        }

        public override string ToString()
        {
            return ClassType.FullName;
        }

        public virtual bool IsProperty(string name)
        {
            return ClassType.GetProperty(name) != null;
        }

        public virtual Method GetMethod(string methodName)
        {
            foreach (Class @class in classHierarchy)
            {
                MethodInfo info = @class.ClassType.GetMethod(methodName, bindingFlag);
                if (info != null) return new Method(info);
            }
            return null;
        }

        public virtual Property GetProperty(string propertyName)
        {
            foreach (Class @class in classHierarchy)
            {
                PropertyInfo info = @class.ClassType.GetProperty(propertyName, bindingFlag);
                if (info != null) return new Property(info);
            }
            return null;
        }

        public virtual void IgnoreClass(Type ignoredType)
        {
            this.ignoredType = ignoredType;
        }

        public virtual List<Type> AllNonAutoSerializableTypes()
        {
            if (ClassType.IsArray) throw new BricksException("Cannot find serializable types from array");
            var types = new List<Type>();
            AddTypes(types);
            return types;
        }

        private void AddTypes(List<Type> types)
        {
            if (ClassType.IsPrimitive || ClassType.Equals(typeof(object)) || ClassType.Equals(typeof(string)) || types.Contains(ClassType))
            {
                return;
            }
            types.Add(ClassType);
            EachField(info => new Class(info.FieldType).AddTypes(types));
        }
    }
}