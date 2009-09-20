using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;

namespace FastGTD.DataAccess
{
    public abstract class ItemRepository<T>
    {
        private readonly ISessionFactory _session_factory;

        protected ItemRepository()
        {
            _session_factory = CreateSessionFactory();
        }

        protected abstract string GetTableName();

        public IList<T> GetAll()
        {
            ISession session = GetSession();
            return session.CreateCriteria(typeof(T)).List<T>();
        }

        public void DeleteAll()
        {
            ISession session = GetSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                session.Delete("from " + GetTableName());
                tx.Commit();
            }
        }

        public void Delete(T item)
        {
            ISession session = GetSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                session.Delete(item);
                tx.Commit();
            }
        }

        public void Save(T item)
        {
            ISession session = GetSession();

            using (ITransaction tx = session.BeginTransaction())
            {
                session.SaveOrUpdate(item);
                tx.Commit();
            }
        }

        public T GetById(int id)
        {
            ISession session = GetSession();
            return session.Load<T>(id);
        }

        private static ISessionFactory CreateSessionFactory()
        {
            var cfg = new Configuration();

            cfg.Properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            cfg.Properties.Add("connection.driver_class", "NHibernate.Driver.SQLite20Driver");
            cfg.Properties.Add("connection.connection_string", "Data Source=FastGTD.db");
            cfg.Properties.Add("dialect", "NHibernate.Dialect.SQLiteDialect");
            cfg.Properties.Add("query.substitutions", "true=1;false=0");

            cfg.AddAssembly("FastGTD.DataTransfer");
            return cfg.BuildSessionFactory();
        }

        protected ISession GetSession()
        {
            return _session_factory.OpenSession();
        }
    }
}