using System;
using System.Collections;
using NHibernate.Type;

namespace Repository
{
    public class NOOPInterceptor {
        public bool OnLoad(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            return false;
        }

        public void PreFlush(ICollection entities)
        {
            // no op
        }

        public object IsUnsaved(object entity)
        {
            return null;
        }

        public int[] FindDirty(object entity, object id, object[] currentState, object[] previousState,
                               string[] propertyNames, IType[] types)
        {
            return null;
        }

        public object Instantiate(Type type, object id)
        {
            return null;
        }
    }
}