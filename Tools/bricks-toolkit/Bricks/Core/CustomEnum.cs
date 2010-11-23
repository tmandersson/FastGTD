using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bricks.Core
{
    [Serializable]
    public class CustomEnum
    {
        protected string name;

        protected CustomEnum() {}

        public CustomEnum(string name)
        {
            this.name = name;
        }

        public virtual string Name
        {
            get { return name; }
        }

        public override string ToString()
        {
            return name;
        }

        public override int GetHashCode()
        {
            return (name != null) ? name.GetHashCode() : base.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!GetType().IsAssignableFrom(other.GetType()) && !other.GetType().IsAssignableFrom(GetType()))
                return false;

            CustomEnum customEnum = (CustomEnum) other;
            return customEnum.name.Equals(name);
        }

        //Used by combo box data binding
        public virtual CustomEnum Value
        {
            get { return this; }
        }

        public static CustomEnum ValueOf<T>(string value)
        {
            return ValueOf(typeof (T), value);
        }

        public static CustomEnum ValueOf(Type type, string value)
        {
            foreach (FieldInfo info in type.GetFields(BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public))
            {
                if (info.FieldType.IsAssignableFrom(type))
                {
                    CustomEnum customEnum = (CustomEnum) info.GetValue(type);
                    if (customEnum.Name.Equals(value)) return customEnum;
                }
            }
            return null;
        }

        public static List<T> AllTypes<T>()
        {
            List<T> allTypes = new List<T>();
            Type type = typeof (T);
            foreach (FieldInfo info in type.GetFields())
                if (type.Equals(info.FieldType)) allTypes.Add((T) info.GetValue(type));
            return allTypes;
        }

        public static List<T> OnlyTypes<T>(params CustomEnum[] customEnums)
        {
            List<T> onlyTypes = new List<T>();
            Type type = typeof (T);
            foreach (CustomEnum customEnum in customEnums)
            {
                foreach (FieldInfo info in type.GetFields())
                {
                    object value = info.GetValue(type);
                    if (customEnum.Name.Equals(value.ToString()) && type.Equals(info.FieldType))
                        onlyTypes.Add((T) value);
                }
            }
            return onlyTypes;
        }
    }
}