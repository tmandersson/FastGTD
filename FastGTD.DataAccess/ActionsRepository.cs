using System;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NHibernate;
using NHibernate.Cfg;

namespace FastGTD.DataAccess
{
    public class ActionsRepository : IActionsListPersistence
    {
        private readonly ISessionFactory _session_factory;

        public ActionsRepository()
        {
            _session_factory = CreateSessionFactory();
        }

        public void Save(ActionItem item)
        {
            ISession session = GetSession();

            using (ITransaction tx = session.BeginTransaction())
            {
                session.SaveOrUpdate(item);
                tx.Commit();
            }
        }

        public ActionItem GetById(int id)
        {
            ISession session = GetSession();
            return session.Load<ActionItem>(id);
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

        private ISession GetSession()
        {
            return _session_factory.OpenSession();
        }
    }
}