using System.Collections.Generic;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NHibernate;

namespace FastGTD.DataAccess
{
    public class ActionsRepository : ItemRepository, IItemPersistence<ActionItem>
    {
        public IList<ActionItem> GetAll()
        {
            ISession session = GetSession();
            return session.CreateCriteria(typeof(ActionItem)).List<ActionItem>();
        }

        public void DeleteAll()
        {
            ISession session = GetSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                session.Delete("from ActionItem");
                tx.Commit();
            }
        }

        public void Delete(ActionItem item)
        {
            ISession session = GetSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                session.Delete(item);
                tx.Commit();
            }
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
    }
}