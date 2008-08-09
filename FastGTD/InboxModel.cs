using System.Collections.Generic;

namespace FastGTD
{
    public interface IInboxModel
    {
        ICollection<InboxItem> InboxItems { get; }
        //AddInboxItem(string )
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
    }
}