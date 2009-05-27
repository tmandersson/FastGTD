using System.Collections.Generic;
using FastGTD.DataTransfer;

namespace FastGTD.DataAccess
{
    public interface IInBoxItemRepository : IInBoxItemPersister
    {
        IList<InBoxItem> GetAll();
        void DeleteAll();
        void Delete(InBoxItem item);
    }
}