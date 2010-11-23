using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bricks.RuntimeFramework
{
    public class CodeMember
    {
        protected MemberInfo memberInfo;

        public CodeMember(MemberInfo memberInfo)
        {
            this.memberInfo = memberInfo;
        }

        public virtual bool HasAttribute(Type attributeType)
        {
            return memberInfo.GetCustomAttributes(attributeType, true).Length != 0;
        }

        public virtual T Attribute<T>() where T : Attribute
        {
            List<T> tees = Attributes<T>();
            if (tees.Count == 0) throw new BricksException(string.Format("No attribute of type {0} found on type {1}.", typeof (T), memberInfo));
            return tees[0];
        }

        public virtual List<T> Attributes<T>() where T : Attribute
        {
            object[] objects = memberInfo.GetCustomAttributes(typeof (T), false);
            List<T> tees = new List<T>();
            foreach (T t in objects) tees.Add(t);
            return tees;
        }
    }
}