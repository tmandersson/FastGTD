using FastGTD.Domain;

namespace FastGTD
{
    public class ActionsListController
    {
        public ActionsListController(IActionsListModel model)
        {
            model.Load();
        }
    }
}
