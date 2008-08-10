using System.Collections.Generic;

namespace FastGTD
{
    public interface IInboxModel
    {
        ICollection<InboxItem> InboxItems { get; }
        void AddInboxItem(string new_in_item);
    }

    public class InboxItem
    {
    }

    public class InboxModel : IInboxModel
    {
        public ICollection<InboxItem> InboxItems
        {
            get { return new List<InboxItem>(); }
        }

        public void AddInboxItem(string new_in_item)
        {
            throw new System.NotImplementedException();
        }
    }
}