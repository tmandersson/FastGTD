using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD.DataAccess
{
    public class ActionsRepository : ItemRepository<ActionItem>, IItemPersistence<ActionItem>
    {
        protected override string GetTableName()
        {
            return "ActionItem";
        }
    }
}