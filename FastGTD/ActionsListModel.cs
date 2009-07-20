using System.Collections.Generic;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public class ActionsListModel
    {
        private readonly IList<ActionItem> _items = new List<ActionItem>();

        public IList<ActionItem> Items
        {
            get { return _items; }
        }

        public ActionItem Add(string name)
        {
            var action = new ActionItem(name);
            _items.Add(action);
            return action;
        }
    }
}
