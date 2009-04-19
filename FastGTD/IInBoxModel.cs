using System.Collections.Generic;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public interface IInBoxModel
    {
        void Load();
        InBoxItem CreateItem(string item);
        IList<InBoxItem> Items { get; }
        void RemoveItem(InBoxItem item);
        event VoidDelegate Changed;
    }
}