using FastGTD.DataTransfer;

namespace FastGTD.Domain
{
    public class ItemConverter : IItemConverter
    {
        private readonly IInBoxModel _inbox_model;
        private readonly IActionsListModel _actions_list_model;

        public ItemConverter(IInBoxModel inbox_model, IActionsListModel actions_list_model)
        {
            _inbox_model = inbox_model;
            _actions_list_model = actions_list_model;
        }

        public ActionItem ConvertToAction(InBoxItem item)
        {
            _inbox_model.Remove(item);
            ActionItem action = _actions_list_model.Add(item.Name);
            return action;
        }
    }
}