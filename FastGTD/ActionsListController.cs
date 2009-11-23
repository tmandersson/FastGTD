using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD
{
    public class ActionsListController : ItemListController<ActionItem>
    {
        public ActionsListController(
            IItemView<ActionItem> view,
            IGTDWindow window,
            IItemModel<ActionItem> model) : base(view, window, model)
        { }
    }
}
