using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD
{
    public class InBoxModel : ItemModel<InBoxItem>, IItemModel<InBoxItem>
    {
        public InBoxModel(IItemPersistence<InBoxItem> persistence) : base(persistence) { }
    }
}