using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD
{
    public class ActionsListController
    {
        public ActionsListController(IGTDItemModel<ActionItem> model)
        {
            model.Load();
        }
    }
}
