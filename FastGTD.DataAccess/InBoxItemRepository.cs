using System.Collections.Generic;
using FastGTD.DataTransfer;
using NHibernate;
using NHibernate.Cfg;

namespace FastGTD.DataAccess
{
    // TODO: Implement some repository interface which domain/core of FastGTD should consume. 
    // To avoid coupling to this particular data access implementation.
    public class InBoxItemRepository : IInBoxItemPersister
    {
        protected ISessionFactory _session_factory;

        public InBoxItemRepository()
        {
            _session_factory = CreateSessionFactory();
        }

        protected static ISessionFactory CreateSessionFactory()
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

        public InBoxItem CreateNew(string name)
        {
            return InBoxItem.CreateNew(this);;
        }

        public InBoxItem GetByID(int id)
        {
            ISession session = GetSession();
            return session.Load<InBoxItem>(id);
        }

        public IList<InBoxItem> GetAll()
        {
            ISession session = GetSession();
            return session.CreateCriteria(typeof (InBoxItem)).List<InBoxItem>();
        }
    }
}
