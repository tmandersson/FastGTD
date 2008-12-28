using System.Collections.Generic;
using FastGTD.DataTransfer;

namespace FastGTD.DataAccess
{
    public interface IInBoxItemRepository
    {
        IList<InBoxItem> GetAll();
        void DeleteAll();
    }
}