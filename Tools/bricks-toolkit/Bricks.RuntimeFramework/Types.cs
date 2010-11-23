using System;
using System.Collections.Generic;

namespace Bricks.RuntimeFramework
{
    public class Types : List<Type>
    {
        public Types() {}
        public Types(params Type[] types) : base(types) {}

        public virtual bool IsAssignableFrom(Type type)
        {
            foreach (Type item in this)
                if (item.IsAssignableFrom(type)) return true;
            return false;
        }
    }
}