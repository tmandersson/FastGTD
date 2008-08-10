using System.Collections.Generic;

namespace FastGTD
{
    public interface IInBoxModel
    {
        IList<string> InboxItems { get; }

        event ContentUpdatedEvent ContentUpdated;

        void AddInboxItem(string new_in_item);
    }
}