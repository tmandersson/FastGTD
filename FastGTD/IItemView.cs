using System.Collections.Generic;
using FastGTD.Domain;

namespace FastGTD
{
    public interface IItemView
    {       
        void SetFocusOnTextBox();
        string TextBoxText { get; set; }
        IEnumerable<GTDItem> SelectedItems { get; }
        IListSelectionChanger List { get; }
        void ClearItems();
        void AddItem(GTDItem item);
    }
}