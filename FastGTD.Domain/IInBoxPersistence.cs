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
        IList<ActionItem> GetAll();
        void DeleteAll();
        void Save(ActionItem item);
    }
}