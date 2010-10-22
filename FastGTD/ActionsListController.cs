using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD
{
    public class ActionsListController : ItemListController<ActionItem>
    {
        public ActionsListController(
            IItemView<ActionItem> view,
            IItemModel<ActionItem> model) : base(view, view, model)
        { }
    }
}
