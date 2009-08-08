using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD
{
    public class ActionsListController
    {
        public ActionsListController(IItemModel<ActionItem> model)
        {
            model.Load();
        }
    }
}
