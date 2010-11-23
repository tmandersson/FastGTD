using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Bricks.RuntimeFramework
{
    public class MethodInfos : BricksCollection<MethodInfo>
    {
        public MethodInfos() {}

        private MethodInfos(ICollection collection)
        {
            AddRange(collection);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            foreach (MethodInfo methodInfo in this)
                stringBuilder.Append(methodInfo.Name + " method in class " + methodInfo.DeclaringType.FullName).AppendLine();
            return stringBuilder.ToString();
        }

        public override BricksCollection<MethodInfo> Filter(Predicate<MethodInfo> predicate)
        {
            BricksCollection<MethodInfo> collection = base.Filter(predicate);
            return new MethodInfos(collection);
        }
    }
}