using System;
using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD
{
    public class ItemConverter : IItemConverter
    {
        private readonly IItemModel<InBoxItem> _inbox_model;
        private readonly IItemModel<ActionItem> _actions_list_model;

        public ItemConverter(IItemModel<InBoxItem> inbox_model, IItemModel<ActionItem> actions_list_model)
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

        public IItemModel<ActionItem> ActionModel
        {
            get { return _actions_list_model; }
        }
    }
}