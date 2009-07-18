using System.Collections.Generic;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public interface IInBoxModel
    {
        void Load();
        InBoxItem Add(string item);
        void Remove(InBoxItem item);
        ActionItem ConvertToAction(InBoxItem item);
        IList<InBoxItem> Items { get; }
        event VoidDelegate Changed;
    }
}