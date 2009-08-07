using FastGTD.DataTransfer;

namespace FastGTD.Domain
{
    public class ItemConverter : IItemConverter
    {
        private readonly IGTDItemModel<InBoxItem> _inbox_model;
        private readonly IGTDItemModel<ActionItem> _actions_list_model;

        public ItemConverter(IGTDItemModel<InBoxItem> inbox_model, IGTDItemModel<ActionItem> actions_list_model)
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