using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public interface IInBoxView
    {
        void SetFocusOnTextBox();

        void Show();
        void StartMessageLoop();
        void Close();

        bool KeyPreview { set; }
        string TextBoxText { get; set; }
        ListView ListViewInBoxItems { get; }
        IEnumerable<InBoxItem> SelectedItems { get; }
        event KeyEventHandler KeyDown;
        event EventHandler Resize;
        void SetFirstColumnFullWidth();
        event EventHandler AddButtonClick;
        event EventHandler DeleteButtonClick;
        
        void ClearInBoxItems();
        void AddInBoxItem(InBoxItem item);
        void MoveListSelectionDown();
        void MoveListSelectionUp();
    }
}