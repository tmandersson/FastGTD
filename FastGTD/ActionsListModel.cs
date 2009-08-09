using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD
{
    public class ActionsListModel : ItemModel<ActionItem>, IItemModel<ActionItem>
    {
        public ActionsListModel(IItemPersistence<ActionItem> persistence) : base(persistence) { }
    }
}