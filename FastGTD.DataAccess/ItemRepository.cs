using NHibernate;
using NHibernate.Cfg;

namespace FastGTD.DataAccess
{
    public class ItemRepository
    {
        protected ISessionFactory _session_factory;

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

        protected ISession GetSession()
        {
            return _session_factory.OpenSession();
        }
    }
}