using System.Collections.Generic;

namespace FastGTD
{
    public interface IInboxModel
    {
        ICollection<string> InboxItems { get; }
        void AddInboxItem(string new_in_item);
    }

    public class InboxModel : IInboxModel
    {
        public ICollection<string> InboxItems
        {
            get { return new List<string>(); }
        }

        public void AddInboxItem(string new_in_item)
        {
        }
    }
}