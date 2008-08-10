using System.Collections.Generic;

namespace FastGTD
{
    public interface IInBoxModel
    {
        IList<string> InboxItems { get; }
        void AddInboxItem(string new_in_item);
    }

    public class InBoxModel : IInBoxModel
    {
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
        }
    }
}