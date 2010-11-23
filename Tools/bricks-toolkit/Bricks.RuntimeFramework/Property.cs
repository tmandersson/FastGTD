using System.Reflection;

namespace Bricks.RuntimeFramework
{
    public class Property : CodeMember
    {
        public Property(PropertyInfo propertyInfo) : base(propertyInfo) {}

        public override string ToString()
        {
            return memberInfo.Name;
        }

        public virtual object Get(object parent)
        {
            return ((PropertyInfo) memberInfo).GetValue(parent, null);
        }

        public virtual void Set(object parent, object value)
        {
            ((PropertyInfo) memberInfo).SetValue(parent, value, null);
        }
    }
}