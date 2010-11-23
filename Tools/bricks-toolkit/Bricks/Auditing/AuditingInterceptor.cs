using System;
using System.Collections;
using System.Reflection;
using System.Security;
using Bricks.Core;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Type;

namespace Repository
{
    public class AuditingInterceptor : NOOPInterceptor, IInterceptor
    {
        private readonly Configuration configuration;
        private ISessionFactory sessionFactory;
        private NhibernateChangeDetectingStrategy changeStrategy;
        [ThreadStatic] private static IList objectsFlushed;
        private SecurityContext securityContext;

        public AuditingInterceptor(Configuration configuration)
        {
            this.configuration = configuration;
        }

        private ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    sessionFactory = configuration.BuildSessionFactory();
                    changeStrategy = new NhibernateChangeDetectingStrategy(sessionFactory);
                }
                return sessionFactory;
            }
        }

        public AuditingInterceptor(ISessionFactory sessionFactory, NhibernateChangeDetectingStrategy changeStrategy)
        {
            this.sessionFactory = sessionFactory;
            this.changeStrategy = changeStrategy;
        }

        public virtual bool OnFlushDirty(object obj, object id, object[] currentState, object[] previousState, string[] propertyNames,
                                         IType[] types)
        {
            if (ShouldAudit(obj))
            {
                if (objectsFlushed == null) objectsFlushed = new ArrayList();
                objectsFlushed.Add(obj);

                ISession session = AbstractRepository.AuditSession(SessionFactory);
                object persistedObjectId = entity.GetId();
                AbstractEntity preUpdateState = (AbstractEntity) session.Get(obj.GetType(), persistedObjectId);
                preUpdateState = (AbstractEntity) TargetForceLoader.GetTargetValue(preUpdateState);
                UpdateRootObject(entity, preUpdateState);
            }
            return false;
        }

        private static bool ShouldAudit(object obj)
        {
            return obj is AbstractEntity && new TypeAttributes(obj.GetType()).IsMarked(typeof (AuditAttribute));
        }

        public void UpdateRootObject(AbstractEntity newObject, AbstractEntity preUpdateState)
        {
            changeStrategy.FindChanges(newObject, preUpdateState, new NhibernateChangeDetectingStrategy.ChangeHandler(OnChanges));
        }

        protected void OnChanges(AbstractEntity newObject, string propertyName, string oldValue, string newValue)
        {
            if (!S.Value(oldValue).Equals(S.Value(newValue)))
                Add(AuditLog.Update(newObject, propertyName, oldValue, newValue, GetLoggedInUserName()));
        }

        protected virtual string GetLoggedInUserName()
        {
            securityContext = AbstractRepository.ServerContext.SecurityContext;
            return securityContext.Employee.ToString();
        }

        protected virtual void Add(AuditLog auditLog)
        {
            AbstractRepository.CurrentSession.Save(auditLog);
        }

        public virtual bool OnSave(object obj, object id, object[] state, string[] propertyNames, IType[] types)
        {
            if (ShouldAudit(obj))
            {
                AbstractEntity entity = (AbstractEntity) obj;
                Add(AuditLog.Create(entity, GetLoggedInUserName()));
            }
            return false;
        }

        public virtual void OnDelete(object obj, object id, object[] state, string[] propertyNames, IType[] types)
        {
            if (ShouldAudit(obj))
            {
                AbstractEntity entity = (AbstractEntity) obj;
                Add(AuditLog.Remove(entity, GetLoggedInUserName()));
            }
        }

        public virtual void PostFlush(ICollection entities) {}

        public static void Clear()
        {
            if (objectsFlushed != null) objectsFlushed.Clear();
        }
    }
}
