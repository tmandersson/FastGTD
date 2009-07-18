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
    }
}
