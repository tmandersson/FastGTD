using System.Collections.Generic;

namespace FastGTD
{
    public delegate void ContentUpdatedEvent();

    public class InBoxModel : IInBoxModel
    {
        public event ContentUpdatedEvent ContentUpdated;

        private readonly IList<string> _items;

        public InBoxModel()
        {
            _items = new List<string>();
        }
        public IList<string> InboxItems
        {
            get { return _items; }
        }

        public void AddInboxItem(string new_in_item)
        {
            _items.Add(new_in_item);
            if (ContentUpdated != null) ContentUpdated();
        }
    }
}