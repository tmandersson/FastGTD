using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD
{
    public class ActionsListController : ItemListController<ActionItem>
    {
        public ActionsListController(
            IItemView view,
            IGTDWindow window,
            IItemModel<ActionItem> model,
            IPublishKeyEvents key_events)
            : base(view, window, model, key_events)
        { }
    }
}
