using System.Collections.Generic;
using FastGTD.DataTransfer;
using NHibernate;
using NHibernate.Cfg;

namespace FastGTD.DataAccess
{
    public class InBoxItemRepository : IInBoxPersistenceProvider
    {
        private readonly ISessionFactory _session_factory;

        public InBoxItemRepository()
        {
            _session_factory = CreateSessionFactory();
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

        public void Save(InBoxItem item)
        {
            ISession session = GetSession();

            using (ITransaction tx = session.BeginTransaction())
            {
                session.SaveOrUpdate(item);
                tx.Commit();
            }
        }

        private ISession GetSession()
        {
            return _session_factory.OpenSession();
        }

        public void Delete(InBoxItem item)
        {
            ISession session = GetSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                session.Delete(item);
                tx.Commit();
            }
        }

        public InBoxItem GetById(int id)
        {
            ISession session = GetSession();
            return session.Load<InBoxItem>(id);
        }

        public IList<InBoxItem> GetAll()
        {
            ISession session = GetSession();
            return session.CreateCriteria(typeof (InBoxItem)).List<InBoxItem>();
        }

        public void DeleteAll()
        {
            ISession session = GetSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                session.Delete("from InBoxItem");
                tx.Commit();
            }
        }
    }
}
