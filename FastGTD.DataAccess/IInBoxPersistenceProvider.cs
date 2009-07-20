using System.Collections.Generic;
using FastGTD.DataTransfer;

namespace FastGTD.DataAccess
{
    public interface IInBoxPersistenceProvider
    {
        IList<InBoxItem> GetAll();
        void DeleteAll();
        void Delete(InBoxItem item);
        void Save(InBoxItem item);
    }
}