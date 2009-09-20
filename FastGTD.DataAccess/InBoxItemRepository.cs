using System.Collections.Generic;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NHibernate;

namespace FastGTD.DataAccess
{
    public class InBoxItemRepository : ItemRepository, IItemPersistence<InBoxItem>
    {
        public InBoxItemRepository()
        {
            _session_factory = CreateSessionFactory();
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

        public void Delete(InBoxItem item)
        {
            ISession session = GetSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                session.Delete(item);
                tx.Commit();
            }
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

        public InBoxItem GetById(int id)
        {
            ISession session = GetSession();
            return session.Load<InBoxItem>(id);
        }
    }
}
