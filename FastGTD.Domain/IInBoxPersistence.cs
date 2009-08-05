using System.Collections.Generic;
using FastGTD.DataTransfer;

namespace FastGTD.Domain
{
    public interface IInBoxPersistence
    {
        IList<InBoxItem> GetAll();
        void DeleteAll();
        void Delete(InBoxItem item);
        void Save(InBoxItem item);
    }

    public interface IActionsListPersistence
    {
        void Save(ActionItem item);
    }
}