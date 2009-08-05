using System.Collections.Generic;
using FastGTD.DataTransfer;

namespace FastGTD.Domain
{
    public interface IActionsListModel
    {
        void Load();
        IList<ActionItem> Items { get; }
        ActionItem Add(string name);
        void ClearItems();
    }
}