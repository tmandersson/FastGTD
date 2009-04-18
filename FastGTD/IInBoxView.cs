using System;
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

        ListViewController ListController { get; }
        bool KeyPreview { get; set; }
        string TextBoxText { get; set; }
        ListView ListViewInBoxItems { get; }
        event KeyEventHandler KeyDown;
        event EventHandler Resize;
        void SetFirstColumnFullWidth();
        event EventHandler AddButtonClick;
        event EventHandler DeleteButtonClick;
        
        void ClearInBoxItems();
        void AddInBoxItem(InBoxItem item);
    }
}