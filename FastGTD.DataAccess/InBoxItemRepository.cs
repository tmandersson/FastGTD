using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD.DataAccess
{
    public class InBoxItemRepository : ItemRepository<InBoxItem>, IItemPersistence<InBoxItem>
    {
        protected override string GetTableName()
        {
            return "InBoxItem";
        }
    }
}
